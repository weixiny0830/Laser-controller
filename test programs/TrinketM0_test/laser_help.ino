#include "Arduino.h" 
void laser_help()
{
  char exit_flag = 1;
  String menu = "\
  Welcome to the Laser Control Help.\n\
  Enter the number to see the description of each function.\n\
  1. Laser Current.\n\
  2. Laser Power Output.\n\
  3. Set Laser Power.\n\
  4. Emission.\n\
  5. Exit Help.\n";
  String current_help = "\
  Laser current: Read the real-time laser current output.\n";
  String power_help = "\
  Laser pwoer output: Read the real-time laser power output.\n";
  String set_power_help = "\
  Set laser power output: Allow the user to set the power directly.\n\
  To make a change, type a new value in the entry box and press \"Enter\".\n";
  String laser_emission = "\
  Laser emision: The Emission provides the capabilit to start\n\
  and stop laser emission. You can input either 1 or On to\n\
  start the emission, 0 or Off to stop the emission.";
  
  int selection_num; //Save the slection number
  Serial.println(menu);//Send menu to the terminal, no delay
  while(exit_flag == 1)
  {
    selection_num = legal_input();
    delay(200);
    switch(selection_num)
      {
        case 0: 
        {
          Serial.println(current_help);
          Serial.println('\n');
          Serial.println(menu);
          break;
          }
        case 1: 
        {
          Serial.println(power_help);
          Serial.println('\n');
          Serial.println(menu);
          break;
          }
        case 2: 
        {
          Serial.println(set_power_help);
          Serial.println('\n');
          Serial.println(menu);
          break;
          }
        case 3: 
        {
          Serial.println(laser_emission);
          Serial.println('\n');
          Serial.println(menu);
          break;
          }
        case 4: 
        {
          Serial.println("Exit Help");
          Serial.println('\n');
          exit_flag = 0;
          break;
          }
        case 6: 
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
