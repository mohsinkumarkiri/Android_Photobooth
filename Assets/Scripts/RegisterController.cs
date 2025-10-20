using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text.RegularExpressions;

public class RegisterController : MonoBehaviour
{

    public ScreenController screenController;

    public TMP_InputField nameInput;
    public TMP_InputField emailInput;
    public TMP_InputField phoneInput;

    [SerializeField] private TextMeshProUGUI feedbackStatus;    //To get resgistration status (debug text)

    [SerializeField] private string filePath;

    // Start is called before the first frame update
    void Start()
    {
        // Set the directory and file path
        string directoryPath = Path.Combine(Application.persistentDataPath).Replace("data", "media");
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
            Debug.Log("Created directory: " + directoryPath);

            feedbackStatus.text = "Created directory: " + directoryPath;
        }

        filePath = Path.Combine(directoryPath, "UserData.csv");

        if (!File.Exists(filePath))
        {
            string header = "Name,Email,Phone";
            File.WriteAllText(filePath, header + "\n");
            Debug.Log("Created file with header: " + filePath);

            feedbackStatus.text = "Created file at: " + filePath;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void saveUserData()
    {
        // Assign input values to respective text fields
        string name = nameInput.text.Trim();
        string email = emailInput.text.Trim();
        string phone = phoneInput.text.Trim();

        // String Validations
        if (!ValidateName(name))
        {
            ShowFeedback("Invalid name. Only letters and spaces allowed.");
            return;
        }

        if (!ValidateEmail(email))
        {
            ShowFeedback("Invalid email format.");
            return;
        }

        if (!ValidatePhone(phone))
        {
            ShowFeedback("Invalid phone number. Use digits and optional leading '+'.");
            return;
        }

        // Save User Data to CSV
        string newLine = $"{EscapeCSV(name)},{EscapeCSV(email)},{EscapeCSV(phone)}";
        File.AppendAllText(filePath, newLine + "\n");
        //PlayerPrefs.SetString("mailTo", email);
        Debug.Log("Data saved to: " + filePath);

        //Debug the result
        feedbackStatus.text = "Data saved to: " + filePath;
        ShowFeedback("Data saved successfully!");
        clearFields();

        //Skip the form 
        screenController.isRegisterationSkipped = false;

        // Get to next screem
        screenController.setScreen(3);
        
    }

    private bool ValidateName(string name)
    {
        return Regex.IsMatch(name, @"^[A-Za-z\s]+$");
    }

    private bool ValidateEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private bool ValidatePhone(string phone)
    {
        return Regex.IsMatch(phone, @"^\+?[0-9]+$");
    }

    private void ShowFeedback(string message)
    {
        if (feedbackStatus != null)
        {
            feedbackStatus.text = message;
        }
        Debug.Log(message);
    }


    // Clear InputFields 
    private void clearFields()
    {
        nameInput.text = "";
        emailInput.text = "";
        phoneInput.text = "";
        feedbackStatus.text = "";
    }

    private string EscapeCSV(string value)
    {
        if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }
        return value;
    }
}
