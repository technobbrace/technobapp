#include <SoftwareSerial.h>
SoftwareSerial mySerial(2, 3); // RX, TX



void setup() {
  // put your setup code here, to run once:
  mySerial.begin(9600);
  Serial.begin(9600);


// qua definisco il nome del bluetooth e l'indirizzo del service e della charachteristic

  sendCommand("AT");
  sendCommand("AT+ROLE0");
  sendCommand("AT+UUID0xFFE0");
  
  sendCommand("AT+CHAR0xFFE1");
  sendCommand("AT+NAMEADJUST");
}

void sendCommand(const char * command){   // qua manda un segnale all'app, per ora non mi serve
  Serial.print("Command send :");
  Serial.println(command);
  mySerial.println(command);
  //wait some time
  delay(100);
  
 char reply[100];
   char  i = 0;
  while (mySerial.available()) {
    reply[i] = mySerial.read();
    i += 1;
  }
  //end the string
  reply[i] = '\0';
  Serial.print(reply);
  Serial.println("Reply end");
}



void loop() { // qua leggo i dati sulla charachteristic e li trasformo in byte. ( 83 = segnale massaggio cervicale; 92 = segnale massaggio lombare;
  // 86 = on; 87 = off; 93 = setta postura; 84 = potenza +; 90 = potenza -;
  int c;
byte frase; 
  
  if (Serial.available()) {
    c = Serial.read();
    frase = byte(frase + c);
   Serial.print(frase);
    
    mySerial.print(frase);
  }
  if (mySerial.available()) {
    c = mySerial.read();
   frase = byte(frase + c);
    Serial.print(frase);
    
    mySerial.print(frase);   
  }

  if (frase == 83){
    digitalWrite(13, HIGH);
  }

  if ( frase == 92){
    digitalWrite(13, LOW);
  }
}
