#include "Arduino.h"
float extern power_param;
float extern current_param;
float extern current_cal_param;
int extern flash_start_address;
int extern flash_size;
unsigned short extern Laser_Max_Power;
unsigned short extern Laser_service_time[2];

void laser_current(float current_param)//read laser running current
{
  Serial.println('\n');
  Serial.println("Enter \'Exit\' to exit to main menu.");
  Serial.println("The laser is running at the current of:");
  while (recv_command != "Exit\n")
  {
    Service_Time(Laser_service_time);
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
      Serial.println(recv_command);
    }
    Serial.print(analog_read(A4)*current_param);//read current from PIN7, IDE port A4
    Serial.println(" mA");
  }
  recv_command = "";
}

void laser_power(float power_param)//read laser running power
{
  Serial.println("Enter \'set\' to re-set the power output, or \'Exit\' to exit to main menu.");
  Serial.println("The laser is running at the power of:");
  while (recv_command != "Exit\n" && recv_command != "set\n")
  {
    Service_Time(Laser_service_time);
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
      Serial.println(recv_command);
    }
    Serial.print(analog_read(A3)*power_param);//read power from PIN8, IDE portA3
    Serial.println(" mW");
  }
  if (recv_command == "set\n")
  {
    laser_power_set(power_param);
    recv_command = "";
    laser_power(power_param);
  }
  else if (recv_command = "Exit\n")
    recv_command = "";
}

void laser_power_set(float power_param)//set laser power through COM
{
  float write_voltage = 0;//DAC outputs voltage
  float power_value_in_percentage = 0; //
  Serial.println("Please type the power value in mW:");
  power_value_in_percentage = power_input();
  write_voltage = analog_write_2V(power_value_in_percentage * 0.01);
  Serial.print("The laser power has been set to ");
  Serial.print(write_voltage * power_param);//Serial outputs the power in mw
  Serial.println(" mW");
  Serial.println('\n');
}

void laser_power_set_Max()//set laser maximum output power through COM
{
  byte *pow_par2byte, *cur_par2byte, *hour2byte, hourbyte0, hourbyte1, Maxpow0, Maxpow1;
  byte *Maxpower2byte;//save to EEPROM by byte

  Serial.println("Please enter the maximum power output in mW: ");
  Laser_Max_Power = Max_power_input();
  power_param = float(Laser_Max_Power) / 2000;
  current_param = power_param * current_cal_param;
  hour2byte = short2byte(Laser_service_time[0]);
  hourbyte0 = hour2byte[0];
  hourbyte1 = hour2byte[1];

  Maxpower2byte = short2byte(Laser_Max_Power);//convert Maxpower2byte to 2 bytes for saving in EEPROM
  Maxpow0 = Maxpower2byte[0];
  Maxpow1 = Maxpower2byte[1];//pointer points to wrong address, such that defines two bytes to save the returned max power value
  pow_par2byte = float2byte(power_param);//convert power_param to 4 bytes for saving in EEPROM, from low to high
  cur_par2byte = float2byte(current_param);//convert power_param to 4 bytes for saving in EEPROM, from low to high

  if (Laser_service_time[0] != 0 || Laser_service_time[1] > 20)
  {
    EEPROM.write(flash_start_address,     hourbyte0);//EEPROM write takes 3.3ms to complete
    delay(5);
    EEPROM.write(flash_start_address + 1, hourbyte1);
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

    EEPROM.write(flash_start_address + 11, Maxpow0);//write maximum output power to EEPROM
    delay(5);
    EEPROM.write(flash_start_address + 12, Maxpow1);
    delay(5);

    EEPROM.write((flash_start_address + flash_size - 1), flash_start_address);//record the flash address to the last byte
    delay(5);

    EEPROM.commit();
  }
  delay(950);
  Serial.print("The maximum power is set to: ");
  Serial.print(Laser_Max_Power);
  Serial.println("mW");
}

void laser_power_set_PC(int Pow_value)//set laser output power through PC command
{
  float Pow_set_percentage;
  if (Pow_value > 0 && Pow_value < 1.2 * Laser_Max_Power)
  {
    Pow_set_percentage = int(float((Pow_value) / float(Laser_Max_Power)) * 10000);
    float write_voltage = 0;//DAC outputs voltage
    write_voltage = analog_write_2V(Pow_set_percentage * 0.01);
    Serial.println("Succeed");
  }
  else
  {
    Serial.println("Failed");
  }
}

void laser_power_set_Max_PC(int Max_pow_set)//set laser maximum output power through PC command
{
  byte *pow_par2byte, *cur_par2byte, *hour2byte, hourbyte0, hourbyte1, Maxpow0, Maxpow1;
  byte *Maxpower2byte;//save to EEPROM by byte

  if (Max_pow_set > 0 && Max_pow_set < 100000)
  {
    Laser_Max_Power = Max_pow_set;
    power_param = float(Laser_Max_Power) / 2000;
    current_param = power_param * current_cal_param;

    Maxpower2byte = short2byte(Laser_Max_Power);//convert Maxpower2byte to 2 bytes for saving in EEPROM
    Maxpow0 = Maxpower2byte[0];
    Maxpow1 = Maxpower2byte[1];//pointer points to wrong address, such that defines two bytes to save the returned max power value
    pow_par2byte = float2byte(power_param);//convert power_param to 4 bytes for saving in EEPROM, from low to high
    cur_par2byte = float2byte(current_param);//convert power_param to 4 bytes for saving in EEPROM, from low to high
    hour2byte = short2byte(Laser_service_time[0]);
    hourbyte0 = hour2byte[0];
    hourbyte1 = hour2byte[1];

    if (Laser_service_time[0] != 0 || Laser_service_time[1] > 20)
    {
      EEPROM.write(flash_start_address,     hourbyte0);//EEPROM write takes 3.3ms to complete
      delay(5);
      EEPROM.write(flash_start_address + 1, hourbyte1);
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

      EEPROM.write(flash_start_address + 11, Maxpow0);//write maximum output power to EEPROM
      delay(5);
      EEPROM.write(flash_start_address + 12, Maxpow1);
      delay(5);

      EEPROM.write((flash_start_address + flash_size - 1), flash_start_address);//record the flash address to the last byte
      delay(5);

      EEPROM.commit();
    }
    delay(950);
    Serial.println("Succeed");
  }
  else
  {
    Serial.println("Failed");
  }
}
