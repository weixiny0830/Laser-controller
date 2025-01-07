float anaread[5] = {0,1,2,3,4};
int sizearray = sizeof(anaread)/sizeof(anaread[0]);
void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
}

int array_delet(float *array_in, int sizeofarrzy, int del_num)
{
  for(int i = del_num; i < sizeofarrzy; i++)
  {
    array_in[i] = array_in[i+1];
    }
    sizeofarrzy-=1;
    return sizeofarrzy;
  }
void loop() {
  if(Serial.read() == '2')
  {
    sizearray = array_delet(anaread,sizearray,2);
    }
  for(int i=0;i<sizearray;i++)
  {
    Serial.print(anaread[i]);
    }
    Serial.print("size of arrzy");
    Serial.println(sizearray);
  delay(1000);
}
