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
        List<IMyConveyorSorter> sorters;
        List<MyInventoryItemFilter> StoneIce;
        List<MyInventoryItemFilter> Stone;
        List<MyInventoryItemFilter> Ice;

        public Program()
        {
            panel = GridTerminalSystem.GetBlockWithName("sorters panel (atmo miner)") as IMyTextPanel;
            sorters = new List<IMyConveyorSorter>();
            GridTerminalSystem.GetBlockGroupWithName("sorters (atmo miner)").GetBlocksOfType<IMyConveyorSorter>(sorters);

            StoneIce = new List<MyInventoryItemFilter>();
            Stone = new List<MyInventoryItemFilter>();
            Ice = new List<MyInventoryItemFilter>();

            StoneIce.Add(MyDefinitionId.Parse("MyObjectBuilder_Ore / Stone"));
            StoneIce.Add(MyDefinitionId.Parse("MyObjectBuilder_Ore / Ice"));

            Ice.Add(MyDefinitionId.Parse("MyObjectBuilder_Ore / Ice"));
            Stone.Add(MyDefinitionId.Parse("MyObjectBuilder_Ore / Stone"));

        }

        public void Main(string filterType)
        {
            switch (filterType)
            {
                case "1":
                    foreach (IMyConveyorSorter sorter in sorters)
                    {
                        sorter.CustomData = filterType;
                        sorter.SetFilter(MyConveyorSorterMode.Whitelist, StoneIce);
                    }
                    break;

                case "2":
                    foreach (IMyConveyorSorter sorter in sorters)
                    {
                        sorter.CustomData = filterType;
                        sorter.SetFilter(MyConveyorSorterMode.Whitelist, Stone);
                    }
                    break;

                case "3":
                    foreach (IMyConveyorSorter sorter in sorters)
                    {
                        sorter.CustomData = filterType;
                        sorter.SetFilter(MyConveyorSorterMode.Whitelist, Ice);
                    }
                    break;

                case "ON":
                    enableSorters(true);
                    break;

                case "OFF":
                    enableSorters(false);
                    break;

                default:
                    break;

            }
            displayText();
        }

        private void enableSorters(bool status)
        {
            foreach (IMyConveyorSorter sorter in sorters)
                sorter.Enabled = status;
        }




        public void displayText()
        {
            bool sortersEnabled = sorters[0].Enabled;
            string filterType = sorters[0].CustomData;
            string stoneIce, stone, ice, on, off;
            string output;

            stoneIce = "1: stone + ice";
            stone = "2: stone";
            ice = "3: ice";

            on = "8: sorters on";
            off = "9: sorters off";

            switch (filterType)
            {
                case "1":
                    stoneIce += "     X";
                    break;

                case "2":
                    stone += "     X";
                    break;

                case "3":
                    ice += "     X";
                    break;

                default:
                    break;
            }

            if (sortersEnabled)
                on += "     X";
            else
                off += "     X";

            output = "EJECT:\n\n" + stoneIce + "\n" + stone + "\n" + ice + "\n\n" + on + "\n" + off;

            panel.WriteText(output);

        }

    }
}
