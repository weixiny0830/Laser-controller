#include"Arduino.h"

const int analogInPin = A4;  // Analog input pin that the potentiometer is attached to
const int analogOutPin = A0; // Analog output pin that the LED is attached to
float Serial_output_Value = 0, Analog_write_value = 0; //DAC output variables

float analog_write_2V(int serial_read_value)//analog write input is the percetage of 2V, i.e. 10% is 200mv
{
  Analog_write_value = (serial_read_value + 0.2) * 1026 * 0.606 * 0.01;//adjust the DAC output by adding offset, full scale is 2V.
  Serial_output_Value = serial_read_value * 3.3 * 0.606 * 10; //serial output value from 0-2V
  analogWrite(analogOutPin, int(Analog_write_value));//write the value to DAC
  return Serial_output_Value;
}

int array_delet(float *array_in, int sizeofarrzy, int del_num)//delect a element from an array
{
  for (int i = del_num; i < sizeofarrzy; i++)
  {
    array_in[i] = array_in[i + 1];
  }
  sizeofarrzy -= 1;
  return sizeofarrzy;//return the size of an array after deleting
}

float analog_read()//read voltage
//take 50 samplying points, cut 5 small value, cut 5 peak value, do the average for the rest
{
  const int size_of_sampling = 50;//50 samplying points.
  float max_ang_read = 0, min_ang_read = 0, average_analog_read, sum_read = 0;
  float analog_read_voltage[size_of_sampling];
  int min_pos = 0, max_pos = 0;
  int analog_read_value = 0; // value read from the port
  
  min_ang_read = analogRead(analogInPin);//read a analog data as min reference
  for (int i = 0; i < size_of_sampling; i++)
  {
    analog_read_value = analogRead(analogInPin);//read value from ADC, 12bit resolution
    analog_read_voltage[i] = (float(analog_read_value) / 4096) * 3.3 * 1000;
    delay(1);
  }

  int analog_array_size = sizeof(analog_read_voltage) / sizeof(analog_read_voltage[0]);//get the size of samplying array
  for (char i = 0; i < 5; i++)//cut 5 small values and 5 peak values
  {
    for (int i = 0; i < analog_array_size; i++)
    {
      if (analog_read_voltage[i] > max_ang_read)
      {
        max_ang_read = analog_read_voltage[i];
        max_pos = i;
      }
      if (analog_read_voltage[i] < min_ang_read)
      {
        min_ang_read = analog_read_voltage[i];
        min_pos = i;
      }

    }
    analog_array_size = array_delet(analog_read_voltage, analog_array_size, max_pos);//delete the 5 small
    analog_array_size = array_delet(analog_read_voltage, analog_array_size, min_pos);//delete the 5 peak
  }
  //   Serial.println(analog_array_size);
  for (int i = 0; i < analog_array_size; i++)//do the average
  {
    sum_read += analog_read_voltage[i];
  }
  average_analog_read = sum_read / float(analog_array_size) - 7;//the offset if consider to be 7mv
  return average_analog_read;
}
