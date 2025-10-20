using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using TMPro; // Required for TextMeshPro

public class VideoUIController : MonoBehaviour
{
    [Header("Video Player inside the panel")]
    public VideoPlayer videoPlayer;

    [Header("Slider UI")]
    public Slider videoSlider;

    [Header("Timer Text (TMP)")]
    public TextMeshProUGUI timeText;

    private bool isDragging = false;

    void Update()
    {
        // Auto-update slider only if not dragging
        if (videoPlayer.isPlaying && !isDragging)
        {
            videoSlider.minValue = 0f;
            videoSlider.maxValue = (float)videoPlayer.length;

            if (videoPlayer.length > 0)
            {
                videoSlider.value = (float)videoPlayer.time;
            }
        }

        // Update the timer text every frame
        if (timeText != null && videoPlayer.length > 0)
        {
            timeText.text = $"{FormatTime(videoPlayer.time)} / {FormatTime(videoPlayer.length)}";
        }
    }

    // Called when user presses down on slider handle
    public void OnPointerDown()
    {
        isDragging = true;
    }

    // Called continuously when user changes slider value
    public void OnValueChanged()
    {
        if (isDragging && videoPlayer != null)
        {
            videoPlayer.time = videoSlider.value;
        }
    }

    // Called when user releases slider handle
    public void OnPointerUp()
    {
        if (videoPlayer != null)
        {
            videoPlayer.time = videoSlider.value;
        }
        isDragging = false;
    }


    public void OnSliderClick(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;
        if (pointerData == null || videoPlayer == null || videoSlider == null)
            return;

        // Get the RectTransform of the slider's fill area
        RectTransform sliderRect = videoSlider.GetComponent<RectTransform>();

        // Convert click position to local position in slider space
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            sliderRect, pointerData.position, pointerData.pressEventCamera, out localPoint))
        {
            // Normalize the X position (0 to 1)
            float normalizedValue = Mathf.InverseLerp(
                -sliderRect.rect.width / 2f, sliderRect.rect.width / 2f, localPoint.x);

            // Clamp between 0 and 1 just in case
            normalizedValue = Mathf.Clamp01(normalizedValue);

            // Set slider and video time
            videoSlider.value = normalizedValue * (float)videoPlayer.length;
            videoPlayer.time = videoSlider.value;
        }
    }



    // Helper to format time as mm:ss
    private string FormatTime(double time)
    {
        int minutes = Mathf.FloorToInt((float)time / 60f);
        int seconds = Mathf.FloorToInt((float)time % 60f);
        return $"{minutes}:{seconds:00}";
    }
}
