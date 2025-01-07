#include "Arduino.h"
short extern laser_status; //default laser emission is off
void laser_switch()
{
  pinMode(laser_emission, INPUT);
  laser_status = digitalRead(laser_emission);
  if (laser_status == 0)
  {
    Serial.println("Laser emission status: Off");
    Serial.println("Enter 1/on to start the laser emission.\n");
  }
  else
  {
    Serial.println("Laser emission status: On");
    Serial.println("Enter 0/off to stop the laser emission.\n");
  }
  int switch_input = Emission_input(); //A variable to save switch status

  switch (switch_input)
  {
    case 0:
      {
        pinMode(laser_emission, OUTPUT);
        digitalWrite(laser_emission, HIGH); //high logic level to turn on the laser
        Serial.println("The laser starts emission.\n");
        laser_status = 1;
        delay(100);
        break;
      }
    case 1:
      {
        pinMode(laser_emission, OUTPUT);
        digitalWrite(laser_emission, HIGH); //high logic level to turn on the laser
        Serial.println("The laser starts emission.\n");
        laser_status = 1;
        delay(100);
        break;
      }
    case 2:
      {
        pinMode(laser_emission, OUTPUT);
        digitalWrite(laser_emission, LOW); //low logic level to turn off the laser
        Serial.println("The laser stops emission.\n");
        laser_status = 0;
        delay(100);
        break;
      }
    case 3:
      {
        pinMode(laser_emission, OUTPUT);
        digitalWrite(laser_emission, LOW); //low logic level to turn off the laser
        Serial.println("The laser stops emission.\n");
        laser_status = 0;
        delay(100);
        break;
      }
  }
}

void laser_switch_PC_on()
{
  pinMode(laser_emission, OUTPUT);
  digitalWrite(laser_emission, HIGH); //high logic level to turn on the laser
  laser_status = 1;
}

void laser_switch_PC_off()
{
  pinMode(laser_emission, OUTPUT);
  digitalWrite(laser_emission, LOW); //high logic level to turn on the laser
  laser_status = 0;
}
