void sendCallback(IOTHUB_CLIENT_CONFIRMATION_RESULT result, void *userContextCallback)
{
  if (result == 0)
  {
    Serial.println(F("Message sent to Azure IoT Hub."));
  }
  else
  {
    Serial.println(F("Failed to send message to Azure IoT Hub."));
  }
}

void sendMessage(IOTHUB_CLIENT_LL_HANDLE iotHubClientHandle, char *buffer)
{
  IOTHUB_MESSAGE_HANDLE messageHandle = IoTHubMessage_CreateFromByteArray((const unsigned char *)buffer, strlen(buffer));
  if (messageHandle == NULL)
  {
      Serial.println(F("Unable to create a new IoTHubMessage."));
  }
  else
  {
    (void)printf("Sending message: %s.\r\n", buffer);
    if (IoTHubClient_LL_SendEventAsync(iotHubClientHandle, messageHandle, sendCallback, NULL) != 0)
    {
        Serial.println(F("Failed to hand over the message to IoTHubClient."));
    }
    else
    {
        Serial.println(F("IoTHubClient accepted the message for delivery."));
    }
    
    IoTHubMessage_Destroy(messageHandle);
  }
}