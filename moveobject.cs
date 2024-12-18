using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class PhantomObjectManipulator : MonoBehaviour
{
    // OpenHaptics DLL関数のインポート
    [DllImport("hd")]
    private static extern int hdInitDevice(string deviceName);

    [DllImport("hd")]
    private static extern void hdStartScheduler();

    [DllImport("hd")]
    private static extern void hdStopScheduler();

    [DllImport("hd")]
    private static extern void hdGetDoublev(int param, double[] values);

    [DllImport("hd")]
    private static extern int hdGetIntegerv(int param, out int value);

    [DllImport("hd")]
    private static extern void hdSetForce(double[] force);

    // 定数
    private const int HD_CURRENT_POSITION = 0x2000; // 現在のデバイス位置
    private const int HD_CURRENT_BUTTONS = 0x2001;  // 現在のボタン状態

    private int deviceHandle;
    private GameObject selectedObject = null;

    void Start()
    {
        // デバイスの初期化
        deviceHandle = hdInitDevice("Default Device");
        if (deviceHandle == 0)
        {
            Debug.LogError("Phantomデバイスの初期化に失敗しました。");
            return;
        }
        hdStartScheduler();
    }

    void Update()
    {
        if (deviceHandle == 0) return;

        // Phantomの現在位置を取得
        double[] position = new double[3];
        hdGetDoublev(HD_CURRENT_POSITION, position);

        // ボタン状態を取得
        int buttonState;
        hdGetIntegerv(HD_CURRENT_BUTTONS, out buttonState);

        // デバイス座標をUnityワールド座標に変換
        Vector3 devicePosition = new Vector3((float)position[0], (float)position[1], (float)position[2]);
        devicePosition = MapToUnityCoordinates(devicePosition);

        // ドラッグ処理
        HandleObjectInteraction(devicePosition, buttonState);
    }

    void HandleObjectInteraction(Vector3 devicePosition, int buttonState)
    {
        if ((buttonState & 1) != 0) // ボタンが押されている場合
        {
            if (selectedObject == null)
            {
                // オブジェクト選択
                RaycastHit hit;
                if (Physics.Raycast(devicePosition, Vector3.forward, out hit))
                {
                    selectedObject = hit.collider.gameObject;
                }
            }
            else
            {
                // 選択中のオブジェクトを移動
                selectedObject.transform.position = devicePosition;
            }
        }
        else
        {
            // ボタンが離されたら選択解除
            selectedObject = null;
        }
    }

    Vector3 MapToUnityCoordinates(Vector3 phantomPosition)
    {
        // Phantomデバイスの座標をUnityワールド座標にスケール変換
        float scaleFactor = 0.1f; // デバイス座標系のスケール調整
        return phantomPosition * scaleFactor;
    }

    void ApplyForce(Vector3 force)
    {
        // Phantomデバイスに力を設定
        double[] forceArray = { force.x, force.y, force.z };
        hdSetForce(forceArray);
    }

    void OnApplicationQuit()
    {
        hdStopScheduler();
        Debug.Log("Phantomデバイスのスケジューラを停止しました。");
    }
}
