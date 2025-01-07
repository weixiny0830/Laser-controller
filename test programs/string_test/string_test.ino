String recv_command = "";
void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
}

String menu = "\
Welcome to the Laser Control Panel.\n\
Enter the number to execute the function.\n\
1. Laser Current.\n\
2. Laser Power Output.\n\
3. Set Laser Power Output.\n\
4. Turn off the Laser.\n\
5. Exit the Laser Control Panel.\n\
6. Help.\n";

//struct angles
//{
//  bool a;
//  int b;
//};
//struct angles f()
//{
//  struct angles ang;
//  ang.a = true;
//  ang.b = 10;
//  return ang;
//}

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
        Serial.println("Next step");
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


void loop() {
  int i=1;
  i = legal_input();
  Serial.print("return value=");
  Serial.println(i);
  delay(200);
}
