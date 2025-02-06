using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RebellionSystem : MonoBehaviour
{
    // UI References
    public Text rebellionLevelText;
    public Text stabilityLevelText;
    public Text populationSupportText;
    public Text internationalReputationText;
    public Text mediaInfluenceText;
    public Text factionMediaSupportText;
    public Button brutalRepressionButton;
    public Button diplomaticSolutionButton;
    public Button negotiateWithRebelsButton;
    public Button offerReformButton;
    public Button increasePoliceForceButton;
    public Button reduceMilitarySpendingButton;
    public Button improveInfrastructureButton;
    public Button suppressProtestsButton;
    public Button supportOppositionButton;
    public Button launchPropagandaCampaignButton;
    public Button reduceTaxRatesButton;
    public Button increasePublicSectorButton;
    public Button reduceBlackMarketButton;
    public Button hireSpecialForcesButton;
    public Button fireMinistersButton;

    // Government Manager Reference
    [SerializeField] private GovernmentManager governmentManager;

    // Economy Manager Reference
    [SerializeField] private EconomyManager economyManager;

    // Diplomacy System Reference
    [SerializeField] private DiplomacySystem diplomacySystem;

    // Propaganda System Reference
    [SerializeField] private PropagandaSystem propagandaSystem;

    // Rebel Faction List
    private List<RebelFaction> rebelFactions = new List<RebelFaction>();

    // Population Support Variable
    private float populationSupport = 0.5f;

    // International Reputation Variable
    private float internationalReputation = 0.8f;

    // Media Influence Variable
    private float mediaInfluence = 0.5f;

    // Faction Media Support Dictionary
    private Dictionary<string, float> factionMediaSupport = new Dictionary<string, float>();

    // Rebellion and Stability Variables
    public float rebellionLevel;
    public float stabilityLevel;

    // Event Subscription
    private void OnEnable()
    {
        if (diplomacySystem != null)
        {
            diplomacySystem.declareWarButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.DeclareWar));
            diplomacySystem.makePeaceButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.MakePeace));
            diplomacySystem.formAllianceButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.FormAlliance));
            diplomacySystem.breakAllianceButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.BreakAlliance));
            diplomacySystem.signTradeAgreementButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.SignTradeAgreement));
            diplomacySystem.endTradeAgreementButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.EndTradeAgreement));
            diplomacySystem.imposeSanctionsButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.ImposeSanctions));
            diplomacySystem.liftSanctionsButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.LiftSanctions));
            diplomacySystem.conductNuclearStrikeButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.ConductNuclearStrike));
            diplomacySystem.conductMilitaryInterventionButton.onClick.AddListener(() => OnDiplomaticAction(DiplomaticAction.ConductMilitaryIntervention));
        }
    }

    private void OnDisable()
    {
        if (diplomacySystem != null)
        {
            diplomacySystem.declareWarButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.DeclareWar));
            diplomacySystem.makePeaceButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.MakePeace));
            diplomacySystem.formAllianceButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.FormAlliance));
            diplomacySystem.breakAllianceButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.BreakAlliance));
            diplomacySystem.signTradeAgreementButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.SignTradeAgreement));
            diplomacySystem.endTradeAgreementButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.EndTradeAgreement));
            diplomacySystem.imposeSanctionsButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.ImposeSanctions));
            diplomacySystem.liftSanctionsButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.LiftSanctions));
            diplomacySystem.conductNuclearStrikeButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.ConductNuclearStrike));
            diplomacySystem.conductMilitaryInterventionButton.onClick.RemoveListener(() => OnDiplomaticAction(DiplomaticAction.ConductMilitaryIntervention));
        }
    }

    void Start()
    {
        // Initialize rebellion and stability levels
        rebellionLevel = 0.1f;
        stabilityLevel = 0.9f;

        // Initialize population support, international reputation, and media influence
        populationSupport = 0.5f;
        internationalReputation = 0.8f;
        mediaInfluence = 0.5f;

        // Initialize rebel factions
        InitializeRebelFactions();

        // Setup UI elements
        SetupUI();

        // Start random events coroutine
        StartCoroutine(RandomEvents());

        // Subscribe to economy events
        if (economyManager != null)
        {
            economyManager.taxRateSlider.onValueChanged.AddListener(OnTaxRateChanged);
            economyManager.publicSectorSlider.onValueChanged.AddListener(OnPublicSectorChanged);
            economyManager.blackMarketSlider.onValueChanged.AddListener(OnBlackMarketChanged);
            economyManager.policeForceInput.onEndEdit.AddListener(OnPoliceForceChanged);
            economyManager.armyForceInput.onEndEdit.AddListener(OnArmyForceChanged);
        }

        // Subscribe to propaganda events
        if (propagandaSystem != null)
        {
            propagandaSystem.censorInternetButton.onClick.AddListener(OnCensorInternetClick);
            propagandaSystem.blockSourcesButton.onClick.AddListener(OnBlockSourcesClick);
            propagandaSystem.releaseMediaTypeButton.onClick.AddListener(OnReleaseMediaTypeClick);
            propagandaSystem.televisionToggle.onValueChanged.AddListener(OnTelevisionToggleChanged);
            propagandaSystem.socialMediaToggle.onValueChanged.AddListener(OnSocialMediaToggleChanged);
            propagandaSystem.newspapersToggle.onValueChanged.AddListener(OnNewspapersToggleChanged);
        }
    }

    void InitializeRebelFactions()
    {
        // Example initialization of rebel factions
        rebelFactions.Add(new RebelFaction { Name = "Militant Group", Strength = 0.1f, Loyalty = 0.7f });
        rebelFactions.Add(new RebelFaction { Name = "Student Protesters", Strength = 0.05f, Loyalty = 0.8f });
        rebelFactions.Add(new RebelFaction { Name = "Ethnic Separatists", Strength = 0.1f, Loyalty = 0.6f });

        Debug.Log("Rebel factions initialized:");
        foreach (var faction in rebelFactions)
        {
            Debug.Log($"Name: {faction.Name}, Strength: {faction.Strength}, Loyalty: {faction.Loyalty}");
        }
    }

    void SetupUI()
    {
        // Add listeners for button clicks
        brutalRepressionButton.onClick.AddListener(OnBrutalRepressionClick);
        diplomaticSolutionButton.onClick.AddListener(OnDiplomaticSolutionClick);
        negotiateWithRebelsButton.onClick.AddListener(OnNegotiateWithRebelsClick);
        offerReformButton.onClick.AddListener(OnOfferReformClick);
        increasePoliceForceButton.onClick.AddListener(OnIncreasePoliceForceClick);
        reduceMilitarySpendingButton.onClick.AddListener(OnReduceMilitarySpendingClick);
        improveInfrastructureButton.onClick.AddListener(OnImproveInfrastructureClick);
        suppressProtestsButton.onClick.AddListener(OnSuppressProtestsClick);
        supportOppositionButton.onClick.AddListener(OnSupportOppositionClick);
        launchPropagandaCampaignButton.onClick.AddListener(OnLaunchPropagandaCampaignClick);
        reduceTaxRatesButton.onClick.AddListener(OnReduceTaxRatesClick);
        increasePublicSectorButton.onClick.AddListener(OnIncreasePublicSectorClick);
        reduceBlackMarketButton.onClick.AddListener(OnReduceBlackMarketClick);
        hireSpecialForcesButton.onClick.AddListener(OnHireSpecialForcesClick);
        fireMinistersButton.onClick.AddListener(OnFireMinistersClick);

        // Update UI text
        UpdateUIText();
    }

    void UpdateUIText()
    {
        rebellionLevelText.text = "Rebellion Level: " + rebellionLevel.ToString("F2");
        stabilityLevelText.text = "Stability Level: " + stabilityLevel.ToString("F2");
        populationSupportText.text = "Population Support: " + populationSupport.ToString("P2");
        internationalReputationText.text = "International Reputation: " + internationalReputation.ToString("P2");
        mediaInfluenceText.text = "Media Influence: " + mediaInfluence.ToString("P2");

        factionMediaSupportText.text = "Faction Media Support:\n";
        foreach (var faction in rebelFactions)
        {
            factionMediaSupportText.text += $"{faction.Name}: {faction.MediaSupport.ToString("P2")}\n";
        }
    }

    #region Diplomatic Actions

    void OnDiplomaticAction(DiplomaticAction action)
    {
        switch (action)
        {
            case DiplomaticAction.DeclareWar:
                rebellionLevel += 0.1f;
                stabilityLevel -= 0.1f;
                Debug.Log("Rebellion level increased due to declaring war. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnWar();
                break;
            case DiplomaticAction.MakePeace:
                rebellionLevel -= 0.05f;
                stabilityLevel += 0.05f;
                Debug.Log("Stability level increased due to making peace. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnPeace();
                break;
            case DiplomaticAction.FormAlliance:
                rebellionLevel -= 0.05f;
                stabilityLevel += 0.05f;
                Debug.Log("Stability level increased due to forming an alliance. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnAlliance();
                break;
            case DiplomaticAction.BreakAlliance:
                rebellionLevel += 0.05f;
                stabilityLevel -= 0.05f;
                Debug.Log("Rebellion level increased due to breaking an alliance. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnBreakAlliance();
                break;
            case DiplomaticAction.SignTradeAgreement:
                rebellionLevel -= 0.05f;
                stabilityLevel += 0.05f;
                Debug.Log("Stability level increased due to signing a trade agreement. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnTradeAgreement();
                break;
            case DiplomaticAction.EndTradeAgreement:
                rebellionLevel += 0.05f;
                stabilityLevel -= 0.05f;
                Debug.Log("Rebellion level increased due to ending a trade agreement. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnEndTradeAgreement();
                break;
            case DiplomaticAction.VoteForUNResolution:
                Debug.Log("Stability level increased due to voting for a UN resolution. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnUNResolution(true);
                break;
            case DiplomaticAction.ImposeSanctions:
                rebellionLevel += 0.05f;
                stabilityLevel -= 0.05f;
                Debug.Log("Rebellion level increased due to imposing sanctions. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnSanctions();
                break;
            case DiplomaticAction.LiftSanctions:
                rebellionLevel -= 0.05f;
                stabilityLevel += 0.05f;
                Debug.Log("Stability level increased due to lifting sanctions. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnLiftSanctions();
                break;
            case DiplomaticAction.ConductNuclearStrike:
                rebellionLevel += 0.2f;
                stabilityLevel -= 0.2f;
                Debug.Log("Rebellion level increased due to conducting a nuclear strike. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnNuclearStrike();
                break;
            case DiplomaticAction.ConductMilitaryIntervention:
                rebellionLevel += 0.1f;
                stabilityLevel -= 0.1f;
                Debug.Log("Rebellion level increased due to conducting a military intervention. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
                AffectEconomyOnMilitaryIntervention();
                break;
        }
        UpdateUIText();
    }

    void AffectEconomyOnWar()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-500f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.1f);
            Debug.Log("Economy affected due to war. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.1f;
            Debug.Log("Media influence reduced due to war. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.2f;
        Debug.Log("International reputation reduced due to war. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnPeace()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(300f);
            economyManager.UpdatePublicSector(economyManager.publicSector + 0.05f);
            Debug.Log("Economy affected due to peace. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.05f;
            Debug.Log("Media influence increased due to peace. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += 0.1f;
        Debug.Log("International reputation increased due to peace. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnAlliance()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(200f);
            economyManager.UpdatePublicSector(economyManager.publicSector + 0.05f);
            Debug.Log("Economy affected due to alliance. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.05f;
            Debug.Log("Media influence increased due to alliance. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += 0.1f;
        Debug.Log("International reputation increased due to alliance. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnBreakAlliance()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-100f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.05f);
            Debug.Log("Economy affected due to breaking an alliance. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.05f;
            Debug.Log("Media influence reduced due to breaking an alliance. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.1f;
        Debug.Log("International reputation reduced due to breaking an alliance. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnTradeAgreement()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(400f);
            economyManager.UpdatePublicSector(economyManager.publicSector + 0.1f);
            Debug.Log("Economy affected due to trade agreement. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.05f;
            Debug.Log("Media influence increased due to trade agreement. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += 0.1f;
        Debug.Log("International reputation increased due to trade agreement. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnEndTradeAgreement()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-200f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.1f);
            Debug.Log("Economy affected due to ending a trade agreement. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.05f;
            Debug.Log("Media influence reduced due to ending a trade agreement. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.1f;
        Debug.Log("International reputation reduced due to ending a trade agreement. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnUNResolution(bool vote)
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(vote ? 100f : -50f);
            Debug.Log("Economy affected due to UN resolution. Budget: " + economyManager.budget);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += vote ? 0.05f : -0.05f;
            Debug.Log("Media influence affected due to UN resolution. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += vote ? 0.1f : -0.1f;
        Debug.Log("International reputation affected due to UN resolution. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnSanctions()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-200f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.1f);
            Debug.Log("Economy affected due to sanctions. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.1f;
            Debug.Log("Media influence reduced due to sanctions. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.1f;
        Debug.Log("International reputation reduced due to sanctions. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnLiftSanctions()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(100f);
            economyManager.UpdatePublicSector(economyManager.publicSector + 0.1f);
            Debug.Log("Economy affected due to lifting sanctions. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.1f;
            Debug.Log("Media influence increased due to lifting sanctions. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += 0.1f;
        Debug.Log("International reputation increased due to lifting sanctions. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnNuclearStrike()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-1000f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.2f);
            Debug.Log("Economy affected due to nuclear strike. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.2f;
            Debug.Log("Media influence reduced due to nuclear strike. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.5f;
        Debug.Log("International reputation reduced due to nuclear strike. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnMilitaryIntervention()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-300f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.1f);
            Debug.Log("Economy affected due to military intervention. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.1f;
            Debug.Log("Media influence reduced due to military intervention. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.2f;
        Debug.Log("International reputation reduced due to military intervention. International Reputation: " + internationalReputation);
    }

    #endregion

    #region Government Actions

    void OnTaxRateChanged(float rate)
    {
        if (rate > 0.3f)
        {
            rebellionLevel += 0.05f;
            stabilityLevel -= 0.05f;
            Debug.Log("Rebellion level increased due to high tax rates. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        }
        else
        {
            rebellionLevel -= 0.05f;
            stabilityLevel += 0.05f;
            Debug.Log("Stability level increased due to low tax rates. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        }
        UpdateUIText();
    }

    void OnPublicSectorChanged(float sector)
    {
        if (sector < 0.6f)
        {
            rebellionLevel += 0.05f;
            stabilityLevel -= 0.05f;
            Debug.Log("Rebellion level increased due to low public sector spending. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        }
        else
        {
            rebellionLevel -= 0.05f;
            stabilityLevel += 0.05f;
            Debug.Log("Stability level increased due to high public sector spending. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        }
        UpdateUIText();
    }

    void OnBlackMarketChanged(float market)
    {
        if (market > 0.1f)
        {
            rebellionLevel += 0.05f;
            stabilityLevel -= 0.05f;
            Debug.Log("Rebellion level increased due to high black market activity. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        }
        else
        {
            rebellionLevel -= 0.05f;
            stabilityLevel += 0.05f;
            Debug.Log("Stability level increased due to low black market activity. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        }
        UpdateUIText();
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

    void OnNegotiateWithRebelsClick()
    {
        rebellionLevel -= 0.1f;
        stabilityLevel += 0.1f;
        Debug.Log("Rebellion level decreased due to negotiating with rebels. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.05f;
            Debug.Log("Media influence increased due to negotiating with rebels. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += 0.1f;
        Debug.Log("International reputation increased due to negotiating with rebels. International Reputation: " + internationalReputation);
    }

    void OnOfferReformClick()
    {
        rebellionLevel -= 0.15f;
        stabilityLevel += 0.2f;
        Debug.Log("Rebellion level decreased due to offering reforms. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.1f;
            Debug.Log("Media influence increased due to offering reforms. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation += 0.2f;
        Debug.Log("International reputation increased due to offering reforms. International Reputation: " + internationalReputation);
    }

    void OnIncreasePoliceForceClick()
    {
        if (governmentManager != null)
        {
            governmentManager.UpdatePoliceForce(governmentManager.policeForce + 50);
        }
        rebellionLevel -= 0.05f;
        stabilityLevel += 0.05f;
        Debug.Log("Rebellion level decreased due to increasing police force. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();

        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.05f;
            Debug.Log("Media influence reduced due to increasing police force. Media Influence: " + mediaInfluence);
        }

        // Affect international reputation
        internationalReputation -= 0.05f;
        Debug.Log("International reputation reduced due to increasing police force. International Reputation: " + internationalReputation);
    }

    void OnReduceMilitarySpendingClick()
    {
        if (governmentManager != null)
        {
            governmentManager.UpdateArmyForce(governmentManager.armyForce - 50);
        }
        rebellionLevel -= 0.1f;
        stabilityLevel += 0.1f;
        Debug.Log("Rebellion level decreased due to reducing military spending. Rebellion Level: " + rebellionLevel + ", Stability Level: " + stabilityLevel);
        UpdateUIText();

               // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence += 0.05f;
            Debug.Log("Media influence increased due to lifting sanctions. Media Influence: " + mediaInfluence);
        }
        // Affect international reputation
        internationalReputation += 0.1f;
        Debug.Log("International reputation increased due to lifting sanctions. International Reputation: " + internationalReputation);
    }

    void AffectEconomyOnNuclearStrike()
    {
        if (economyManager != null)
        {
            economyManager.UpdateBudget(-1000f);
            economyManager.UpdatePublicSector(economyManager.publicSector - 0.2f);
            Debug.Log("Economy affected due to nuclear strike. Budget: " + economyManager.budget + ", Public Sector: " + economyManager.publicSector);
        }
        // Affect media influence
        if (propagandaSystem != null)
        {
            mediaInfluence -= 0.2f;
            Debug.Log("Media influence reduced due to nuclear strike. Media Influence: " + mediaInfluence);
        }
        // Affect international reputation
        internationalReputation -= 0.3f;
        Debug.Log("International reputation reduced due to nuclear strike. International Reputation: " + internationalReputation);
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