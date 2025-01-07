#include "Arduino.h"
float extern current_param;
float extern power_param;
float extern current_cal_param;
unsigned short extern Laser_service_time[2];//save time, Laser_service_time[0] is hour, Laser_service_time[1] is minute
unsigned short extern Laser_Max_Power;
byte *pow_par2byte, *cur_par2byte;
void laser_calibration()
{
  Serial.println("Please enter the current laser power in 'mw' to calibrate the laser");
  float calibrated_power = power_calibrate_input();//return the calibrated power and then calculate the power parameter and current parameter.
  power_param = calibrated_power / analog_read(A3);
  current_param = calibrated_power / analog_read(A4);//this is the properity parameter calibration

  pow_par2byte = float2byte(power_param);//convert power_param to byte for saving in EEPROM, from low to high
  cur_par2byte = float2byte(current_param);//convert power_param to byte for saving in EEPROM, from low to high

  if (Laser_service_time[0] != 0 || Laser_service_time[1] > 20)
  {
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

    EEPROM.commit();
  }

  Serial.println("Calibration done!");
  delay(1000);
}

void laser_calibration_PC(float calibrated_power)
{
  if (calibrated_power > 0 && calibrated_power < Laser_Max_Power)
  {
    power_param = calibrated_power / analog_read(A3);
    current_param = calibrated_power / analog_read(A4) * current_cal_param;
    pow_par2byte = float2byte(power_param);//convert power_param to byte for saving in EEPROM, from low to high
    cur_par2byte = float2byte(current_param);//convert power_param to byte for saving in EEPROM, from low to high
    if (Laser_service_time[0] != 0 || Laser_service_time[1] > 20)
    {
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

      EEPROM.commit();
      delay(100);
    }
    Serial.println("Succeed");
  }
  else
    Serial.println("Failed");
}
