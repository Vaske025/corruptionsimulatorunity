using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacySystem : MonoBehaviour
{
    // UI References
    public Dropdown countryDropdown;
    public Button declareWarButton;
    public Button makePeaceButton;
    public Button negotiatePeaceTermsButton; // Nova dugmad
    public Button humanitarianInterventionButton;
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
        negotiatePeaceTermsButton.onClick.AddListener(OnNegotiatePeaceTermsClick); // Novo dugme
        humanitarianInterventionButton.onClick.AddListener(OnHumanitarianInterventionClick); // Novo dugme
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

    void OnNegotiatePeaceTermsClick() // Nova funkcija
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            NegotiatePeaceTerms(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.NegotiatePeaceTerms);
        }
    }

    void OnHumanitarianInterventionClick() // Nova funkcija
    {
        Country targetCountry = GetSelectedCountry();
        if (targetCountry != null)
        {
            HumanitarianIntervention(targetCountry);
            ReactToDiplomaticAction(targetCountry, DiplomaticAction.HumanitarianIntervention);
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

    public void NegotiatePeaceTerms(Country targetCountry) // Nova funkcija
    {
        if (diplomaticRelations.ContainsKey(targetCountry) && diplomaticRelations[targetCountry].RelationType == DiplomaticRelationType.War)
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Neutral;
            diplomaticRelations[targetCountry].Strength = 0.6f; // Malo bolji odnos nakon pregovora
            Debug.Log($"{currentCountry.Name} negotiated peace terms with {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No war with {targetCountry.Name}");
        }
        UpdateDiplomaticRelationsText();
    }

    public void HumanitarianIntervention(Country targetCountry) // Nova funkcija
    {
        if (diplomaticRelations.ContainsKey(targetCountry))
        {
            diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.Neutral;
            diplomaticRelations[targetCountry].Strength = 0.7f; // Pozitivan uticaj na odnose
            Debug.Log($"{currentCountry.Name} conducted a humanitarian intervention in {targetCountry.Name}");
        }
        else
        {
            Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
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
        // Postavljanje vrste odnosa na "Rat" i smanjenje jačine odnosa na 0.0f
        diplomaticRelations[targetCountry].RelationType = DiplomaticRelationType.War;
        diplomaticRelations[targetCountry].Strength = 0.0f;

        // Logovanje akcije u konzoli
        Debug.Log($"{currentCountry.Name} conducted a military intervention in {targetCountry.Name}");

        // Ažuriranje UI teksta za diplomatske odnose
        UpdateDiplomaticRelationsText();

        // Dodatni efekti na ekonomiju, stabilnost i medije
        AffectEconomyOnMilitaryIntervention();
        AffectStabilityOnMilitaryIntervention();
        AffectMediaOnMilitaryIntervention();
    }
    else
    {
        // Upozorenje ako nema diplomatskih odnosa sa ciljnom zemljom
        Debug.LogWarning($"No diplomatic relation with {targetCountry.Name}");
    }
}

// Metoda koja utiče na ekonomiju prilikom vojne intervencije
void AffectEconomyOnMilitaryIntervention()
{
    if (economyManager != null)
    {
        // Smanjenje budžeta zbog vojne intervencije
        economyManager.UpdateBudget(-500f);

        // Smanjenje javnog sektora zbog preusmeravanja resursa
        economyManager.UpdatePublicSector(economyManager.publicSector - 0.1f);

        // Logovanje promena u ekonomiji
        Debug.Log("Economy affected due to military intervention. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
    }
}

// Metoda koja utiče na stabilnost prilikom vojne intervencije
void AffectStabilityOnMilitaryIntervention()
{
    // Smanjenje stabilnosti u trenutnoj zemlji
    stabilityLevel -= 0.1f;

    // Logovanje promene stabilnosti
    Debug.Log("Stability level decreased due to military intervention. Stability Level: " + stabilityLevel);
}

// Metoda koja utiče na medije prilikom vojne intervencije
void AffectMediaOnMilitaryIntervention()
{
    if (propagandaSystem != null)
    {
        // Smanjenje medijskog uticaja zbog negativnog javnog mnjenja
        mediaInfluence -= 0.1f;

        // Logovanje promene medijskog uticaja
        Debug.Log("Media influence reduced due to military intervention. Media Influence: " + mediaInfluence);
    }
}
    void AffectEconomyOnMilitaryIntervention()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-700f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.15f);
            Debug.Log("Economy affected due to military intervention. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }
        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.15f;
            Debug.Log("Media influence reduced due to military intervention. Media Influence: " + mediaInfluence);
        }
        // Affect international reputation
        internationalReputation -= 0.2f;
        Debug.Log("International reputation reduced due to military intervention. International Reputation: " + internationalReputation);
    }
    #endregion
    #region Economy Actions
    void OnTaxRateChanged(float value)
    {
        if (economyManager != null)
        {
            economyManager.UpdateTaxRate(value);
            Debug.Log("Tax rate changed to: " + value);
            UpdateUIText();
        }
    }
    void OnPublicSectorChanged(float value)
    {
        if (economyManager != null)
        {
            economyManager.UpdatePublicSector(value);
            Debug.Log("Public sector changed to: " + value);
            UpdateUIText();
        }
    }
    void OnBlackMarketChanged(float value)
    {
        if (economyManager != null)
        {
            economyManager.UpdateBlackMarket(value);
            Debug.Log("Black market changed to: " + value);
            UpdateUIText();
        }
    }
    void OnPoliceForceChanged(string input)
    {
        if (int.TryParse(input, out int force))
        {
            if (force > governmentManager.policeForce)
            {
                rebellionLevel -= 0.05f;
                stabilityLevel += 0.05f;
                Debug.Log("Stability level increased due to increased police force. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
            }
            else if (force < governmentManager.policeForce)
            {
                rebellionLevel += 0.05f;
                stabilityLevel -= 0.05f;
                Debug.Log("Rebellion level increased due to decreased police force. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
            }
            governmentManager.UpdatePoliceForce(force);
        }
        else
        {
            Debug.LogWarning("Invalid input for Police Force");
        }
        UpdateUIText();
    }
    void OnArmyForceChanged(string input)
    {
        if (int.TryParse(input, out int force))
        {
            if (force > governmentManager.armyForce)
            {
                rebellionLevel += 0.1f;
                stabilityLevel -= 0.1f;
                Debug.Log("Rebellion level increased due to increased army force. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
            }
            else if (force < governmentManager.armyForce)
            {
                rebellionLevel -= 0.1f;
                stabilityLevel += 0.1f;
                Debug.Log("Stability level increased due to decreased army force. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
            }
            governmentManager.UpdateArmyForce(force);
        }
        else
        {
            Debug.LogWarning("Invalid input for Army Force");
        }
        UpdateUIText();
    }
    #endregion
    #region Propaganda Actions
    void OnCensorInternetClick()
    {
        if (propagandaSystem != null)
        {
            propagandaSystem.CensorInternet();
            mediaInfluence -= 0.1f;
            Debug.Log("Media influence reduced due to internet censorship. Media Influence: " + mediaInfluence);
        }
        UpdateUIText();
    }
    void OnBlockSourcesClick()
    {
        if (propagandaSystem != null)
        {
            propagandaSystem.BlockSources();
            mediaInfluence -= 0.05f;
            Debug.Log("Media influence reduced due to blocking sources. Media Influence: " + mediaInfluence);
        }
        UpdateUIText();
    }
    void OnReleaseMediaTypeClick()
    {
        if (propagandaSystem != null)
        {
            propagandaSystem.ReleaseMediaType();
            mediaInfluence += 0.05f;
            Debug.Log("Media influence increased due to releasing media type. Media Influence: " + mediaInfluence);
        }
        UpdateUIText();
    }
    void OnTelevisionToggleChanged(bool isOn)
    {
        if (propagandaSystem != null)
        {
            propagandaSystem.ToggleTelevision(isOn);
            Debug.Log("Television toggled: " + isOn);
        }
        UpdateUIText();
    }
    void OnSocialMediaToggleChanged(bool isOn)
    {
        if (propagandaSystem != null)
        {
            propagandaSystem.ToggleSocialMedia(isOn);
            Debug.Log("Social media toggled: " + isOn);
        }
        UpdateUIText();
    }
    void OnNewspapersToggleChanged(bool isOn)
    {
        if (propagandaSystem != null)
        {
            propagandaSystem.ToggleNewspapers(isOn);
            Debug.Log("Newspapers toggled: " + isOn);
        }
        UpdateUIText();
    }
    #endregion
    #region Random Events
    IEnumerator RandomEvents()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 30f)); // Wait for a random time between 10 and 30 seconds
            int eventType = Random.Range(0, 4);
            switch (eventType)
            {
                case 0:
                    EconomicCrisis();
                    break;
                case 1:
                    PoliticalScandal();
                    break;
                case 2:
                    SocialUnrest();
                    break;
                case 3:
                    MilitaryDefection();
                    break;
            }
        }
    }
    void EconomicCrisis()
    {
        rebellionLevel += 0.1f;
        stabilityLevel -= 0.1f;
        Debug.Log("Economic crisis occurred. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();
        // Apply economic penalty
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-100f);
        }
    }
    void PoliticalScandal()
    {
        rebellionLevel += 0.15f;
        stabilityLevel -= 0.15f;
        Debug.Log("Political scandal occurred. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();
        // Apply economic penalty
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-150f);
        }
    }
    void SocialUnrest()
    {
        rebellionLevel += 0.05f;
        stabilityLevel -= 0.05f;
        Debug.Log("Social unrest occurred. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();
    }
    void MilitaryDefection()
    {
        rebellionLevel += 0.2f;
        stabilityLevel -= 0.2f;
        Debug.Log("Military defection occurred. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();
        // Reduce army force
        if (governmentManager != null)
        {
            governmentManager.UpdateArmyForce(governmentManager.armyForce - 50);
        }
        // Apply economic penalty
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-300f);
        }
    }
    #endregion
}
#region Advanced Diplomacy Integration
void HandleInternationalReputation(DiplomaticAction action)
{
    float reputationChange = 0f;
    switch (action)
    {
        case DiplomaticAction.DeclareWar:
            reputationChange = -0.2f;
            break;
        case DiplomaticAction.MakePeace:
            reputationChange = 0.15f;
            break;
        case DiplomaticAction.FormAlliance:
            reputationChange = 0.1f;
            break;
        case DiplomaticAction.BreakAlliance:
            reputationChange = -0.1f;
            break;
        case DiplomaticAction.SignTradeAgreement:
            reputationChange = 0.05f;
            break;
        case DiplomaticAction.EndTradeAgreement:
            reputationChange = -0.05f;
            break;
        case DiplomaticAction.VoteForUNResolution:
            reputationChange = 0.1f;
            break;
        case DiplomaticAction.ImposeSanctions:
            reputationChange = -0.1f;
            break;
        case DiplomaticAction.LiftSanctions:
            reputationChange = 0.1f;
            break;
        case DiplomaticAction.ConductNuclearStrike:
            reputationChange = -0.5f;
            break;
        case DiplomaticAction.ConductMilitaryIntervention:
            reputationChange = -0.3f;
            break;
    }
    UpdateInternationalReputation(reputationChange);
}
void UpdateInternationalReputation(float change)
{
    internationalReputation += change;
    internationalReputation = Mathf.Clamp(internationalReputation, 0f, 1f);
    Debug.Log($"International Reputation updated to: {internationalReputation}");
    UpdateUIText();
}
#endregion
#region Propaganda System Integration
public void OnCensorInternetClick()
{
    if (propagandaSystem != null)
    {
        propagandaSystem.CensorInternet();
        mediaInfluence -= 0.1f;
        Debug.Log("Media influence reduced due to internet censorship. Media Influence: " + mediaInfluence);
    }
    UpdateUIText();
}
public void OnBlockSourcesClick()
{
    if (propagandaSystem != null)
    {
        propagandaSystem.BlockSources();
        mediaInfluence -= 0.05f;
        Debug.Log("Media influence reduced due to blocking sources. Media Influence: " + mediaInfluence);
    }
    UpdateUIText();
}
public void OnReleaseMediaTypeClick()
{
    if (propagandaSystem != null)
    {
        propagandaSystem.ReleaseMediaType();
        mediaInfluence += 0.05f;
        Debug.Log("Media influence increased due to releasing media type. Media Influence: " + mediaInfluence);
    }
    UpdateUIText();
}
public void OnTelevisionToggleChanged(bool isOn)
{
    if (propagandaSystem != null)
    {
        propagandaSystem.ToggleTelevision(isOn);
        Debug.Log("Television toggled: " + isOn);
    }
    UpdateUIText();
}
public void OnSocialMediaToggleChanged(bool isOn)
{
    if (propagandaSystem != null)
    {
        propagandaSystem.ToggleSocialMedia(isOn);
        Debug.Log("Social media toggled: " + isOn);
    }
    UpdateUIText();
}
public void OnNewspapersToggleChanged(bool isOn)
{
    if (propagandaSystem != null)
    {
        propagandaSystem.ToggleNewspapers(isOn);
        Debug.Log("Newspapers toggled: " + isOn);
    }
    UpdateUIText();
}
#endregion
#region Population Support Mechanics
void UpdatePopulationSupport()
{
    float supportChange = CalculatePopulationSupportChange();
    populationSupport += supportChange;
    populationSupport = Mathf.Clamp(populationSupport, 0f, 1f);
    Debug.Log("Population support updated to: " + populationSupport);
    UpdateUIText();
}
float CalculatePopulationSupportChange()
{
    float supportChange = 0f;
    // Stability affects support positively
    supportChange += stabilityLevel * 0.2f;
    // Rebellion affects support negatively
    supportChange -= rebellionLevel * 0.3f;
    // Economic factors
    if (economyManager != null)
    {
        supportChange += economyManager.budget > 0 ? 0.1f : -0.1f;
    }
    // Media influence
    supportChange += mediaInfluence * 0.1f;
    return supportChange;
}
#endregion
#region Random Events with Advanced Logic
IEnumerator AdvancedRandomEvents()
{
    while (true)
    {
        yield return new WaitForSeconds(Random.Range(10f, 30f));
        int eventType = Random.Range(0, 8); // Expanded event types
        switch (eventType)
        {
            case 0:
                EconomicCrisis();
                break;
            case 1:
                PoliticalScandal();
                break;
            case 2:
                SocialUnrest();
                break;
            case 3:
                MilitaryDefection();
                break;
            case 4:
                NaturalDisaster();
                break;
            case 5:
                AssassinationAttempt();
                break;
            case 6:
                ForeignEspionage();
                break;
            case 7:
                TechnologicalBreakthrough();
                break;
        }
    }
}
void NaturalDisaster()
{
    rebellionLevel += 0.1f;
    stabilityLevel -= 0.1f;
    if (economyManager != null)
    {
        economyManager.UpdateBudget(-300f);
    }
    Debug.Log("Natural disaster occurred. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
    UpdateUIText();
}
void AssassinationAttempt()
{
    rebellionLevel += 0.15f;
    stabilityLevel -= 0.15f;
    if (governmentManager != null)
    {
        governmentManager.UpdateMinisterMorale(governmentManager.ministerMorale - 0.2f);
    }
    Debug.Log("Assassination attempt occurred. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
    UpdateUIText();
}
void ForeignEspionage()
{
    rebellionLevel += 0.05f;
    stabilityLevel -= 0.05f;
    if (diplomacySystem != null)
    {
        foreach (var relation in diplomacySystem.diplomaticRelations)
        {
            if (relation.Value.Strength > 0.3f)
            {
                relation.Value.Strength -= 0.1f;
                Debug.Log($"{relation.Key.Name} diplomatic strength reduced due to foreign espionage.");
            }
        }
    }
    Debug.Log("Foreign espionage detected. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
    UpdateUIText();
}
void TechnologicalBreakthrough()
{
    rebellionLevel -= 0.05f;
    stabilityLevel += 0.1f;
    if (economyManager != null)
    {
        economyManager.UpdateBudget(500f);
    }
    Debug.Log("Technological breakthrough achieved. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
    UpdateUIText();
}
#endregion
#region Faction-Specific Actions
void OnNegotiateWithFactionsClick()
{
    rebellionLevel -= 0.1f;
    stabilityLevel += 0.1f;
    if (propagandaSystem != null)
    {
        mediaInfluence += 0.05f;
    }
    Debug.Log("Negotiated with factions. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
    UpdateUIText();
}
void OnSuppressFactionClick()
{
    rebellionLevel += 0.1f;
    stabilityLevel -= 0.1f;
    if (propagandaSystem != null)
    {
        mediaInfluence -= 0.1f;
    }
    Debug.Log("Suppressed faction activities. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
    UpdateUIText();
}
#endregion
#region Save and Load System
public void SaveGame()
{
    PlayerPrefs.SetFloat("RebellionLevel", rebellionLevel);
    PlayerPrefs.SetFloat("StabilityLevel", stabilityLevel);
    PlayerPrefs.SetFloat("PopulationSupport", populationSupport);
    PlayerPrefs.SetFloat("InternationalReputation", internationalReputation);
    PlayerPrefs.SetFloat("MediaInfluence", mediaInfluence);
    Debug.Log("Game saved successfully.");
}
public void LoadGame()
{
    rebellionLevel = PlayerPrefs.GetFloat("RebellionLevel", 0.1f);
    stabilityLevel = PlayerPrefs.GetFloat("StabilityLevel", 0.9f);
    populationSupport = PlayerPrefs.GetFloat("PopulationSupport", 0.5f);
    internationalReputation = PlayerPrefs.GetFloat("InternationalReputation", 0.5f);
    mediaInfluence = PlayerPrefs.GetFloat("MediaInfluence", 0.5f);
    Debug.Log("Game loaded successfully.");
    UpdateUIText();
}
#endregion
#region Endgame Conditions
void CheckEndgameConditions()
{
    if (rebellionLevel >= 1f)
    {
        TriggerGameOver("Rebellion has taken over the country!");
    }
    else if (stabilityLevel <= 0f)
    {
        TriggerGameOver("The country has collapsed due to instability!");
    }
    else if (populationSupport <= 0f)
    {
        TriggerGameOver("The population has completely lost faith in the government!");
    }
}
void TriggerGameOver(string message)
{
    Debug.LogError(message);
    // Implement game-over screen or restart logic here
}
#endregion
}