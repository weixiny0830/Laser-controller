#include "Arduino.h"
void laser_switch()
{
  char laser_status = 0; //default laser emission is off
  pinMode(laser_emission, INPUT);
  laser_status = digitalRead(laser_emission);
  if(laser_status == 0)
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

  switch(switch_input)
  {
    case 0:
    {
      pinMode(laser_emission, OUTPUT);
      digitalWrite(laser_emission,HIGH);//high logic level to turn on the laser
      Serial.println("The laser starts emission.\n");
      delay(1000);
      break;
      }
    case 1:
    {
      pinMode(laser_emission, OUTPUT);
      digitalWrite(laser_emission,HIGH);//high logic level to turn on the laser
      Serial.println("The laser starts emission.\n");
      delay(1000);
      break;
      }
    case 2:
    {
      pinMode(laser_emission, OUTPUT);
      digitalWrite(laser_emission,LOW);//high logic level to turn on the laser
      Serial.println("The laser stops emission.\n");
      delay(1000);
      break;
      }
    case 3:
    {
      pinMode(laser_emission, OUTPUT);
      digitalWrite(laser_emission,LOW);//high logic level to turn on the laser
      Serial.println("The laser stops emission.\n");
      delay(1000);
      break;
      }
    }
  }
