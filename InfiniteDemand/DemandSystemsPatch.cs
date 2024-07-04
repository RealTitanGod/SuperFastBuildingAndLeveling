using Game.Simulation;
using Unity.Mathematics;
using Colossal.Collections;
using HarmonyLib;

namespace InfiniteDemand
{
    // Super Fast Build & Ultimate Demand
    [HarmonyPatch(typeof(ZoneSpawnSystem), "OnUpdate")]
    public class ZoneSpawnSystemPatch
    {
        static void Prefix(ZoneSpawnSystem __instance)
        {
            if (InfiniteDemand.m_Setting.EnableSuperFastBuild)
            {
                __instance.debugFastSpawn = true;
            }
            else
            {
                __instance.debugFastSpawn = false;
            }
        }
    }

    // Fast Leveling
    [HarmonyPatch(typeof(BuildingUpkeepSystem), "OnUpdate")]
    public class BuildingUpkeepSystemPatch
    {
        static void Prefix(BuildingUpkeepSystem __instance)
        {
            if (InfiniteDemand.m_Setting.EnableSuperFastLeveling)
            {
                __instance.debugFastLeveling = true;
            }
            else
            {
                __instance.debugFastLeveling = false;
            }
        }
    }

    // Residential Demand
    [HarmonyPatch(typeof(ResidentialDemandSystem), "OnUpdate")]
    public class ResidentialDemandSystemPatch
    {
        private static AccessTools.FieldRef<ResidentialDemandSystem, NativeValue<int3>> ResidentialBuildingDemand = AccessTools.FieldRefAccess<ResidentialDemandSystem, NativeValue<int3>>("m_BuildingDemand");
        private static AccessTools.FieldRef<ResidentialDemandSystem, NativeValue<int>> ResidentialHouseholdDemand = AccessTools.FieldRefAccess<ResidentialDemandSystem, NativeValue<int>>("m_HouseholdDemand");

        static void Prefix(ResidentialDemandSystem __instance)
        {
            if (InfiniteDemand.m_Setting.EnableCustomResidentialDemand)
            {
                ResidentialBuildingDemand(__instance).value = InfiniteDemand.m_Setting.HomeBuildingDemand;
                ResidentialHouseholdDemand(__instance).value = InfiniteDemand.m_Setting.HomeBuildingDemand;
            }
        }
    }

    // Commercial Demand
    [HarmonyPatch(typeof(CommercialDemandSystem), "OnUpdate")]
    public class CommercialDemandSystemPatch
    {
        private static AccessTools.FieldRef<CommercialDemandSystem, NativeValue<int>> CommercialBuildingDemand = AccessTools.FieldRefAccess<CommercialDemandSystem, NativeValue<int>>("m_BuildingDemand");
        private static AccessTools.FieldRef<CommercialDemandSystem, NativeValue<int>> CommercialCompanyDemand = AccessTools.FieldRefAccess<CommercialDemandSystem, NativeValue<int>>("m_CompanyDemand");

        static void Prefix(CommercialDemandSystem __instance)
        {
            if (InfiniteDemand.m_Setting.EnableCustomCommercialDemand)
            {
                CommercialBuildingDemand(__instance).value = InfiniteDemand.m_Setting.CommericalBuildingDemand;
                // CommercialCompanyDemand(__instance).value = InfiniteDemand.m_Setting.CommericalBuildingDemand; // Company Demand doesn't seem to have much of an effect in game
            }
        }
    }

    // Office & Industrial Demand
    [HarmonyPatch(typeof(IndustrialDemandSystem), "OnUpdate")]
    public class IndustrialDemandSystemPatch
    {
        private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> IndustrialBuildingDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_IndustrialBuildingDemand");
        private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> IndustrialCompanyDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_IndustrialCompanyDemand");
        private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> StorageBuildingDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_StorageBuildingDemand");
        private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> StorageCompanyDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_StorageCompanyDemand");
        private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> OfficeBuildingDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_OfficeBuildingDemand");
        private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> OfficeCompanyDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_OfficeCompanyDemand");

        static void Prefix(IndustrialDemandSystem __instance)
        {
            if (InfiniteDemand.m_Setting.EnableCustomIndustrialDemand)
            {
                IndustrialBuildingDemand(__instance).value = InfiniteDemand.m_Setting.IndustrialBuildingDemand;
                // IndustrialCompanyDemand(__instance).value = InfiniteDemand.m_Setting.IndustrialBuildingDemand;
                StorageBuildingDemand(__instance).value = InfiniteDemand.m_Setting.IndustrialBuildingDemand;
                // StorageCompanyDemand(__instance).value = InfiniteDemand.m_Setting.IndustrialBuildingDemand;
            }
            if (InfiniteDemand.m_Setting.EnableCustomOfficeDemand)
            {
                OfficeBuildingDemand(__instance).value = InfiniteDemand.m_Setting.OfficeBuildingDemand;
                // OfficeCompanyDemand(__instance).value = InfiniteDemand.m_Setting.OfficeBuildingDemand;
            }
        }
    }
}
