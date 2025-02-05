using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropagandaSystem : MonoBehaviour
{
    // UI References
    public Dropdown mediaTypeDropdown;
    public InputField scandalInput;
    public Button createScandalButton;
    public Button censorInternetButton;
    public Button blockSourcesButton;
    public Button releaseMediaTypeButton; // New button for releasing media
    public Text propagandaStatusText;
    public Toggle televisionToggle;
    public Toggle socialMediaToggle;
    public Toggle newspapersToggle;
    public AudioSource audioSource; // Audio source for UI sounds

    // Media Types
    public List<string> mediaTypes = new List<string> { "Television", "Social Media", "Newspapers" };

    // Propaganda Status
    public bool televisionControlled;
    public bool socialMediaControlled;
    public bool newspapersControlled;
    public bool internetCensored;
    public bool sourcesBlocked;

    // UI Sounds
    public AudioClip uiClickSound;

    void Start()
    {
        // Populate dropdown with media types
        mediaTypeDropdown.AddOptions(mediaTypes);

        // Add listeners for button clicks
        createScandalButton.onClick.AddListener(OnCreateScandalClick);
        censorInternetButton.onClick.AddListener(OnCensorInternetClick);
        blockSourcesButton.onClick.AddListener(OnBlockSourcesClick);
        releaseMediaTypeButton.onClick.AddListener(OnReleaseMediaTypeClick); // Add listener for release button

        // Add listeners for toggle changes
        televisionToggle.onValueChanged.AddListener(OnTelevisionToggleChanged);
        socialMediaToggle.onValueChanged.AddListener(OnSocialMediaToggleChanged);
        newspapersToggle.onValueChanged.AddListener(OnNewspapersToggleChanged);

        // Initialize propaganda status text
        UpdatePropagandaStatusText();
    }

    void OnCreateScandalClick()
    {
        PlayUISound();
        string scandal = scandalInput.text;
        if (!string.IsNullOrEmpty(scandal))
        {
            CreateScandal(scandal);
            scandalInput.text = ""; // Clear input field after creating scandal
        }
        else
        {
            Debug.LogWarning("Scandal input is empty!");
        }
    }

    void OnCensorInternetClick()
    {
        PlayUISound();
        ToggleInternetCensorship();
    }

    void OnBlockSourcesClick()
    {
        PlayUISound();
        ToggleSourceBlocking();
    }

    void OnReleaseMediaTypeClick()
    {
        PlayUISound();
        string mediaType = mediaTypeDropdown.options[mediaTypeDropdown.value].text;
        ReleaseMediaType(mediaType);
    }

    void OnTelevisionToggleChanged(bool isOn)
    {
        PlayUISound();
        if (isOn)
        {
            ControlMediaType("Television");
        }
        else
        {
            ReleaseMediaType("Television");
        }
    }

    void OnSocialMediaToggleChanged(bool isOn)
    {
        PlayUISound();
        if (isOn)
        {
            ControlMediaType("Social Media");
        }
        else
        {
            ReleaseMediaType("Social Media");
        }
    }

    void OnNewspapersToggleChanged(bool isOn)
    {
        PlayUISound();
        if (isOn)
        {
            ControlMediaType("Newspapers");
        }
        else
        {
            ReleaseMediaType("Newspapers");
        }
    }

    void CreateScandal(string scandal)
    {
        Debug.Log($"Scandal created: {scandal}");
        // Here you can add logic to handle the scandal creation
    }

    void ToggleInternetCensorship()
    {
        internetCensored = !internetCensored;
        Debug.Log($"Internet censorship toggled to: {internetCensored}");
        UpdatePropagandaStatusText();
    }

    void ToggleSourceBlocking()
    {
        sourcesBlocked = !sourcesBlocked;
        Debug.Log($"Source blocking toggled to: {sourcesBlocked}");
        UpdatePropagandaStatusText();
    }

    void UpdatePropagandaStatusText()
    {
        propagandaStatusText.text = "Propaganda Status:\n";
        propagandaStatusText.text += $"Television Controlled: {televisionControlled}\n";
        propagandaStatusText.text += $"Social Media Controlled: {socialMediaControlled}\n";
        propagandaStatusText.text += $"Newspapers Controlled: {newspapersControlled}\n";
        propagandaStatusText.text += $"Internet Censored: {internetCensored}\n";
        propagandaStatusText.text += $"Sources Blocked: {sourcesBlocked}\n";
    }

    public void ControlMediaType(string mediaType)
    {
        switch (mediaType)
        {
            case "Television":
                televisionControlled = true;
                televisionToggle.isOn = true;
                break;
            case "Social Media":
                socialMediaControlled = true;
                socialMediaToggle.isOn = true;
                break;
            case "Newspapers":
                newspapersControlled = true;
                newspapersToggle.isOn = true;
                break;
            default:
                Debug.LogWarning($"Unknown media type: {mediaType}");
                break;
        }
        Debug.Log($"Controlled media type: {mediaType}");
        UpdatePropagandaStatusText();
    }

    public void ReleaseMediaType(string mediaType)
    {
        switch (mediaType)
        {
            case "Television":
                televisionControlled = false;
                televisionToggle.isOn = false;
                break;
            case "Social Media":
                socialMediaControlled = false;
                socialMediaToggle.isOn = false;
                break;
            case "Newspapers":
                newspapersControlled = false;
                newspapersToggle.isOn = false;
                break;
            default:
                Debug.LogWarning($"Unknown media type: {mediaType}");
                break;
        }
        Debug.Log($"Released media type: {mediaType}");
        UpdatePropagandaStatusText();
    }

    void PlayUISound()
    {
        if (audioSource != null && uiClickSound != null)
        {
            audioSource.PlayOneShot(uiClickSound);
        }
    }
}