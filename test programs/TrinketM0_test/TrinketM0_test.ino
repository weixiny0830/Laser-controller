const int laser_emission = 0; //the number of the laser emission switch

String recv_command = "";
String menu = "\
Welcome to the Laser Control Panel.\n\
Enter the number to execute the function.\n\
1. Laser Current.\n\
2. Laser Power Output.\n\
3. Set Laser Power.\n\
4. Emission.\n\
5. Exit the Laser Control Panel.\n\
6. Help.\n";

void setup() {
  analogReadResolution(12); //Analog Read Resolution is the same write
  // initialize the digital pin as an output.
  Serial.begin(115200);
  analog_write_2V(100);//default 100% output
}

void laser_exit()
{
  recv_command = "Exit\n";
  Serial.println("Exit the Laser Control Panel.");
  Serial.println('\n');
}
//

void COM_operation()
{
  int selection_num; //save the operator's selection to this variable
  Serial.println(menu);//Send menu to the terminal, no delay
  selection_num = legal_input(); //Wait command input, return the command number if input is legal
  //otherwise, the program will stay in the loop
  delay(200);
  switch (selection_num)
  {
    case 0: laser_current(1);
      break;
    case 1: laser_power(1);
      break;
    case 2: laser_power_set(1);
      break;
    case 3: laser_switch(); //laser switch, enter 0/off to turn off, 1/on to turn on
      break;
    case 4: laser_exit();//给recv_command赋值“Exit”来退出
      break;
    case 5: laser_help();
      break;
    case 6: laser_exit();//给recv_command赋值“Exit”来退出
      break;
    case 7: laser_help();
      break;
  }
}

void send_laser_data2computer()
{
  Serial.println("Sending Data...");
  Serial.println(recv_command);
  delay(500);
}

void loop() {
  while (recv_command != "Menu\n") //open COM menu when the operator enters "Menu"
  {
    send_laser_data2computer();//send data to computer for control
    recv_command = "";
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
    }
  }
  while (recv_command != "Exit\n")
  {
    recv_command = "";
    COM_operation();
  }
}
