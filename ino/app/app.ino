#include <ESP8266WiFi.h>
#include <AzureIoTHub.h>
#include <AzureIoTProtocol_MQTT.h>
#include <AzureIoTUtility.h>
#include <ArduinoJson.h>

#include "config.h"
#include "message.h"
#include "iothubClient.h"
#include "iothubtransportmqtt.h"

// Wifi config
const char* ssid = IOT_CONFIG_WIFI_SSID;
const char* password= IOT_CONFIG_WIFI_PASSWORD;

// Azure Iot Hub config
static bool messagePending = false;
static bool messageSending = true;
static int interval = INTERVAL;
static IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle;
static const char* connectionString = IOT_CONFIG_CONNECTION_STRING;

void setup() {
  Serial.begin(115200);
  initWiFi();
  initTime();
  initSensor();

  // Create AureIOtHubConnectionString
  iotHubClientHandle = IoTHubClient_LL_CreateFromConnectionString(connectionString, MQTT_Protocol);
  
  if (iotHubClientHandle == NULL)
  {
        Serial.println(F("Failed on IoTHubClient_CreateFromConnectionString."));
        while (1);
  }

  Serial.println(F("IoTHubClient_CreateFromConnectionString Created")); 
  dht.begin(); 
}

static int messageCount = 1;
void loop() {
  delay(10000);
    
  if (!messagePending && messageSending)
  {
      char messagePayload[MESSAGE_MAX_LEN];
      readMessage(messageCount, messagePayload);
      sendMessage(iotHubClientHandle, messagePayload);
      messageCount++;
      delay(interval);
  }
  
  IoTHubClient_LL_DoWork(iotHubClientHandle);
  delay(10);
}

void initWiFi()
{
   Serial.print("Connecting to ");
   Serial.println(ssid);
   WiFi.begin(ssid,password);
   while (WiFi.status() != WL_CONNECTED)
   {
    delay(500);
    Serial.print(".");
   } 

    if(WiFi.status() == WL_CONNECTED)
    {
      Serial.println("");
      Serial.println("WiFi connected");
      Serial.println("IP address: ");
      Serial.println(WiFi.localIP());
    }
}

void initTime()
{
    time_t epochTime;
    configTime(0, 0, "pool.ntp.org", "time.nist.gov");

    while (true)
    {
        epochTime = time(NULL);

        if (epochTime == 0)
        {
            Serial.println("Fetching NTP epoch time failed! Waiting 2 seconds to retry.");
            delay(2000);
        }
        else
        {
            Serial.printf("Fetched NTP epoch time is: %lu.\r\n", epochTime);
            break;
        }
    }
}
