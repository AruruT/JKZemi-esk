using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PhantomDeviceManager : MonoBehaviour
{
    // OpenHaptics DLLの関数をインポート
    [DllImport("hd")]
    private static extern int hdInitDevice(string deviceName);

    [DllImport("hd")]
    private static extern void hdStartScheduler();

    [DllImport("hd")]
    private static extern void hdStopScheduler();

    [DllImport("hd")]
    private static extern void hdGetDoublev(int param, double[] values);

    private const int HD_CURRENT_POSITION = 0x2000; // デバイスの位置パラメータ定数
    private int deviceHandle;

    void Start()
    {
        // デバイスの初期化
        deviceHandle = hdInitDevice("Default Device");
        if (deviceHandle == 0)
        {
            Debug.LogError("Phantomデバイスの初期化に失敗しました。");
            return;
        }
        Debug.Log("Phantomデバイスが正常に初期化されました。");

        // スケジューラの開始
        hdStartScheduler();
    }

    void Update()
    {
        if (deviceHandle != 0)
        {
            double[] position = new double[3];
            hdGetDoublev(HD_CURRENT_POSITION, position);

            // デバイスの現在位置をログに表示
            Debug.Log($"Device Position: X={position[0]}, Y={position[1]}, Z={position[2]}");
        }
    }

    void OnApplicationQuit()
    {
        // スケジューラを停止
        hdStopScheduler();
        Debug.Log("Phantomデバイスのスケジューラを停止しました。");
    }
}
