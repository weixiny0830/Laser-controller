#include"Arduino.h"
int legal_input()//Wait command input, return 'true' if input is legal
{
  String menu_key[8];
  char legal_flag = 0;
  //There will be a "\n" added to the end of the string
  //when you use the Serial Monitor to input data
  menu_key[0] = "1\n"; menu_key[1] = "2\n"; menu_key[2] = "3\n"; menu_key[3] = "4\n";
  menu_key[4] = "5\n"; menu_key[5] = "6\n"; menu_key[6] = "Exit\n"; menu_key[7] = "Help\n";

  Serial.println("Please enter the number.");
  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    String input_command = Serial.readString();
    for (int i = 0; i < 8; i++)
    {
      if (input_command == menu_key[i])
      {
        legal_flag = 1;
        //        Serial.println("Next step");
        return i;
      }
      else
      {
        legal_flag = 0;
      }
    }
    if (legal_flag == 0)
      Serial.println("Input is not correct, please re-enter.");

  }
}

int Emission_input()//Wait command input, return 'true' if input is legal
{
  String menu_key[4];
  char legal_flag = 0;
  //There will be a "\n" added to the end of the string
  //when you use the Serial Monitor to input data
  menu_key[0] = "1\n"; menu_key[1] = "on\n"; menu_key[2] = "0\n"; menu_key[3] = "off\n";

  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    String input_command = Serial.readString();
    for (int i = 0; i < 4; i++)
    {
      if (input_command == menu_key[i])
      {
        legal_flag = 1;
        //        Serial.println("Next step");
        return i;
      }
      else
      {
        legal_flag = 0;
      }
    }
    if (legal_flag == 0)
      Serial.println("Input is not correct, please re-enter.");
  }
}

int power_input()//Wait command input, return 'true' if input is legal
{
  char legal_flag = 0;
  //There will be a "\n" added to the end of the string
  //when you use the Serial Monitor to input data

  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    int input_command = Serial.readString().toInt();
    if (input_command > 0 && input_command < 100)
    {
      Serial.print('.');
      delay(200);
      Serial.print('.');
      delay(200);
      Serial.print('.');
      delay(200);
      Serial.print('.');
      delay(200);
      Serial.print('.');
      delay(200);
      Serial.println('.');
      Serial.print("The power is set to: ");
      Serial.print(input_command);
      Serial.println("%");
      legal_flag = 1;
      return input_command;
    }
    else
    {
      legal_flag = 0;
    }
    if (legal_flag == 0)
      Serial.println("Input is not correct, please re-enter.");
  }
}
