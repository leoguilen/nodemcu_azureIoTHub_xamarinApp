#ifndef IOT_CONFIGS_H
#define IOT_CONFIGS_H

#define DEVICE_ID "nodemcu_b71f3b"
// Interval time(ms) for sending message to IoT Hub
#define INTERVAL 600000
#define MESSAGE_MAX_LEN 256
#define DHT_PIN D4
#define DHT_TYPE DHT11

/**
 * WiFi setup
 */
#define IOT_CONFIG_WIFI_SSID            "<WIFI_NAME>"
#define IOT_CONFIG_WIFI_PASSWORD        "<WIFI_PASSWORD>"
                                      
#define IOT_CONFIG_CONNECTION_STRING  "<IOT_HUB_CONNECTION_STRING>"

#endif /* IOT_CONFIGS_H */
