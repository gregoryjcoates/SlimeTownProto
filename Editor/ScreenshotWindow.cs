using UnityEngine;
using UnityEditor;
using System;

public class ScreenshotWindow : EditorWindow
{
    [MenuItem("Window/ScreenshotWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ScreenshotWindow>();
    }

    bool CanPushScreenshotButton = true;
    string folderPath =System.IO.Directory.GetCurrentDirectory() + "/ProjectScreenShots/";
    string c = "empty";
    private void OnGUI()
    {
        if (CanPushScreenshotButton == true)
        {
            if (GUILayout.Button("Take Screenshot") == true)
            {
                CanPushScreenshotButton = false;
                if (System.IO.Directory.Exists(folderPath) == false)
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }



                Debug.Log("button was pressed");
                DateTime a = DateTime.Now;
                string b = Application.productName + a.ToString("yyyyMMddHHmmss") + ".png";
                Debug.Log(b);
                ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, b));
                c = b;
            }
        }

    }

    private void Update()
    {
        if (System.IO.File.Exists(System.IO.Path.Combine(folderPath, c)) == true)
        {
            CanPushScreenshotButton = true;
        }

    }
}
