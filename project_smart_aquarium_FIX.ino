#include <Servo.h>
int sensorPin = A0;            // select the input pin for the ldr
unsigned int sensorValue = 0;  // variable to store the value coming from the ldr
Servo myservo;  // create servo object to control a servo 
                // a maximum of eight servo objects can be created 
 
int pos = 0;    // variable to store the servo position 

//long FISHFEEDER = 43200000; // 12 hours
//long FISHFEEDER = 57600000; // 16 hours
long FISHFEEDER = 10000; // 10 detik

long endtime; 
long now;

void setup()
{
  Serial.begin(9600);// start serial for output - for testing
  pinMode(13, OUTPUT);
  pinMode(sensorPin, INPUT);
  //Start Serial port
  delay(2);
  
  myservo.attach(9);        // attaches the servo on pin 9 to the servo object 
  myservo.write(0);
  delay(15);
}

void loop()
{
  // read the value from the ldr:
  sensorValue = analogRead(sensorPin);     
  if(sensorValue<=150) digitalWrite(13, HIGH);   // set the LED on
  else digitalWrite(13, LOW);   // set the LED on
  
  // For DEBUGGING - Print out our data, uncomment the lines below
  //Serial.print(sensorValue, DEC);     // print the value (0 to 1024)
  //Serial.println("");                   // print carriage return  
  //delay(500);  
  
  now = millis();
  endtime = now + FISHFEEDER;
  
  while(now < endtime) {
   myservo.write(0);
   now = millis();   
  }
  

  for(pos = 0; pos < 180; pos += 1)  // goes from 0 degrees to 180 degrees 
  {                                  // in steps of 1 degree 
    myservo.write(pos);              // tell servo to go to position in variable 'pos' 
    delay(15);                       // waits 15ms for the servo to reach the position 
  } 
  for(pos = 180; pos>=1; pos-=1)     // goes from 180 degrees to 0 degrees 
  {                                
    myservo.write(pos);              // tell servo to go to position in variable 'pos' 
    delay(15);                       // waits 15ms for the servo to reach the position 
  } 
}
