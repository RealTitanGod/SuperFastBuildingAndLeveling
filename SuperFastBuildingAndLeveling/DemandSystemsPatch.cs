using Game.Simulation;
using Unity.Mathematics;
using Colossal.Collections;
using HarmonyLib;

namespace SuperFastBuildingAndLeveling
{
    // Super Fast Building
    [HarmonyPatch(typeof(ZoneSpawnSystem), "OnUpdate")]
    public class ZoneSpawnSystemPatch
    {
        static void Prefix(ZoneSpawnSystem __instance)
        {
            if (!SuperFastBuildingAndLeveling.Ready)
                return;

            bool target = SuperFastBuildingAndLeveling.m_Setting.EnableSuperFastBuild;
            if (__instance.debugFastSpawn != target)
            {
                SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Fast Building update: {__instance.debugFastSpawn} → {target}");
                __instance.debugFastSpawn = target;
            }
        }
    }

    // Fast Leveling
    [HarmonyPatch(typeof(BuildingUpkeepSystem), "OnUpdate")]
    public class BuildingUpkeepSystemPatch
    {
        static void Prefix(BuildingUpkeepSystem __instance)
        {
            if (!SuperFastBuildingAndLeveling.Ready)
                return;

            bool target = SuperFastBuildingAndLeveling.m_Setting.EnableSuperFastLeveling;
            if (__instance.debugFastLeveling != target)
            {
                SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Fast Leveling update: {__instance.debugFastLeveling} → {target}");
                __instance.debugFastLeveling = target;
            }
        }
    }

    // Fast Area-Prop Spawning
    [HarmonyPatch(typeof(AreaSpawnSystem), "OnUpdate")]
    public class AreaSpawnSystemPatch
    {
        static void Prefix(AreaSpawnSystem __instance)
        {
            if (!SuperFastBuildingAndLeveling.Ready)
                return;

            bool target = SuperFastBuildingAndLeveling.m_Setting.EnableSuperFastAreaPropSpawning;
            if (__instance.debugFastSpawn != target)
            {
                SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Fast Area-Prop update: {__instance.debugFastSpawn} → {target}");
                __instance.debugFastSpawn = target;
            }
        }
    }

    // Residential Demand
    [HarmonyPatch(typeof(ResidentialDemandSystem), "OnUpdate")]
    public class ResidentialDemandSystemPatch
    {
        private static readonly AccessTools.FieldRef<ResidentialDemandSystem, NativeValue<int3>> ResidentialBuildingDemand = AccessTools.FieldRefAccess<ResidentialDemandSystem, NativeValue<int3>>("m_BuildingDemand");
        private static readonly AccessTools.FieldRef<ResidentialDemandSystem, NativeValue<int>> ResidentialHouseholdDemand = AccessTools.FieldRefAccess<ResidentialDemandSystem, NativeValue<int>>("m_HouseholdDemand");

        static void Prefix(ResidentialDemandSystem __instance)
        {
            if (!SuperFastBuildingAndLeveling.Ready)
                return;

            if (!SuperFastBuildingAndLeveling.m_Setting.EnableCustomResidentialDemand)
                return;

            int val = SuperFastBuildingAndLeveling.m_Setting.HomeBuildingDemand;
            int3 target = new int3(val, val, val);

            var building = ResidentialBuildingDemand(__instance);
            if (!building.value.Equals(target))
            {
                // SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Residential Building Demand update: {building.value} → {target}");
                building.value = target;
                ResidentialBuildingDemand(__instance) = building;
            }

            var household = ResidentialHouseholdDemand(__instance);
            if (household.value != val)
            {
                // SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Residential Household Demand update: {household.value} → {val}");
                household.value = val;
                ResidentialHouseholdDemand(__instance) = household;
            }
        }
    }

    // Commercial Demand
    [HarmonyPatch(typeof(CommercialDemandSystem), "OnUpdate")]
    public class CommercialDemandSystemPatch
    {
        private static readonly AccessTools.FieldRef<CommercialDemandSystem, NativeValue<int>> CommercialBuildingDemand = AccessTools.FieldRefAccess<CommercialDemandSystem, NativeValue<int>>("m_BuildingDemand");
        // private static AccessTools.FieldRef<CommercialDemandSystem, NativeValue<int>> CommercialCompanyDemand = AccessTools.FieldRefAccess<CommercialDemandSystem, NativeValue<int>>("m_CompanyDemand");

        static void Prefix(CommercialDemandSystem __instance)
        {
            if (!SuperFastBuildingAndLeveling.Ready)
                return;

            if (!SuperFastBuildingAndLeveling.m_Setting.EnableCustomCommercialDemand)
                return;

            int val = SuperFastBuildingAndLeveling.m_Setting.CommercialBuildingDemand;

            var building = CommercialBuildingDemand(__instance);
            if (building.value != val)
            {
                // SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Commercial Building Demand update: {building.value} → {val}");
                building.value = val;
                CommercialBuildingDemand(__instance) = building;
            }
        }
    }

    // Office & Industrial Demand
    [HarmonyPatch(typeof(IndustrialDemandSystem), "OnUpdate")]
    public class IndustrialDemandSystemPatch
    {
        private static readonly AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> IndustrialBuildingDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_IndustrialBuildingDemand");
        // private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> IndustrialCompanyDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_IndustrialCompanyDemand");
        private static readonly AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> StorageBuildingDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_StorageBuildingDemand");
        // private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> StorageCompanyDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_StorageCompanyDemand");
        private static readonly AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> OfficeBuildingDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_OfficeBuildingDemand");
        // private static AccessTools.FieldRef<IndustrialDemandSystem, NativeValue<int>> OfficeCompanyDemand = AccessTools.FieldRefAccess<IndustrialDemandSystem, NativeValue<int>>("m_OfficeCompanyDemand");

        static void Prefix(IndustrialDemandSystem __instance)
        {
            if (!SuperFastBuildingAndLeveling.Ready)
                return;

            if (SuperFastBuildingAndLeveling.m_Setting.EnableCustomIndustrialDemand)
            {
                int val = SuperFastBuildingAndLeveling.m_Setting.IndustrialBuildingDemand;

                var industrial = IndustrialBuildingDemand(__instance);
                if (industrial.value != val)
                {
                    // SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Industrial Building Demand update: {industrial.value} → {val}");
                    industrial.value = val;
                    IndustrialBuildingDemand(__instance) = industrial;
                }

                var storage = StorageBuildingDemand(__instance);
                if (storage.value != val)
                {
                    // SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Industrial Storage Demand update: {storage.value} → {val}");
                    storage.value = val;
                    StorageBuildingDemand(__instance) = storage;
                }
            }

            if (SuperFastBuildingAndLeveling.m_Setting.EnableCustomOfficeDemand)
            {
                int val = SuperFastBuildingAndLeveling.m_Setting.OfficeBuildingDemand;

                var office = OfficeBuildingDemand(__instance);
                if (office.value != val)
                {
                    // SuperFastBuildingAndLeveling.log.Info($"[{nameof(SuperFastBuildingAndLeveling)}] Office Building Demand update: {office.value} → {val}");
                    office.value = val;
                    OfficeBuildingDemand(__instance) = office;
                }
            }
        }
    }
}
