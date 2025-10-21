using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Config: MonoBehaviour
{
    // URL to be written to the file
    private string url = "https://victor-locale-returning-crm.trycloudflare.com/upload-";
    [HideInInspector]
    public string uploadLink;
    // File name
    private string fileName = "UploadURL.txt";

    void Start()
    {
        CreateFileWithUrl();
    }

    void CreateFileWithUrl()
    {
        // Build the path
        string directoryPath = Path.Combine(Application.persistentDataPath).Replace("data", "media");

        // Ensure the directory exists
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Debug.Log("Created directory: " + directoryPath);
        }

        if (Directory.Exists(directoryPath))
        {
            Debug.Log("directory is set at : " + directoryPath);
        }

        // Full path to the txt file
        string filePath = Path.Combine(directoryPath, fileName);

        // Create the file if it does not exist
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, url);
            Debug.Log("Created file and wrote URL: " + filePath);
            uploadLink = url;
        }
        else
        {
            Debug.Log("File already exists: " + filePath);
            uploadLink = File.ReadAllText(filePath);
        }
    }
}
