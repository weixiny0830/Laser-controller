int led = 13;
String recv_command = "";

void setup() {

  // initialize the digital pin as an output.
  pinMode(led, OUTPUT);
  Serial.begin(115200);
}

void send_laser_data2computer()
{
  Serial.println("Sending Data...");
  Serial.println(recv_command);
  delay(500);
}

void COM_operation()
{
  Serial.println("Entering COM operation");
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
    COM_operation();
    recv_command = "";
    if (Serial.available() > 0)
    {
      recv_command = Serial.readString();
    }
  }
}
