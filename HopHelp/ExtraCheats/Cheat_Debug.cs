using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HopHelp.ExtraCheats
{
    internal static class Cheat_Debug
    {
        [CheatMenu]
        public static void SetFPS(string fps)
        {
            if (string.IsNullOrEmpty(fps))
                return;

            if (int.TryParse(fps, out var realFPS))
            {
                realFPS = Mathf.Max(realFPS, 1);
                QualitySettings.vSyncCount  = 0;
                Application.targetFrameRate = realFPS;
            }
        }

        [CheatMenu]
        public static void UnlockAllAbilities()
        {
            var goalManager = SingletonPropertyItem<GoalManager>.Instance;
            var inventory   = Generics.Player?.Inventory;

            GoalData[] goals =
            {
                inventory.GetValue<GoalData>("backpackUnlockGoal"),
                inventory.GetValue<GoalData>("climbUnlockGoal"),
                inventory.GetValue<GoalData>("cameraUnlockGoal"),
                inventory.GetValue<GoalData>("backpackUnlockGoal"),
                inventory.GetValue<GoalData>("climbUnlockGoal")
            };

            foreach(var goal in goals)
                goalManager?.CompleteGoal(goal);

            inventory?.InvokeMethod("Start");
        }
    }
}
