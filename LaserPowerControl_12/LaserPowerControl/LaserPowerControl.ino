//***************PIN MAP******************//
//current read: physical PIN 7, port PIN PA06
//pwoer read: physical PIN 8, port PIN PA07
//switch pin: physical PIN 12, port PIN PA09, IDE 2
//SWC, SWD, GND are for bootloader download
//D+ and D- are for USB hub
//****************************************//
  
#include <FlashAsEEPROM.h>
#include <avr/pgmspace.h>
#include <string.h>
const int laser_emission = 2; //the pin of the laser emission, physical pin 12, port pin PA09
const int menu_num = 11;
String recv_command = "";

unsigned short Laser_service_time[2];//save time, Laser_service_time[0] is hour, Laser_service_time[1] is minute
int flash_start_address = 0;//read data from the flash address
int flash_size = 14;//bytes that saved in the flash, 0-1hour, 2-minute, 3-6pow parameter, 7-10current parameter, 11-12max power, 13-flash address

unsigned short extern hour;
byte extern minute, second;
short laser_status = 1;

unsigned short Laser_Max_Power;//variable to save maximum power
float current_cal_param = 1;
float power_param;//pow_param equals maximum power(mW) / maximum voltage(mV), maximum voltage = 2000mW
float current_param;//current parameter is calculated by the hardware design
int extern read_setPow;


const char menu[] PROGMEM = {"\
Welcome to the Laser Control Panel.\r\n\
Enter the number to execute the function.\r\n\
1. Laser Current.\r\n\
2. Laser Power Output.\r\n\
3. Set Laser Power.\r\n\
4. Emission.\r\n\
5. Laser Service Time.\r\n\
6. Calibration.\r\n\
7. Maximum Power.\r\n\
8. Exit Control Panel.\r\n\
9. Help.\n"
                            };//save the constant string to Flash memory to save running RAM. Notice, the size of the string is limited, don't go over the size limit.


void setup() {
  analogReadResolution(12); //Analog Read Resolution is the same write
  // initialize the digital pin as an output.
  Serial.begin(115200);
  analog_write_2V(int(0.1 * Laser_Max_Power)); //default 10% output
  
  byte service_time_hour[2], pow_param[4], cur_param[4], max_power[2];
  if (EEPROM.isValid())//if data have been written in EEPROM, read saved data from EEPROM
  {
    flash_start_address = EEPROM.read(flash_start_address + flash_size - 1);//read flash start address from the last byte of saved EEPROM
    
    service_time_hour[0] = EEPROM.read(flash_start_address);//read two bytes of hour from EEPROM
    service_time_hour[1] = EEPROM.read(flash_start_address + 1);
    Laser_service_time[0] = byte2short(service_time_hour);//combine two bytes into a short

    Laser_service_time[1] = (EEPROM.read(flash_start_address + 2) + 30) % 60; //add 30 minutes when start
    Laser_service_time[0] = Laser_service_time[0] + ((EEPROM.read(2) + 30) / 60);//if the minutes is over 60, add 1 hour

    pow_param[0] = EEPROM.read(flash_start_address + 3);
    pow_param[1] = EEPROM.read(flash_start_address + 4);
    pow_param[2] = EEPROM.read(flash_start_address + 5);
    pow_param[3] = EEPROM.read(flash_start_address + 6);

    cur_param[0] = EEPROM.read(flash_start_address + 7);
    cur_param[1] = EEPROM.read(flash_start_address + 8);
    cur_param[2] = EEPROM.read(flash_start_address + 9);
    cur_param[3] = EEPROM.read(flash_start_address + 10);

    max_power[0] = EEPROM.read(flash_start_address + 11);
    max_power[1] = EEPROM.read(flash_start_address + 12);
    
    power_param = byte2float(pow_param); //read power parameter
    current_param = byte2float(cur_param); //read current parameter
    Laser_Max_Power = byte2short(max_power);//read maximum output power
  }
  else//if the laser is  the first time to start, give the default value to each parameter.
  {
    flash_start_address = 0;// read/write data from initialized address 0.
    Laser_service_time[0] = 0;
    Laser_service_time[1] = 0;

    Laser_Max_Power = 5;//default the maximum power to be 5mW.

    power_param = float(Laser_Max_Power * 0.0005);//Max power / 2000mV = parameter
    current_param = power_param * current_cal_param;
  }

  pinMode(laser_emission, INPUT);//check the laser emission status when start running the program
  laser_status = digitalRead(laser_emission);
  delay(2000);
}

void laser_exit()
{
  recv_command = "Exit\n";
  Serial.println("Exit the Laser Control Panel.");
  Serial.println('\n');
}
//

void COM_operation()
{
  char printChar;
  int selection_num; //save the operator's selection to this variable
  for (byte k = 0; k < strlen_P(menu); k++) //Send menu to the terminal, no delay
  {
    printChar = pgm_read_byte_near(menu + k);
    Serial.print(printChar);
  }
  Serial.println();
  selection_num = legal_input(); //Wait command input, return the command number if input is legal
  //otherwise, the program will stay in the loop
  delay(200);
  switch (selection_num)
  {
    case 0: laser_current(current_param);
      break;
    case 1: laser_power(power_param);
      break;
    case 2: laser_power_set(power_param);
      break;
    case 3: laser_switch(); //laser switch, enter 0/off to turn off, 1/on to turn on
      break;
    case 4: laser_service_time(); //read the total laser service time
      break;
    case 5: laser_calibration(); //calibrate laser power output
      break;
    case 6: laser_power_set_Max(); //set the maximum laser output power
      break;
    case 7: laser_exit();//type “Exit” to close the controller
      break;
    case 8: laser_help();//type help to read the description
      break;
    case 9: laser_exit();
      break;
    case 10: laser_help();
      break;
  }
}

void send_laser_data2computer()
{
  float current_read = analog_read(A4) * current_param;//read current from physical PIN 7, port PIN PA06
  float power_read = analog_read(A3) * power_param;//read power from physical PIN 8, port PIN PA07
  Service_Time(Laser_service_time);

  Serial.print("Laser,");
  Serial.print(laser_status);
  Serial.print(",");
  Serial.print(current_read);
  Serial.print(",");
  Serial.print(power_read);
  Serial.print(",");
  Serial.print(hour);
  Serial.print(",");
  Serial.print(minute);
  Serial.print(",");
  Serial.print(second);
  Serial.print(",");
  Serial.print(current_param, 4);
  Serial.print(",");
  Serial.print(power_param, 4);
  Serial.print(",");
  Serial.print(Laser_Max_Power);
  Serial.print(",");
  Serial.print("End");
  Serial.print(",");
  unsigned short checksum = 5 + String(laser_status).length() + String(current_read).length() + \
                            String(power_read).length() + String(hour).length() + \
                            String(minute).length() + String(second).length() + \
                            String(current_param).length() + String(power_param).length() + String(Laser_Max_Power).length() + 7;//length of "End" plus 4 '.'
  Serial.print(checksum);
  Serial.println(",");
  Serial.println("Please enter \"Menu\" to enter laser control list,");
  Serial.println(recv_command);
}

void loop() {
  while (recv_command != "Menu\n" && !(recv_command.startsWith("{"))) //open COM menu when the operator enters "Menu"
  {
    send_laser_data2computer();//send data to computer for control
    recv_command = "";
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
    }
  }
  while (recv_command != "Exit\n" && !(recv_command.startsWith("{")))
  {
    recv_command = "";
    COM_operation();
  }
  if (recv_command.startsWith("{")) //if the commends come from windows application
  {
    if (recv_command.charAt(recv_command.length() - 2) == '}') //for some reason, the order string.endsWith doesn't work here, check the last character is '}'
    {
      int sig_pos1; int sig_pos2;
      String PCcommand = "";
      String PCparameter = "";
      String *recv_C_split;
      recv_C_split = rev_substring(recv_command);

      PCcommand = recv_C_split[0];
      PCparameter = recv_C_split[1];

      if (PCparameter == "") //if no paramater, run this secion
      {
        PCcommand = PCcommand.substring(1, PCcommand.length() - 2);
        if (PCcommand == "lason")
          laser_switch_PC_on();
        else if (PCcommand == "lasoff")
          laser_switch_PC_off();
        else if (PCcommand == "getpower")
          Serial.println(analog_read(A3)*power_param);
        else if (PCcommand == "readset")
          Serial.println(read_setPow);
        else if (PCcommand == "hour")
          Serial.println(Laser_service_time[0]);
        else if (PCcommand == "ConVer")
          Serial.println("PCFCs,1");
        else if (PCcommand == "getcal")
        {
          Serial.println(power_param,4);
          Serial.println(current_param,4);
        }
      }
      else//if parameter exists, run this section
      {
        PCcommand = PCcommand.substring(1, PCcommand.length());
        PCparameter = PCparameter.substring(0, PCparameter.length() - 2);
        if (PCcommand == "setpower")
        {
          laser_power_set_PC(PCparameter.toInt());
          read_setPow = PCparameter.toInt();
        }
        else if (PCcommand == "cal")
          laser_calibration_PC(PCparameter.toFloat());
        else if (PCcommand == "setpowerMax")
          laser_power_set_Max_PC(PCparameter.toInt());
        else if (PCcommand == "CLFRTC")
          factory_reset_time(PCparameter.toInt());
      }
      recv_C_split[0] = "";
      recv_C_split[1] = "";
    }
    else//if the last character is not "}", means incorrect input
    {
      Serial.println("Incorrect Input, please re-enter the command");
    }
  }
  recv_command = "";
}
