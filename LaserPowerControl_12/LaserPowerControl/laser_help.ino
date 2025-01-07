#include "Arduino.h"
void laser_help()
{
  char exit_flag = 1;
  const char menu[] PROGMEM = {"\
  Welcome to the Laser Control Help.\n\
  Enter the number to see the description of each function.\n\
  1. Laser Current.\n\
  2. Laser Power Output.\n\
  3. Set Laser Power.\n\
  4. Emission.\n\
  5. Laser Service Time.\n\
  6. Calibration.\n\
  7. Maximum Power.\n\
  8. Exit Help.\n"
                              };
  const char current_help[] PROGMEM = {"\
  Laser current: Read the real-time laser current output.\n"
                                      };
  const char power_help[] PROGMEM = {"\
  Laser pwoer output: Read the real-time laser power output.\n"
                                    };
  const char set_power_help[] PROGMEM = {"\
  Set laser power output: Allow the user to set the power directly.\n\
  To make a change, type a new value in the entry box and press \"Enter\".\n"
                                        };
  const char laser_emission[] PROGMEM = {"\
  Laser emision: The Emission provides the capabilit to start\n\
  and stop laser emission. You can input either 1 or On to\n\
  start the emission, 0 or Off to stop the emission."
                                        };
  const char service_time_help[] PROGMEM = {"\
  Laser service time: Read the total laser service time.\n"
                                           };
  const char calibration_help[] PROGMEM = {"\
  The user can calibrate laser's output power by the external powermeter.\n\
  User can simply input the value read from powermeter to \n\
  finish power calibration. Power calibration usually occurs at 70%-80% of maximum power output. \n\
  Note, DO NOT input the value higher than the maximum output power.\n"
                                           };
  const char Max_power_help[] PROGMEM = {"\
  This function allows the user to input the maximum output power of the laser.\n\
  Laser power control will be based on this value. Note, DO NOT input a value higher than \n\
  the rated power.\n"
                                           };
  char printChar;
  int selection_num; //Save the slection number
  for (byte k = 0; k < strlen_P(menu); k++) //Send menu to the terminal, no delay
  {
    printChar = pgm_read_byte_near(menu + k);
    Serial.print(printChar);
  }
  Serial.println();
  while (exit_flag == 1)
  {
    selection_num = legal_input();
    delay(200);
    switch (selection_num)
    {
      case 0:
        {
          for (byte k = 0; k < strlen_P(current_help); k++) {
            printChar = pgm_read_byte_near(current_help + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
      case 1:
        {
          for (byte k = 0; k < strlen_P(power_help); k++) {
            printChar = pgm_read_byte_near(power_help + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
      case 2:
        {
          for (byte k = 0; k < strlen_P(set_power_help); k++) {
            printChar = pgm_read_byte_near(set_power_help + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
      case 3:
        {
          for (byte k = 0; k < strlen_P(laser_emission); k++) {
            printChar = pgm_read_byte_near(laser_emission + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
      case 4:
        {
          for (byte k = 0; k < strlen_P(service_time_help); k++) {
            printChar = pgm_read_byte_near(service_time_help + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
        case 5:
        {
          for (byte k = 0; k < strlen_P(calibration_help); k++) {
            printChar = pgm_read_byte_near(service_time_help + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
        case 6:
        {
          for (byte k = 0; k < strlen_P(Max_power_help); k++) {
            printChar = pgm_read_byte_near(Max_power_help + k);
            Serial.print(printChar);
          }
          Serial.println();
          Serial.println('\n');
          Serial.println(menu);
          break;
        }
      case 7:
        {
          Serial.println("Exit Help");
          Serial.println('\n');
          exit_flag = 0;
          break;
        }
      case 8:
        {
          Serial.println("Exit Help");
          Serial.println('\n');
          exit_flag = 0;
          break;
        }
      default:
        {
          Serial.println("Input is not correct, please re-enter.");
          Serial.println('\n');
          Serial.println(menu);
          break;
        }

    }
  }
}
