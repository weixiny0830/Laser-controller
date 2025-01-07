#include <FlashAsEEPROM.h>
int Laser_service_time[3];
void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);
  if (EEPROM.isValid())
  {
    Laser_service_time[1] = (EEPROM.read(1) + 10) % 60; //add ten minutes when start
    Laser_service_time[0] = EEPROM.read(0) + ((EEPROM.read(1) + 10) / 60);
    //if the minutes is over 60, add 1 hour
  }
  else
  {
    Laser_service_time[0] = 0;
    Laser_service_time[1] = 0;
  }
}

int *Service_Time(int recorded_time[3])
{
  int *time_inloop = new int[3];//dynamic memory allocation
  unsigned long long running_time;
  int hour = 0, minute = 0, second = 0;
  running_time = millis() / 1000 + recorded_time[1] * 60 + recorded_time[0] * 3600;//add recorded time into the running time
  hour = running_time / 3600;
  minute = running_time % 3600 / 60;
  second = running_time % 3600 - (minute * 60);

  time_inloop[0] = hour;
  time_inloop[1] = minute;
  time_inloop[2] = second;

  if ((minute % 20 == 0) && second == 0)//recorded running time every 20 mins.
  {
    Serial.println("Time recorded!");
        for(int i=0;i<sizeof(time_inloop)-1;i++)//only write hour and minute into EEPROM
        {
          EEPROM.write(i, time_inloop[i]);
          }
        EEPROM.commit();
  }
  delay(1000);
  return time_inloop;
}

void loop() {
  int *Service_time = Service_Time(Laser_service_time);
  Serial.print("Hour:"); Serial.print(Service_time[0]);
  Serial.print("Minute:"); Serial.print(Service_time[1]);
  Serial.print("Second:"); Serial.println(Service_time[2]);
  delete[] Service_time; //release the dynamic memory

}
