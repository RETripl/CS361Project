# CS361Project
Initial Upload

TCP/IP MicroService to return dates of various crops.

This is a simple microservice that uses TCP/IP client-microservice application in C# that receives a crop name from the client and returns 3 dates from data that 
is hosted on the microservice. 

Microservice Application:

The service application listens on a TCP port for incoming connections from the client. When a client sends a message containing a crop the microservice receives it, processes it, then sends a response back to the client containing 3 key dates about the crop. 

The service application sends the three key dates back in a JSON encoded string. 

To run the microservice application, open the "serverApp" project in Visual Studio, build the solution, then build "Program.cs" and run serverApp.exe. 

Requirements:

* Visual Studio
* Newtonsoft.Json NuGetPackage

How to Use
* Clone the repository
* Open the serverApp project in VS
* Build the solution
* Run serverApp.exe
* Run the client
* Follow the prompts in the client console to send messages and receive responses from the microservice

Communication Contract:

-Protocol
  * The communication protocol is TCP
  * The server listens on a specific IP address (127.0.0.1) and port (8000) 
  * The client connects to the microservice over TCP.
  * The micro service receives the message from the client, processes it and sends a response back to the client
  * The client receives the response from the server and prints it to the console. 
  
-Message Format
  * Messages are UTF-8 encoded strings
  * The client sends a message to the server that contains a single crop
  * The microservice sends a response to the client that contains a list of three key dates for the crop. (Plant seed inside, plant seed outside, plant plant outside)
  * The list of strings is encoded as a JSON string. 
  
-Error Handling
  * If the microservice encounters an error while processing a message, it sends an error response to the client. The error response is a JSON string that contains an     error code and an error message. 
  
  

UML Documentation:

![image](https://user-images.githubusercontent.com/105454086/218640051-6de7a149-39f0-4a4d-b977-f45cb7ce3cc9.png)
