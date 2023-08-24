using UnityEngine;
using System.IO.Ports;

public class ArduinoButtonInput : MonoBehaviour
{
    [SerializeField] int baudRate = 9600;

    private System.IO.Ports.SerialPort serialPort;
    private JudgmentArea[] judgmentAreas;

    void Start()
    {
        string[] availablePortNames = SerialPort.GetPortNames();

        if (availablePortNames.Length == 0)
        {
            Debug.LogError("No available serial ports found.");
            return;
        }

        // 取第一个可用的串口名称
        string portName = availablePortNames[0];
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();

        judgmentAreas = FindObjectsOfType<JudgmentArea>(); // 获取所有 JudgmentArea 脚本的实例
    }

    void Update()
    {
        if (serialPort.IsOpen && serialPort.BytesToRead > 0)
        {
            string data = serialPort.ReadLine();
            Debug.Log("Received data from Arduino: " + data);
            string[] buttonStates = data.Trim().Split(',');

            for (int i = 0; i < buttonStates.Length; i++)
            {
                int buttonState = int.Parse(buttonStates[i]);

                if (buttonState == 1)
                {
                    Debug.Log("Button " + i + " pressed");
                    HandleButtonPress(i);
                }
            }
        }
    }

    void HandleButtonPress(int buttonIndex)
    {
        KeyCode[] unityKeyCodes = {
            KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F,
            KeyCode.Space, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L
        };

        if (buttonIndex >= 0 && buttonIndex < unityKeyCodes.Length)
        {
            KeyCode correspondingKeyCode = unityKeyCodes[buttonIndex];
            // 遍历所有 JudgmentArea 实例，调用 HandleButtonPress 方法传递对应的 KeyCode
            foreach (var judgmentArea in judgmentAreas)
            {
                judgmentArea.HandleButtonPress(correspondingKeyCode);
            }
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