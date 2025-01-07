#include "Arduino.h"
byte * short2byte(short in_data)
{
  static byte data_in_byte[2];
  byte data_low = byte(in_data & 0xFF);
  byte data_high = byte(in_data >> 8);
  data_in_byte[0] = data_low;  //save the low 8bits
  data_in_byte[1] = data_high; //save the high 8bits

  return data_in_byte;
}

unsigned short byte2short(byte *byte_p)
{
  unsigned short short_data;
  short_data = short(*(byte_p + 1) << 8) + short(*byte_p);
  return short_data;
}

byte * float2byte(float in_data)
{
  static byte data_in_byte[4];
  unsigned long longdata = 0;
  longdata = *(unsigned long*)&in_data;
  byte data_low1 = byte(longdata & 0xFF);
  byte data_low2 = byte((longdata >> 8) & 0xFF);
  byte data_high1 = byte((longdata >> 16)  & 0xFF);
  byte data_high2 = byte(longdata >> 24);

  data_in_byte[0] = data_low1;
  data_in_byte[1] = data_low2;
  data_in_byte[2] = data_high1;
  data_in_byte[3] = data_high2;
  return data_in_byte;
}

float byte2float(byte *byte_p)
{
  float float_data;
  unsigned long longdata = (*(byte_p + 3) << 24) + (*(byte_p + 2) << 16) + (*(byte_p + 1) << 8) + (*byte_p);
  float_data = *(float*)&longdata;
  return float_data;
}

//byte *word2byte;
//word2byte = float2byte(12312.23);
//float b;
//b = byte2float(word2byte);
//Serial.println(word2byte[0]);
//Serial.println(word2byte[1]);
//Serial.println(word2byte[2]);
//Serial.println(word2byte[3]);
//Serial.println();
//Serial.println(b);

String * rev_substring(String incoming_string)
{
  int recv_position, recv_index = 0;
  static String recv_buff[2];
  do        // do-while loop to substring received command by space
  {
    recv_position = incoming_string.indexOf(" ");
    if (recv_position != -1)// if space exists, split string
    {
      recv_buff[recv_index] = incoming_string.substring(0, recv_position);
      recv_index++;
      incoming_string = incoming_string.substring(recv_position + 1, incoming_string.length());
    }
    else  //if no space, save the string directly
    {
      if (incoming_string.length() > 0)
      recv_buff[recv_index] = incoming_string;
    }
  }
  while (recv_position >= 0);
  incoming_string = "";
  return recv_buff;
}
