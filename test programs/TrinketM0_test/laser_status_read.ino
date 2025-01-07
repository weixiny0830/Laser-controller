#include "Arduino.h" 
void laser_current(float current_param)
{
  Serial.println('\n');
  Serial.println("Enter \'Exit\' to exit to main menu.");
  Serial.println("The laser is running at the current of:");
  while (recv_command != "Exit\n")
  {
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
      Serial.println(recv_command);
    }
    Serial.print(analog_read()*current_param);
    Serial.println(" mA");
    delay(500);
  }
  recv_command = "";
}

void laser_power(float power_param)
{
  Serial.println("Enter \'set\' to re-set the power output, or \'Exit\' to exit to main menu.");
  Serial.println("The laser is running at the power of:");
  while (recv_command != "Exit\n" && recv_command != "set\n")
  {
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
      Serial.println(recv_command);
    }
    Serial.print(analog_read()*power_param);
    Serial.println(" mW");
    delay(500);
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

void laser_power_set(float power_param)
{
  float write_voltage = 0;//DAC outputs voltage
  int power_value_in_percentage = 0; //
  Serial.println("Please type the power value in percentage:");
  power_value_in_percentage = power_input();
  write_voltage = analog_write_2V(power_value_in_percentage);
  Serial.print("The laser power has been set to ");
  Serial.print(write_voltage * power_param);//Serial outputs the power in mw
  Serial.println(" mW");
  Serial.println('\n');
}
