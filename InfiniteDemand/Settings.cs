using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using System.Collections.Generic;

namespace InfiniteDemand
{
    [FileLocation(nameof(InfiniteDemand))]
    [SettingsUIGroupOrder(DemandShiftGroup)]
    [SettingsUIShowGroupName(DemandShiftGroup)]
    public class Setting : ModSetting
    {
        public const string DemandShiftSection = "Infinite Demand Settings";
        public const string ResetSection = "Reset";

        public const string DemandShiftGroup = "DemandShiftGroup";

        public Setting(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        public override void Apply()
        {
            log.Info($"[{nameof(InfiniteDemand)}] Applying settings.");
            base.Apply();
        }

        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool EnableSuperFastBuild { get; set; }

        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool EnableSuperFastLeveling { get; set; }


        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool EnableCustomResidentialDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public int HomeBuildingDemand { get; set; }



        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool EnableCustomCommercialDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public int CommercialBuildingDemand { get; set; }



        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool EnableCustomIndustrialDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public int IndustrialBuildingDemand { get; set; }



        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool EnableCustomOfficeDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public int OfficeBuildingDemand { get; set; }



        [SettingsUIButton]
        [SettingsUISection(DemandShiftSection, DemandShiftGroup)]
        public bool Button
        {
            set
            {
                SetDefaults();
                Contra = false;
                ApplyAndSave();
            }
        }

        [SettingsUIHidden]
        public bool Contra { get; set; }

        public override void SetDefaults()
        {
            log.Info($"[{nameof(InfiniteDemand)}] Setting default values.");
            Contra = true;
            CommercialBuildingDemand = 100;
            IndustrialBuildingDemand = 100;
            OfficeBuildingDemand = 100;
            HomeBuildingDemand = 100;

            EnableCustomResidentialDemand = true;
            EnableCustomCommercialDemand = true;
            EnableCustomOfficeDemand = true;
            EnableCustomIndustrialDemand = true;
            EnableSuperFastBuild = true;
            EnableSuperFastLeveling = false;
            ApplyAndSave();
        }


    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }
        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Infinite Demand" },
                { m_Setting.GetOptionTabLocaleID(Setting.DemandShiftSection), "Infinite Demand" },

                { m_Setting.GetOptionGroupLocaleID(Setting.DemandShiftGroup), "Infinite Demand Settings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button)), "Reset All" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button)), $"Reset percentages to default values (100%)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableSuperFastBuild)), "Use Super Fast Build & Ultimate Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableSuperFastBuild)), $"All property types will be built almost instantly. All other demand settings are ignored when this is enabled. Turn off to use game calculations." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableSuperFastLeveling)), "Use Fast Leveling" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableSuperFastLeveling)), $"Buildings will level up quicker. Turn off to use game calculations." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomResidentialDemand)), "Use Custom Household Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomResidentialDemand)), $"Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HomeBuildingDemand)), "Household Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HomeBuildingDemand)), $"Adjust the demand percentage for for Household Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomCommercialDemand)), "Use Custom Commercial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomCommercialDemand)), $"Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CommercialBuildingDemand)), "Commercial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CommercialBuildingDemand)), $"Adjust the demand percentage for for Commercial Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomOfficeDemand)), "Use Custom Office Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomOfficeDemand)), $"Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OfficeBuildingDemand)), "Office Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OfficeBuildingDemand)), $"Adjust the demand percentage for for Office Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomIndustrialDemand)), "Use Custom Industrial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomIndustrialDemand)), $"Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IndustrialBuildingDemand)), "Industrial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IndustrialBuildingDemand)), $"Adjust the demand percentage for for Industrial Buildings" },

            };
        }

        public void Unload()
        {

        }
    }
}