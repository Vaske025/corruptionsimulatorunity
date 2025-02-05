using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacySystem : MonoBehaviour
{
    // UI References
    public Dropdown countryDropdown;
    public Button declareWarButton;
    public Button makePeaceButton;
    public Button formAllianceButton;
    public Button breakAllianceButton;
    public Button signTradeAgreementButton;
    public Button endTradeAgreementButton;
    public Button voteForUNResolutionButton;
    public Button imposeSanctionsButton;
    public Button liftSanctionsButton;
    public Button conductNuclearStrikeButton;
    public Button conductMilitaryInterventionButton;
    public Text diplomaticRelationsText;

    // List of countries
    public List<Country> countries;

    // Current country
    public Country currentCountry;

    // Diplomatic relations
    public Dictionary<Country, DiplomaticRelation> diplomaticRelations;

    // AI Strategy Types
    public enum AIStrategyType
    {
        Aggressive,
        Defensive,
        Neutral
    }

    void Start()
    {
        // Initialize countries
        countries = new List<Country>();
        InitializeCountries();

        // Initialize diplomatic relations
        diplomaticRelations = new Dictionary<Country, DiplomaticRelation>();
        InitializeDiplomaticRelations();

        // Setup UI elements
        SetupUI();
    }

    void SetupUI()
    {
        // Populate dropdown with countries
        List<string> countryNames = new List<string>();
        foreach (var country in countries)
        {
            if (country != currentCountry)
            {
                countryNames.Add(country.Name);
            }
        }
        countryDropdown.AddOptions(countryNames);

        // Add listeners for button clicks
        declareWarButton.onClick.AddListener(OnDeclareWarClick);
        makePeaceButton.onClick.AddListener(OnMakePeaceClick);
        formAllianceButton.onClick.AddListener(OnFormAllianceClick);
        breakAllianceButton.onClick.AddListener(OnBreakAllianceClick);
        signTradeAgreementButton.onClick.AddListener(OnSignTradeAgreementClick);
        endTradeAgreementButton.onClick.AddListener(OnEndTradeAgreementClick);
        voteForUNResolutionButton.onClick.AddListener(OnVoteForUNResolutionClick);
        imposeSanctionsButton.onClick.AddListener(OnImposeSanctionsClick);
        liftSanctionsButton.onClick.AddListener(OnLiftSanctionsClick);
        conductNuclearStrikeButton.onClick.AddListener(OnConductNuclearStrikeClick);
        conductMilitaryInterventionButton.onClick.AddListener(OnConductMilitaryInterventionClick);

        // Update UI text
        UpdateDiplomaticRelationsText();
    }

    void InitializeCountries()
    {
        // Example initialization of countries
        countries.Add(new Country { Name = "United States", Economy = 1000f, Military = 500, Stability = 0.8f, AIStrategy = AIStrategyType.Aggressive });
        countries.Add(new Country { Name = "Russia", Economy = 800f, Military = 400, Stability = 0.7f, AIStrategy = AIStrategyType.Defensive });
        countries.Add(new Country { Name = "China", Economy = 900f, Military = 450, Stability = 0.85f, AIStrategy = AIStrategyType.Neutral });
        countries.Add(new Country { Name = "Serbia", Economy = 200f, Military = 100, Stability = 0.6f, AIStrategy = AIStrategyType.Neutral });

        // Set current country
        currentCountry = countries.Find(c => c.Name == "Serbia");

        Debug.Log("Countries initialized:");
        foreach (var country in countries)
        {
            Debug.Log($"Name: {country.Name}, Economy: {country.Economy}, Military: {country.Military}, Stability: {country.Stability}, AI Strategy: {country.AIStrategy}");
        }
    }

    void InitializeDiplomaticRelations()
    {
        // Initialize diplomatic relations with default values
        foreach (var country in countries)
        {
            if (country != currentCountry)
            {
                diplomaticRelations[country] = new DiplomaticRelation { RelationType = DiplomaticRelationType.Neutral, Strength = 0.5f };
            }
        }

        Debug.Log("Diplomatic relations initialized:");
        foreach (var relation in diplomaticRelations)
        {
            Debug.Log($"Country: {relation.Key.Name}, Relation Type: {relation.Value.RelationType}, Strength: {relation.Value.Strength}");
        }
    }

    void OnDeclareWarClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            DeclareWar(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.DeclareWar);
        }
    }

    void OnMakePeaceClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            MakePeace(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.MakePeace);
        }
    }

    void OnFormAllianceClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            FormAlliance(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.FormAlliance);
        }
    }

    void OnBreakAllianceClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            BreakAlliance(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.BreakAlliance);
        }
    }

    void OnSignTradeAgreementClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            SignTradeAgreement(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.SignTradeAgreement);
        }
    }

    void OnEndTradeAgreementClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            EndTradeAgreement(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.EndTradeAgreement);
        }
    }

    void OnVoteForUNResolutionClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            VoteOnUNResolution(targetCountry, true);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.VoteForUNResolution);
        }
    }

    void OnImposeSanctionsClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            ImposeSanctions(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.ImposeSanctions);
        }
    }

    void OnLiftSanctionsClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            LiftSanctions(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.LiftSanctions);
        }
    }

    void OnConductNuclearStrikeClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            ConductNuclearStrike(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.ConductNuclearStrike);
        }
    }

    void OnConductMilitaryInterventionClick()
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            ConductMilitaryIntervention(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.ConductMilitaryIntervention);
        }
    }

    Country GetSelectedCountry()
    {
        int selectedIndex = countryDropdown.value;
        if (selectedIndex >= 0 && selectedIndex < countries.Count - 1)
        {
            return countries[selectedIndex + 1]; // +1 because current country is excluded
        }
        return null;
    }

    public void DeclareWar(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.War;
            diplomaticRelations[targetCountry].Strength = 0.0f;
            Debug.Log($"{currentCountry.Name} declared war on {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void MakePeace(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry) && diplomaticRelations[targetCountry].RelationType == DiplomaticRelationType.War)
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Neutral;
            diplomaticRelations[targetCountry].Strength = 0.5f;
            Debug.Log($"{currentCountry.Name} made peace with {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No war with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void FormAlliance(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Alliance;
            diplomaticRelations[targetCountry].Strength = 1.0f;
            Debug.Log($"{currentCountry.Name} formed an alliance with {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void BreakAlliance(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry) && diplomaticRelations[targetCountry].RelationType == DiplomaticRelationType.Alliance)
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Neutral;
            diplomaticRelations[targetCountry].Strength = 0.5f;
            Debug.Log($"{currentCountry.Name} broke the alliance with {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No alliance with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void SignTradeAgreement(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.TradeAgreement;
            diplomaticRelations[targetCountry].Strength = 0.8f;
            Debug.Log($"{currentCountry.Name} signed a trade agreement with {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void EndTradeAgreement(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry) && diplomaticRelations[targetCountry].RelationType == DiplomaticRelationType.TradeAgreement)
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Neutral;
            diplomaticRelations[targetCountry].Strength = 0.5f;
            Debug.Log($"{currentCountry.Name} ended the trade agreement with {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No trade agreement with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void VoteOnUNResolution(Country targetCountry, bool vote)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            Debug.Log($"{currentCountry.Name} voted {(vote ? "for" : "against")} a UN resolution involving {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
    }

    public void ImposeSanctions(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Sanctions;
            diplomaticRelations[targetCountry].Strength = 0.2f;
            Debug.Log($"{currentCountry.Name} imposed sanctions on {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void LiftSanctions(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry) && diplomaticRelations[targetCountry].RelationType == DiplomaticRelationType.Sanctions)
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Neutral;
            diplomaticRelations[targetCountry].Strength = 0.5f;
            Debug.Log($"{currentCountry.Name} lifted sanctions on {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No sanctions on {targetCountry.Name}");
            
        }
        UpdateDiplomaticRelationsText();
    }

    public void ConductNuclearStrike(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.War;
            diplomaticRelations[targetCountry].Strength = 0.0f;
            Debug.Log($"{currentCountry.Name} conducted a nuclear strike on {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void ConductMilitaryIntervention(Country targetCountry)
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.War;
            diplomaticRelations[targetCountry].Strength = 0.0f;
            Debug.Log($"{currentCountry.Name} conducted a military intervention in {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    void ReactToDiplomaticAction(Country targetCountry, DiplomaticAction action)
    {
        foreach (var country in countries)
        {
            if (country != currentCountry && country != targetCountry)
            {
                switch (country.AIStrategy)
                {
                    case AIStrategyType.Aggressive:
                        HandleAggressiveStrategy(country, targetCountry, action, true);
                        break;
                    case AIStrategyType.Defensive:
                        HandleDefensiveStrategy(country, targetCountry, action, true);
                        break;
                    case AIStrategyType.Neutral:
                        HandleNeutralStrategy(country, targetCountry, action, true);
                        break;
                }
            }
        }
    }

    void HandleAggressiveStrategy(Country country, Country targetCountry, DiplomaticAction action, bool vote)
    {
        switch (action)
        {
            case DiplomaticAction.DeclareWar:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s aggressive action.");
                }
                break;
            case DiplomaticAction.MakePeace:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.War)
                {
                    MakePeace(targetCountry);
                    Debug.Log($"{country.Name} made peace with {targetCountry.Name} due to {currentCountry.Name}'s peace-making action.");
                }
                break;
            case DiplomaticAction.FormAlliance:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    FormAlliance(targetCountry);
                    Debug.Log($"{country.Name} formed an alliance with {targetCountry.Name} due to {currentCountry.Name}'s alliance formation.");
                }
                break;
            case DiplomaticAction.BreakAlliance:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Alliance)
                {
                    BreakAlliance(targetCountry);
                    Debug.Log($"{country.Name} broke the alliance with {targetCountry.Name} due to {currentCountry.Name}'s alliance breaking.");
                }
                break;
            case DiplomaticAction.SignTradeAgreement:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    SignTradeAgreement(targetCountry);
                    Debug.Log($"{country.Name} signed a trade agreement with {targetCountry.Name} due to {currentCountry.Name}'s trade agreement signing.");
                }
                break;
            case DiplomaticAction.EndTradeAgreement:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.TradeAgreement)
                {
                    EndTradeAgreement(targetCountry);
                    Debug.Log($"{country.Name} ended the trade agreement with {targetCountry.Name} due to {currentCountry.Name}'s trade agreement ending.");
                }
                break;
            case DiplomaticAction.VoteForUNResolution:
                Debug.Log($"{country.Name} voted {(vote ? "for" : "against")} a UN resolution involving {targetCountry.Name} due to {currentCountry.Name}'s UN resolution voting.");
                break;
            case DiplomaticAction.ImposeSanctions:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    ImposeSanctions(targetCountry);
                    Debug.Log($"{country.Name} imposed sanctions on {targetCountry.Name} due to {currentCountry.Name}'s sanctions imposition.");
                }
                break;
            case DiplomaticAction.LiftSanctions:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Sanctions)
                {
                    LiftSanctions(targetCountry);
                    Debug.Log($"{country.Name} lifted sanctions on {targetCountry.Name} due to {currentCountry.Name}'s sanctions lifting.");
                }
                break;
            case DiplomaticAction.ConductNuclearStrike:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s nuclear strike.");
                }
                break;
            case DiplomaticAction.ConductMilitaryIntervention:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s military intervention.");
                }
                break;
        }
    }

    void HandleDefensiveStrategy(Country country, Country targetCountry, DiplomaticAction action, bool vote) 
    {
        switch (action)
        {
            case DiplomaticAction.DeclareWar:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    MakePeace(targetCountry);
                    Debug.Log($"{country.Name} made peace with {targetCountry.Name} due to {currentCountry.Name}'s aggressive action.");
                }
                break;
            case DiplomaticAction.MakePeace:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.War)
                {
                    MakePeace(targetCountry);
                    Debug.Log($"{country.Name} made peace with {targetCountry.Name} due to {currentCountry.Name}'s peace-making action.");
                }
                break;
            case DiplomaticAction.FormAlliance:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    FormAlliance(targetCountry);
                    Debug.Log($"{country.Name} formed an alliance with {targetCountry.Name} due to {currentCountry.Name}'s alliance formation.");
                }
                break;
            case DiplomaticAction.BreakAlliance:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Alliance)
                {
                    BreakAlliance(targetCountry);
                    Debug.Log($"{country.Name} broke the alliance with {targetCountry.Name} due to {currentCountry.Name}'s alliance breaking.");
                }
                break;
            case DiplomaticAction.SignTradeAgreement:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    SignTradeAgreement(targetCountry);
                    Debug.Log($"{country.Name} signed a trade agreement with {targetCountry.Name} due to {currentCountry.Name}'s trade agreement signing.");
                }
                break;
            case DiplomaticAction.EndTradeAgreement:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.TradeAgreement)
                {
                    EndTradeAgreement(targetCountry);
                    Debug.Log($"{country.Name} ended the trade agreement with {targetCountry.Name} due to {currentCountry.Name}'s trade agreement ending.");
                }
                break;
            case DiplomaticAction.VoteForUNResolution:
                Debug.Log($"{country.Name} voted {(vote ? "for" : "against")} a UN resolution involving {targetCountry.Name} due to {currentCountry.Name}'s UN resolution voting.");
                break;
            case DiplomaticAction.ImposeSanctions:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    ImposeSanctions(targetCountry);
                    Debug.Log($"{country.Name} imposed sanctions on {targetCountry.Name} due to {currentCountry.Name}'s sanctions imposition.");
                }
                break;
            case DiplomaticAction.LiftSanctions:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Sanctions)
                {
                    LiftSanctions(targetCountry);
                    Debug.Log($"{country.Name} lifted sanctions on {targetCountry.Name} due to {currentCountry.Name}'s sanctions lifting.");
                }
                break;
            case DiplomaticAction.ConductNuclearStrike:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s nuclear strike.");
                }
                break;
            case DiplomaticAction.ConductMilitaryIntervention:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s military intervention.");
                }
                break;
        }
    }

    void HandleNeutralStrategy(Country country, Country targetCountry, DiplomaticAction action, bool vote)
    {
        switch (action)
        {
            case DiplomaticAction.DeclareWar:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    MakePeace(targetCountry);
                    Debug.Log($"{country.Name} made peace with {targetCountry.Name} due to {currentCountry.Name}'s aggressive action.");
                }
                break;
            case DiplomaticAction.MakePeace:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.War)
                {
                    MakePeace(targetCountry);
                    Debug.Log($"{country.Name} made peace with {targetCountry.Name} due to {currentCountry.Name}'s peace-making action.");
                }
                break;
            case DiplomaticAction.FormAlliance:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    FormAlliance(targetCountry);
                    Debug.Log($"{country.Name} formed an alliance with {targetCountry.Name} due to {currentCountry.Name}'s alliance formation.");
                }
                break;
            case DiplomaticAction.BreakAlliance:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Alliance)
                {
                    BreakAlliance(targetCountry);
                    Debug.Log($"{country.Name} broke the alliance with {targetCountry.Name} due to {currentCountry.Name}'s action.");
                }
                break;
            case DiplomaticAction.SignTradeAgreement:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    SignTradeAgreement(targetCountry);
                    Debug.Log($"{country.Name} signed a trade agreement with {targetCountry.Name} due to {currentCountry.Name}'s action.");
                }
                break;
            case DiplomaticAction.EndTradeAgreement:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.TradeAgreement)
                {
                    EndTradeAgreement(targetCountry);
                    Debug.Log($"{country.Name} ended the trade agreement with {targetCountry.Name} due to {currentCountry.Name}'s action.");
                }
                break;
            case DiplomaticAction.VoteForUNResolution:
                Debug.Log($"{country.Name} voted {(vote ? "for" : "against")} a UN resolution involving {targetCountry.Name} due to {currentCountry.Name}'s action.");
                break;
            case DiplomaticAction.ImposeSanctions:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    ImposeSanctions(targetCountry);
                    Debug.Log($"{country.Name} imposed sanctions on {targetCountry.Name} due to {currentCountry.Name}'s action.");
                }
                break;
            case DiplomaticAction.LiftSanctions:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Sanctions)
                {
                    LiftSanctions(targetCountry);
                    Debug.Log($"{country.Name} lifted sanctions on {targetCountry.Name} due to {currentCountry.Name}'s action.");
                }
                break;
            case DiplomaticAction.ConductNuclearStrike:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s nuclear strike.");
                }
                break;
            case DiplomaticAction.ConductMilitaryIntervention:
                if (diplomaticRelations[country].RelationType == DiplomaticRelationType.Neutral)
                {
                    DeclareWar(targetCountry);
                    Debug.Log($"{country.Name} declared war on {targetCountry.Name} due to {currentCountry.Name}'s military intervention.");
                }
                break;
        }
    }

    void UpdateDiplomaticRelationsText()
    {
        diplomaticRelationsText.text = "Diplomatic Relations:\n";
        foreach (var relation in diplomaticRelations)
        {
            diplomaticRelationsText.text += $"{relation.Key.Name}: {relation.Value.RelationType} ({relation.Value.Strength:F2})\n";
        }
    }

    [System.Serializable]
    public class Country
    {
        public string Name;
        public float Economy;
        public float Military;
        public float Stability;
        public AIStrategyType AIStrategy;
    }

    [System.Serializable]
    public class DiplomaticRelation
    {
        public DiplomaticRelationType RelationType;
        public float Strength;
    }

    public enum DiplomaticRelationType
    {
        Neutral,
        War,
        Alliance,
        TradeAgreement,
        Sanctions
    }

    public enum DiplomaticAction
    {
        DeclareWar,
        MakePeace,
        FormAlliance,
        BreakAlliance,
        SignTradeAgreement,
        EndTradeAgreement,
        VoteForUNResolution,
        ImposeSanctions,
        LiftSanctions,
        ConductNuclearStrike,
        ConductMilitaryIntervention
    }
}