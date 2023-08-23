using UnityEngine;
using System.IO.Ports;
using System;


public class ArduinoButtonInput : MonoBehaviour
{
    [SerializeField] string portName = "COM3"; // 串口名称，根据实际情况更改
    [SerializeField] int baudRate = 9600;

    private System.IO.Ports.SerialPort serialPort;
    private JudgmentArea judgmentArea;
    private KeyCode[] buttonKeyCodes;

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();

        judgmentArea = FindObjectOfType<JudgmentArea>(); // 获取JudgmentArea脚本
        buttonKeyCodes = new KeyCode[] {
            KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, // 依次映射按钮0、按钮1、按钮2到不同的KeyCode
            KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6,
            KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9
        };
    }

    void Update()
    {
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string data = serialPort.ReadLine();
            string[] buttonStates = data.Trim().Split(',');

            for (int i = 0; i < buttonStates.Length; i++)
            {
                int buttonState = int.Parse(buttonStates[i]);

                if (buttonState == 1)
                {
                    HandleButtonPress(i);
                }
            }
        }
    }

    void HandleButtonPress(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < buttonKeyCodes.Length)
        {
            KeyCode correspondingKeyCode = buttonKeyCodes[buttonIndex];

            // 在这里调用JudgmentArea中的方法，传递对应的KeyCode
            judgmentArea.HandleKeyCode(correspondingKeyCode);
        }
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}






