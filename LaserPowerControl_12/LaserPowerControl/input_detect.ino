#include"Arduino.h"
unsigned short extern Laser_Max_Power;
int read_setPow = 0;//for displaying set power value, default equals to 0mW
int legal_input()//Wait command input, return 'true' if input is legal
{
  String menu_key[menu_num];
  char legal_flag = 0;
  //There will be a "\n" added to the end of the string
  //when you use the Serial Monitor to input data
  menu_key[0] = "1\n"; menu_key[1] = "2\n"; menu_key[2] = "3\n"; menu_key[3] = "4\n";
  menu_key[4] = "5\n"; menu_key[5] = "6\n"; menu_key[6] = "7\n"; menu_key[7] = "8\n";
  menu_key[8] = "9\n"; menu_key[9]  = "Exit\n"; menu_key[10] = "Help\n";

  Serial.println("Please enter the number.");
  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    String input_command = Serial.readString();
    for (int i = 0; i < menu_num; i++)
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
  int P_inPer = 0;
  //There will be a "\n" added to the end of the string
  //when you use the Serial Monitor to input data
  Serial.print("The current laser maximum output power is: ");
  Serial.print(Laser_Max_Power);
  Serial.println("mW");
  Serial.println("Please enter a number between 0 and the maximum output power.");
  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    int input_command = Serial.readString().toInt();
    if (input_command > 0 && input_command < (1.2 * Laser_Max_Power))
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
      legal_flag = 1;
      read_setPow = int(input_command);
      P_inPer = int((float(input_command) / float(Laser_Max_Power)) * 10000);
      return P_inPer;
    }
    else
    {
      legal_flag = 0;
    }
    if (legal_flag == 0)
      Serial.println("Input number is not correct, please enter a number between 0 and the maximum laser power.");
  }
}

int Max_power_input()//Wait command input, return 'true' if input is legal
{
  char legal_flag = 0;
  //There will be a "\n" added to the end of the string
  //when you use the Serial Monitor to input data

  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    unsigned short input_command = Serial.readString().toInt();
    if (input_command > 0 && input_command < 100000)
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
      legal_flag = 1;
      return input_command;
    }
    else
    {
      legal_flag = 0;
    }
    if (legal_flag == 0)
      Serial.println("Input number is not correct, please re-enter.");
  }
}

float power_calibrate_input()//detect whether the input is legal
{
  char legal_flag = 0;
  while (!Serial.available() && legal_flag == 0) //Wait until characters go into the Serial port
  {
    while (!Serial.available()); //Wait inside the loop
    float input_command = Serial.readString().toFloat();
    if (input_command > 0 && input_command < Laser_Max_Power)
    {
      legal_flag = 1;
      return input_command;
    }
    else
    {
      legal_flag = 0;
    }
    if (legal_flag == 0)
    {
      Serial.println("Input is not correct, please re-enter.");
      Serial.println("Power is usually set to 70% of the maximum output to calibrate the laser.");
    }
  }
}
