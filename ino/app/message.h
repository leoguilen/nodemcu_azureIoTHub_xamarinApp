#include <ArduinoJson.h>
#include <DHT.h>

#include "config.h"

static DHT dht(DHT_PIN, DHT_TYPE);
void initSensor()
{
    dht.begin();
}

void readMessage(int messageId, char *payload)
{
    float h = dht.readHumidity();
    // Read temperature as Celsius
    float t = dht.readTemperature();
    // Read temperature as Fahrenheit
    float f = dht.readTemperature(true);
    // Compute heat index in Fahrenheit (the default)
    float hif = dht.computeHeatIndex(f, h);
    // Compute heat index in Celsius (isFahreheit = false)
    float hic = dht.computeHeatIndex(t, h, false);
  
    // Check if any reads failed and exit early (to try again).
    if (isnan(h) || isnan(t) || isnan(f)) {
      Serial.println(F("Failed to read from DHT sensor!"));
      return;
    }
 
    StaticJsonBuffer<MESSAGE_MAX_LEN> jsonBuffer;
    JsonObject &root = jsonBuffer.createObject();
    
    root["deviceId"] = DEVICE_ID;
    root["messageId"] = messageId;

    // NAN is not the valid json, change it to NULL
    if (std::isnan(h))
    {
        root["humidity"] = NULL;
    }
    else
    {
        root["humidity"] = h;
    }
    
    if (std::isnan(t))
    {
        root["tempInCelsius"] = NULL;
    }
    else
    {
        root["tempInCelsius"] = t;
    }

    if (std::isnan(f))
    {
        root["tempInFahrenheit"] = NULL;
    }
    else
    {
        root["tempInFahrenheit"] = f;
    }

    root["heatIndexInFahrenheit"] = hif;
    root["heatIndexInCelsius"] = hic;

    root.printTo(payload, MESSAGE_MAX_LEN);
}
