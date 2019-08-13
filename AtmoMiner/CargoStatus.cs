using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        IMyTextPanel panel;
        List<IMyCargoContainer> cargos;

        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
            panel = GridTerminalSystem.GetBlockWithName("cargo status panel (atmo miner)") as IMyTextPanel;
            cargos = new List<IMyCargoContainer>();
            GridTerminalSystem.GetBlockGroupWithName("cargo containers (atmo miner)").GetBlocksOfType<IMyCargoContainer>(cargos);

            panel.ContentType = ContentType.TEXT_AND_IMAGE;
            panel.FontSize = 2.2f;
            panel.TextPadding = 33.5f;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            displayStatus(getCargoStatus());
        }

        private double getCargoStatus()
        {
            double currentVolume, maxVolume;
            currentVolume = maxVolume = 0;

            foreach(IMyCargoContainer cargo in cargos)
            {
                currentVolume+= (double) cargo.GetInventory(0).CurrentVolume;
                maxVolume += (double)cargo.GetInventory(0).MaxVolume;
            }

            return currentVolume * 100 / maxVolume;
        }

        private void displayStatus(double percent)
        {
            string percentString = String.Format("{0:0.00}%", percent);
            panel.WriteText("CARGO STATUS:\n" + percentString);
        }
    }
}
