using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using System.Collections.Generic;

namespace SuperFastBuildingAndLeveling
{
    [FileLocation(nameof(SuperFastBuildingAndLeveling))]
    [SettingsUITabOrder(GeneralSettingsTab, LegacySettingsTab, InfoTab)]
    [SettingsUIGroupOrder(GeneralSettingsGroup, ResidentialSettingsGroup, CommercialSettingsGroup, IndustrialSettingsGroup, OfficeSettingsGroup, InfoGroup)]
    [SettingsUIShowGroupName(ResidentialSettingsGroup, CommercialSettingsGroup, IndustrialSettingsGroup, OfficeSettingsGroup)]
    public class Setting : ModSetting
    {
        public const string GeneralSettingsTab = "GeneralSettingsTab";
        public const string GeneralSettingsGroup = "GeneralSettingsGroup";
        public const string LegacySettingsTab = "LegacySettingsTab";
        public const string ResidentialSettingsGroup = "ResidentialSettingsGroup";
        public const string CommercialSettingsGroup = "CommercialSettingsGroup";
        public const string IndustrialSettingsGroup = "IndustrialSettingsGroup";
        public const string OfficeSettingsGroup = "OfficeSettingsGroup";
        public const string InfoTab = "InfoTab";
        public const string InfoGroup = "InfoGroup";

        public Setting(IMod mod) : base(mod)
        {
            SetDefaults();
        }

        public override void Apply()
        {
            log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Applying settings.");
            base.Apply();
        }

        [SettingsUISection(GeneralSettingsTab, GeneralSettingsGroup)]
        public bool EnableSuperFastBuild { get; set; }

        [SettingsUISection(GeneralSettingsTab, GeneralSettingsGroup)]
        public bool EnableSuperFastLeveling { get; set; }


        [SettingsUISection(LegacySettingsTab, ResidentialSettingsGroup)]
        public bool EnableCustomResidentialDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(LegacySettingsTab, ResidentialSettingsGroup)]
        public int HomeBuildingDemand { get; set; }



        [SettingsUISection(LegacySettingsTab, CommercialSettingsGroup)]
        public bool EnableCustomCommercialDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(LegacySettingsTab, CommercialSettingsGroup)]
        public int CommercialBuildingDemand { get; set; }



        [SettingsUISection(LegacySettingsTab, IndustrialSettingsGroup)]
        public bool EnableCustomIndustrialDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(LegacySettingsTab, IndustrialSettingsGroup)]
        public int IndustrialBuildingDemand { get; set; }



        [SettingsUISection(LegacySettingsTab, OfficeSettingsGroup)]
        public bool EnableCustomOfficeDemand { get; set; }

        [SettingsUISlider(min = 0, max = 100, step = 1, scalarMultiplier = 1, unit = Unit.kPercentage)]
        [SettingsUISection(LegacySettingsTab, OfficeSettingsGroup)]
        public int OfficeBuildingDemand { get; set; }


        [SettingsUISection(InfoTab, InfoGroup)]
        public string Mod => "Super Fast Building And Leveling";

        [SettingsUISection(InfoTab, InfoGroup)]
        public string Version => "2.0.2";

        [SettingsUISection(InfoTab, InfoGroup)]
        public string Developer => "TitanGod_";

        [SettingsUIButton]
        [SettingsUISection(InfoTab, InfoGroup)]
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
            log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Setting default values.");
            Contra = true;
            CommercialBuildingDemand = 100;
            IndustrialBuildingDemand = 100;
            OfficeBuildingDemand = 100;
            HomeBuildingDemand = 100;

            EnableCustomResidentialDemand = false;
            EnableCustomCommercialDemand = false;
            EnableCustomOfficeDemand = false;
            EnableCustomIndustrialDemand = false;
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
                { m_Setting.GetSettingsLocaleID(), "Super Fast Building And Leveling" },

                { m_Setting.GetOptionTabLocaleID(nameof(Setting.GeneralSettingsTab)), "General Settings" },
                { m_Setting.GetOptionTabLocaleID(nameof(Setting.LegacySettingsTab)), "Legacy Settings" },
                { m_Setting.GetOptionTabLocaleID(nameof(Setting.InfoTab)), "Info" },

                { m_Setting.GetOptionGroupLocaleID(nameof(Setting.GeneralSettingsGroup)), "General Settings" },
                { m_Setting.GetOptionGroupLocaleID(nameof(Setting.ResidentialSettingsGroup)), "Residential Settings" },
                { m_Setting.GetOptionGroupLocaleID(nameof(Setting.CommercialSettingsGroup)), "Commercial Settings" },
                { m_Setting.GetOptionGroupLocaleID(nameof(Setting.IndustrialSettingsGroup)), "Industrial Settings" },
                { m_Setting.GetOptionGroupLocaleID(nameof(Setting.OfficeSettingsGroup)), "Office Settings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableSuperFastBuild)), "Use Super Fast Building" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableSuperFastBuild)), "All types of zoned property will be built almost instantly. Disable to return to regular gameplay building speed." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableSuperFastLeveling)), "Use Fast Leveling" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableSuperFastLeveling)), "All buildings will level up at a quicker rate. Disable to return to regular gameplay building speed. Enabling this setting may cause the game to stutter depending on your city's size." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomResidentialDemand)), "Use Custom Household Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomResidentialDemand)), "Sets the zone's demand level to your selected percentage. Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.HomeBuildingDemand)), "Household Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.HomeBuildingDemand)), "Adjust the demand percentage for Household Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomCommercialDemand)), "Use Custom Commercial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomCommercialDemand)), "Sets the zone's demand level to your selected percentage. Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CommercialBuildingDemand)), "Commercial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CommercialBuildingDemand)), "Adjust the demand percentage for Commercial Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomOfficeDemand)), "Use Custom Office Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomOfficeDemand)), "Sets the zone's demand level to your selected percentage. Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OfficeBuildingDemand)), "Office Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OfficeBuildingDemand)), "Adjust the demand percentage for Office Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.EnableCustomIndustrialDemand)), "Use Custom Industrial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.EnableCustomIndustrialDemand)), "Sets the zone's demand level to your selected percentage. Turn off to use game calculations." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IndustrialBuildingDemand)), "Industrial Building Demand" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IndustrialBuildingDemand)), "Adjust the demand percentage for Industrial Buildings" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Mod)), "Mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Mod)), "" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Version)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Version)), "" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Developer)), "Developer" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Developer)), "" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.Button)), "Reset All" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.Button)), "Reset all settings to default values." },


            };
        }

        public void Unload()
        {

        }
    }
}