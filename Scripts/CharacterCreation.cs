using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    // UI References
    public Dropdown genderDropdown;
    public Dropdown raceDropdown;
    public Dropdown hairstyleDropdown;
    public Dropdown clothingDropdown;
    public Dropdown countryDropdown;
    public Dropdown personalityDropdown;
    public Button createButton;
    public Text errorText;
    public GameObject leaderModel; // Reference to the leader model GameObject

    // Player Data
    private string selectedGender;
    private string selectedRace;
    private string selectedHairstyle;
    private string selectedClothing;
    private string selectedCountry;
    private string selectedPersonality;

    // List of available options
    private List<string> genders = new List<string> { "Male", "Female" };
    private List<string> races = new List<string> { "Caucasian", "Asian", "African" };
    private List<string> hairstyles = new List<string> { "Short Hair", "Long Hair", "Bald" };
    private List<string> clothing = new List<string> { "Suit", "Casual", "Military Uniform" };
    private List<string> countries = new List<string> { "United States", "Russia", "China", "Serbia" };
    private List<string> personalities = new List<string> { "Visionary", "Manipulator", "Populist", "Despot" };

    // Dictionary for leader models
    private Dictionary<string, GameObject> leaderModels = new Dictionary<string, GameObject>();

    // Randomly generated properties if not manually selected
    private string randomGender;
    private string randomRace;
    private string randomHairstyle;
    private string randomClothing;
    private string randomCountry;
    private string randomPersonality;

    void Start()
    {
        // Populate dropdowns with options
        genderDropdown.AddOptions(genders);
        raceDropdown.AddOptions(races);
        hairstyleDropdown.AddOptions(hairstyles);
        clothingDropdown.AddOptions(clothing);
        countryDropdown.AddOptions(countries);
        personalityDropdown.AddOptions(personalities);

        // Add listeners for dropdown changes
        genderDropdown.onValueChanged.AddListener(OnGenderChanged);
        raceDropdown.onValueChanged.AddListener(OnRaceChanged);
        hairstyleDropdown.onValueChanged.AddListener(OnHairstyleChanged);
        clothingDropdown.onValueChanged.AddListener(OnClothingChanged);
        countryDropdown.onValueChanged.AddListener(OnCountryChanged);
        personalityDropdown.onValueChanged.AddListener(OnPersonalityChanged);

        // Add listener for button click
        createButton.onClick.AddListener(OnCreateButtonClick);

        // Initialize error text
        errorText.text = "";

        // Generate random properties
        GenerateRandomProperties();

        // Load leader models
        LoadLeaderModels();
    }

    void LoadLeaderModels()
    {
        // Load leader models from resources or prefabs
        leaderModels["Male_Caucasian_ShortHair_Suit"] = Resources.Load<GameObject>("Prefabs/Leaders/Male_Caucasian_ShortHair_Suit");
        leaderModels["Female_Asian_LongHair_Casual"] = Resources.Load<GameObject>("Prefabs/Leaders/Female_Asian_LongHair_Casual");
        // Add more models as needed
    }

    void UpdateLeaderModel()
    {
        // Destroy current leader model if exists
        if (leaderModel != null)
        {
            Destroy(leaderModel);
        }

        // Create new leader model based on selected options
        string modelKey = selectedGender + "_" + selectedRace + "_" + selectedHairstyle + "_" + selectedClothing;
        if (leaderModels.ContainsKey(modelKey))
        {
            leaderModel = Instantiate(leaderModels[modelKey], transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Model not found for key: " + modelKey);
        }
    }

    void GenerateRandomProperties()
    {
        // Generate random properties for the leader
        randomGender = genders[Random.Range(0, genders.Count)];
        randomRace = races[Random.Range(0, races.Count)];
        randomHairstyle = hairstyles[Random.Range(0, hairstyles.Count)];
        randomClothing = clothing[Random.Range(0, clothing.Count)];
        randomCountry = countries[Random.Range(0, countries.Count)];
        randomPersonality = personalities[Random.Range(0, personalities.Count)];

        Debug.Log("Randomly Generated Leader Properties:");
        Debug.Log("Gender: " + randomGender);
        Debug.Log("Race: " + randomRace);
        Debug.Log("Hairstyle: " + randomHairstyle);
        Debug.Log("Clothing: " + randomClothing);
        Debug.Log("Country: " + randomCountry);
        Debug.Log("Personality: " + randomPersonality);
    }

    void OnGenderChanged(int index)
    {
        // Update selected gender
        selectedGender = genders[index];
        Debug.Log("Selected Gender: " + selectedGender);
        UpdateLeaderModel();
    }

    void OnRaceChanged(int index)
    {
        // Update selected race
        selectedRace = races[index];
        Debug.Log("Selected Race: " + selectedRace);
        UpdateLeaderModel();
    }

    void OnHairstyleChanged(int index)
    {
        // Update selected hairstyle
        selectedHairstyle = hairstyles[index];
        Debug.Log("Selected Hairstyle: " + selectedHairstyle);
        UpdateLeaderModel();
    }

    void OnClothingChanged(int index)
    {
        // Update selected clothing
        selectedClothing = clothing[index];
        Debug.Log("Selected Clothing: " + selectedClothing);
        UpdateLeaderModel();
    }

    void OnCountryChanged(int index)
    {
        // Update selected country
        selectedCountry = countries[index];
        Debug.Log("Selected Country: " + selectedCountry);
    }

    void OnPersonalityChanged(int index)
    {
        // Update selected personality
        selectedPersonality = personalities[index];
        Debug.Log("Selected Personality: " + selectedPersonality);
    }

    void OnCreateButtonClick()
    {
        // Validate if all options are selected
        if (ValidateSelection())
        {
            CreateLeader();
            errorText.text = "Leader created successfully!";
        }
        else
        {
            // Use randomly generated properties if no manual selection
            selectedGender = randomGender;
            selectedRace = randomRace;
            selectedHairstyle = randomHairstyle;
            selectedClothing = randomClothing;
            selectedCountry = randomCountry;
            selectedPersonality = randomPersonality;

            CreateLeader();
            errorText.text = "Leader created with random properties!";
        }
    }

    bool ValidateSelection()
    {
        // Check if all options are selected
        return !string.IsNullOrEmpty(selectedGender) &&
               !string.IsNullOrEmpty(selectedRace) &&
               !string.IsNullOrEmpty(selectedHairstyle) &&
               !string.IsNullOrEmpty(selectedClothing) &&
               !string.IsNullOrEmpty(selectedCountry) &&
               !string.IsNullOrEmpty(selectedPersonality);
    }

    void CreateLeader()
    {
        // Placeholder for leader creation logic
        Debug.Log("Creating leader with:");
        Debug.Log("Gender: " + selectedGender);
        Debug.Log("Race: " + selectedRace);
        Debug.Log("Hairstyle: " + selectedHairstyle);
        Debug.Log("Clothing: " + selectedClothing);
        Debug.Log("Country: " + selectedCountry);
        Debug.Log("Personality: " + selectedPersonality);

        // Here you can add code to instantiate the leader character with the selected attributes
    }
}