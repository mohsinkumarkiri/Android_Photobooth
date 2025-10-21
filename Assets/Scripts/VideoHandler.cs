using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using RenderHeads.Media.AVProMovieCapture;
using System.Net.Http.Headers;
using System;
using System.Threading.Tasks;

public class VideoHandler : MonoBehaviour
{
    public Config config;
    public ScreenController screenController;
    private string _filePath;
    public VideoPlayer previewVideoPlayer;
    public CaptureFromScreen captureFromScreen;

    private string responseFileName = "";
    private Queue<string> uploadQueue = new Queue<string>();
    public bool isUploading = false;
    // Start is called before the first frame update


    public void playCapturedVideo()
    {
        previewVideoPlayer.url = getLastCapturedVideoPath();
        screenController.setScreen(5);
        previewVideoPlayer.Play();
    }

    private string getLastCapturedVideoPath()
    {
        return captureFromScreen.LastFilePath;
    }

    public void AddToUploadQueue()
    {
        previewVideoPlayer.Stop();
        string videoPath = getLastCapturedVideoPath();

        if (File.Exists(videoPath))
        {
            uploadQueue.Enqueue(videoPath);
            if (!isUploading)
            {
                ProcessUploadQueue(); // now async void
            }
        }
        else
        {
            Debug.LogWarning("Cannot upload, video file not found at: " + videoPath);
        }
    }


    private async void ProcessUploadQueue()
    {
        isUploading = true;

        while (uploadQueue.Count > 0)
        {
            string filepath = uploadQueue.Dequeue();
            _filePath = filepath; // set the correct path
            await Main();
        }

        isUploading = false;
    }
    private static readonly HttpClient client = new HttpClient();

    public async Task Main()
    {
        // string url = config.uploadLink + "video";
        string url = "https://victor-locale-returning-crm.trycloudflare.com/upload-video";
        await UploadVideo(url, _filePath);
    }

    public async Task UploadVideo(string url, string filePath)
    {
        Debug.Log("Upload Video Called");
        string mailId = PlayerPrefs.GetString("mailTo");
        try
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Debug.Log("File not found: " + filePath);
                return;
            }

            // Read the file into a stream
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Create the multipart content
            var formData = new MultipartFormDataContent();

            // Add the file content
            var fileContent = new StreamContent(fileStream);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("video/mp4");
            formData.Add(fileContent, "video", Path.GetFileName(filePath));
            formData.Add(new StringContent(mailId), "mailTo");
            // Send the POST request
            HttpResponseMessage response = await client.PostAsync(url, formData);

            // Handle the response
            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
                responseFileName = contents;

                if (!string.IsNullOrEmpty(responseFileName))
                {
                    Debug.Log("Video " + responseFileName + " mailed successfully to " + mailId);
                }
            }
            else
            {
                Debug.Log($"Failed to upload video. Status code: {response.StatusCode} to " + mailId);
                string error = await response.Content.ReadAsStringAsync();
                Debug.Log($"Error: {error}");
            }
        }
        catch (Exception ex)
        {
            Debug.Log("An error occurred: " + ex.Message);
        }
    }
}
