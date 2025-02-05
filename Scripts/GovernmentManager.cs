using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GovernmentManager : MonoBehaviour
{
    // UI References
    public Slider taxRateSlider;
    public Slider publicSectorSlider;
    public Slider blackMarketSlider;
    public InputField policeForceInput;
    public InputField armyForceInput;
    public Text budgetText;
    public Text ministerLoyaltyText;
    public Text ministerMoraleText;

    // Minister Lists
    public List<Minister> ministers;

    // Economic Variables
    public float budget;
    public float taxRate;
    public float publicSector;
    public float blackMarket;

    // Military Variables
    public int policeForce;
    public int armyForce;

    // Loyalty and Morale Variables
    public float ministerLoyalty;
    public float ministerMorale;

    void Start()
    {
        // Initialize ministers
        ministers = new List<Minister>();
        InitializeMinisters();

        // Set initial economic values
        budget = 1000f;
        taxRate = 0.2f;
        publicSector = 0.6f;
        blackMarket = 0.1f;

        // Set initial military values
        policeForce = 100;
        armyForce = 200;

        // Set initial loyalty and morale values
        ministerLoyalty = 0.8f;
        ministerMorale = 0.7f;

        // Setup UI elements
        SetupUI();
    }

    void SetupUI()
    {
        // Initialize sliders and input fields
        taxRateSlider.value = taxRate;
        publicSectorSlider.value = publicSector;
        blackMarketSlider.value = blackMarket;
        policeForceInput.text = policeForce.ToString();
        armyForceInput.text = armyForce.ToString();

        // Add listeners for UI changes
        taxRateSlider.onValueChanged.AddListener(OnTaxRateChanged);
        publicSectorSlider.onValueChanged.AddListener(OnPublicSectorChanged);
        blackMarketSlider.onValueChanged.AddListener(OnBlackMarketChanged);
        policeForceInput.onEndEdit.AddListener(OnPoliceForceChanged);
        armyForceInput.onEndEdit.AddListener(OnArmyForceChanged);

        // Update UI text
        UpdateUIText();
    }

    void InitializeMinisters()
    {
        // Example initialization of ministers
        ministers.Add(new Minister { Name = "John Doe", Position = "Minister of Defense", Loyalty = 0.9f, Morale = 0.8f });
        ministers.Add(new Minister { Name = "Jane Smith", Position = "Minister of Finance", Loyalty = 0.8f, Morale = 0.7f });
        ministers.Add(new Minister { Name = "Alice Johnson", Position = "Minister of Foreign Affairs", Loyalty = 0.7f, Morale = 0.6f });

        Debug.Log("Ministers initialized:");
        foreach (var minister in ministers)
        {
            Debug.Log($"Name: {minister.Name}, Position: {minister.Position}, Loyalty: {minister.Loyalty}, Morale: {minister.Morale}");
        }
    }

    void OnTaxRateChanged(float rate)
    {
        UpdateTaxRate(rate);
        UpdateBudget(-100f); // Example budget change
    }

    void OnPublicSectorChanged(float sector)
    {
        UpdatePublicSector(sector);
        UpdateBudget(-50f); // Example budget change
    }

    void OnBlackMarketChanged(float market)
    {
        UpdateBlackMarket(market);
        UpdateBudget(50f); // Example budget change
    }

    void OnPoliceForceChanged(string input)
    {
        if (int.TryParse(input, out int force))
        {
            UpdatePoliceForce(force);
        }
        else
        {
            Debug.LogWarning("Invalid input for Police Force");
        }
    }

    void OnArmyForceChanged(string input)
    {
        if (int.TryParse(input, out int force))
        {
            UpdateArmyForce(force);
        }
        else
        {
            Debug.LogWarning("Invalid input for Army Force");
        }
    }

    public void HireMinister(string name, string position, float loyalty, float morale)
    {
        ministers.Add(new Minister { Name = name, Position = position, Loyalty = loyalty, Morale = morale });
        Debug.Log($"Minister {name} hired as {position} with Loyalty: {loyalty} and Morale: {morale}");
    }

    public void FireMinister(Minister minister)
    {
        ministers.Remove(minister);
        Debug.Log($"Minister {minister.Name} fired from {minister.Position}");
    }

    public void UpdateBudget(float amount)
    {
        budget += amount;
        Debug.Log($"Budget updated to: {budget}");
        UpdateUIText();
    }

    public void UpdateTaxRate(float rate)
    {
        taxRate = rate;
        Debug.Log($"Tax Rate updated to: {taxRate}");
        UpdateUIText();
    }

    public void UpdatePublicSector(float sector)
    {
        publicSector = sector;
        Debug.Log($"Public Sector updated to: {publicSector}");
        UpdateUIText();
    }

    public void UpdateBlackMarket(float market)
    {
        blackMarket = market;
        Debug.Log($"Black Market updated to: {blackMarket}");
        UpdateUIText();
    }

    public void UpdatePoliceForce(int force)
    {
        policeForce = force;
        Debug.Log($"Police Force updated to: {policeForce}");
        UpdateUIText();
    }

    public void UpdateArmyForce(int force)
    {
        armyForce = force;
        Debug.Log($"Army Force updated to: {armyForce}");
        UpdateUIText();
    }

    public void UpdateMinisterLoyalty(float loyalty)
    {
        ministerLoyalty = loyalty;
        Debug.Log($"Minister Loyalty updated to: {ministerLoyalty}");
        UpdateUIText();
    }

    public void UpdateMinisterMorale(float morale)
    {
        ministerMorale = morale;
        Debug.Log($"Minister Morale updated to: {ministerMorale}");
        UpdateUIText();
    }

    void UpdateUIText()
    {
        budgetText.text = $"Budget: ${budget:F2}";
        ministerLoyaltyText.text = $"Minister Loyalty: {ministerLoyalty:P2}";
        ministerMoraleText.text = $"Minister Morale: {ministerMorale:P2}";
    }

    [System.Serializable]
    public class Minister
    {
        public string Name;
        public string Position;
        public float Loyalty;
        public float Morale;
    }
}