#include "Arduino.h"
char exit_flag = 1;
int extern flash_start_address;
int extern flash_size;
unsigned short extern Laser_service_time[2];
unsigned short hour, factory_reset_hour = 0;
byte minute, second, factory_reset_flag = 0;

float extern current_param;
float extern power_param;
unsigned short extern Laser_Max_Power;

void Service_Time(unsigned short recorded_time[2])//record the laser service time into EEPROM
{
  byte *pow_parm2byte, *cur_parm2byte, *Maxpower2byte, hourbyte0, hourbyte1;
  unsigned long long running_time;
  byte *hour2byte;
  running_time = millis() / 1000 + !(factory_reset_flag) * (recorded_time[0] * 3600 + recorded_time[1] * 60) + factory_reset_flag * factory_reset_hour * 3600;//add recorded time into the running time

  hour = running_time / 3600;
  minute = running_time % 3600 / 60;
  second = running_time % 3600 - (minute * 60);
  hour2byte = short2byte(hour);
  hourbyte0 = hour2byte[0];
  hourbyte1 = hour2byte[1];

  if ((millis() % 1200000) < 550)//recorded running time every 20 mins +- 1.1 second.
  {
    //    Serial.println(millis());
    //    Serial.println("time recorded");

    pow_parm2byte = float2byte(power_param);//convert power_param to byte for saving in EEPROM, from low to high
    cur_parm2byte = float2byte(current_param);//convert power_param to byte for saving in EEPROM, from low to high
    Maxpower2byte = short2byte(Laser_Max_Power);

    EEPROM.write(flash_start_address,     hourbyte0);//EEPROM write takes 3.3ms to complete
    delay(5);
    EEPROM.write(flash_start_address + 1, hourbyte1);
    delay(5);
    EEPROM.write(flash_start_address + 2, minute); //only write hour and minute into EEPROM
    delay(5);

    EEPROM.write(flash_start_address + 3, pow_parm2byte[0]);//write pow/cur parameter into EEPROM only after service time passes 20 min
    delay(5);
    EEPROM.write(flash_start_address + 4, pow_parm2byte[1]);
    delay(5);
    EEPROM.write(flash_start_address + 5, pow_parm2byte[2]);
    delay(5);
    EEPROM.write(flash_start_address + 6, pow_parm2byte[3]);
    delay(5);

    EEPROM.write(flash_start_address + 7, cur_parm2byte[0]);//for loop doesn't work here for unknown reason
    delay(5);
    EEPROM.write(flash_start_address + 8, cur_parm2byte[1]);
    delay(5);
    EEPROM.write(flash_start_address + 9, cur_parm2byte[2]);
    delay(5);
    EEPROM.write(flash_start_address + 10, cur_parm2byte[3]);
    delay(5);

    EEPROM.write(flash_start_address + 11, Maxpower2byte[0]);
    delay(5);
    EEPROM.write(flash_start_address + 12, Maxpower2byte[1]);
    delay(5);

    EEPROM.write((flash_start_address + flash_size - 1), flash_start_address);//record the flash address to the last byte
    delay(5);

    EEPROM.commit();

    if (millis() % 10800000000 < 550) //change the flash address every 3000 hours
    {
      flash_start_address += flash_size;
      EEPROM.write((flash_start_address + flash_size - 1), flash_start_address);//record the flash address to the last byte
      EEPROM.commit();
    }
  }
  delay(333);
}

void laser_service_time()//display laser service time
{
  recv_command = "";
  String exit_time = "\
  Input \"Exit\" to exit.\n";
  char exit_flag = 1;
  Serial.println(exit_time);

  while (recv_command != "Exit\n")
  {
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
      Serial.println(recv_command);
    }
    Service_Time(Laser_service_time);
    Serial.print("Hour:"); Serial.print(hour);
    Serial.print(" Minute:"); Serial.print(minute);
    Serial.print(" Second:"); Serial.println(second);
  }
  recv_command = "";
}

void factory_reset_time(int FRhour)
{
  byte *pow_parm2byte, *cur_parm2byte= NULL, *Maxpower2byte= NULL, Maxpow0, Maxpow1, FRhourbyte0, FRhourbyte1;
  short FRhour_short;
  Laser_service_time[0] = FRhour;
  FRhour_short = short(FRhour);
  Maxpower2byte = short2byte(Laser_Max_Power);//convert Maxpower2byte to 2 bytes for saving in EEPROM
  Maxpow0 = Maxpower2byte[0];
  Maxpow1 = Maxpower2byte[1];//pointer points to wrong address, such that defines two bytes to save the returned max power value
  
  pow_par2byte = float2byte(power_param);//convert power_param to 4 bytes for saving in EEPROM, from low to high
  cur_par2byte = float2byte(current_param);//convert power_param to 4 bytes for saving in EEPROM, from low to high

  if (FRhour_short > 0 && FRhour_short < 60000)
  {
    byte *hour2byte;
    factory_reset_hour = FRhour_short;
    factory_reset_flag = 1;
    hour2byte = short2byte(FRhour_short);
    FRhourbyte0 = hour2byte[0];
    FRhourbyte1 = hour2byte[1];//the pointer points to the wrong address for some unknown reason, defines two byte variables to save the hour data
    
    EEPROM.write((flash_start_address + flash_size - 1), flash_start_address);//record the flash address to the last byte
    delay(5);
    
    EEPROM.write(flash_start_address,     FRhourbyte0);//EEPROM write takes 3.3ms to complete
    delay(5);
    EEPROM.write(flash_start_address + 1, FRhourbyte1);
    delay(5);
    EEPROM.write(flash_start_address + 2, 0); //only write hour and minute into EEPROM
    delay(5);

    EEPROM.write(flash_start_address + 3, pow_par2byte[0]);//write pow/cur parameter into EEPROM only after service time passes 20 min
    delay(5);
    EEPROM.write(flash_start_address + 4, pow_par2byte[1]);
    delay(5);
    EEPROM.write(flash_start_address + 5, pow_par2byte[2]);
    delay(5);
    EEPROM.write(flash_start_address + 6, pow_par2byte[3]);
    delay(5);

    EEPROM.write(flash_start_address + 7, cur_par2byte[0]);//for loop doesn't work here for unknown reason
    delay(5);
    EEPROM.write(flash_start_address + 8, cur_par2byte[1]);
    delay(5);
    EEPROM.write(flash_start_address + 9, cur_par2byte[2]);
    delay(5);
    EEPROM.write(flash_start_address + 10, cur_par2byte[3]);
    delay(5);
    
    EEPROM.write(flash_start_address + 11, Maxpow0);
    delay(5);
    EEPROM.write(flash_start_address + 12, Maxpow1);
    delay(5);

    
    EEPROM.commit();

    Serial.println("Successed");
    delay(1000);
  }
  else
    Serial.println("Failed");
}
