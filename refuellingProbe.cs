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
        IMyPistonBase piston1, piston2, piston3, piston4;
        IMyMotorStator rotor1, rotor2;
        IMyShipConnector connector;
        IMyTimerBlock timer;

        public Program()
        {
            piston1 = GridTerminalSystem.GetBlockWithName("piston 1 (main dock)") as IMyPistonBase;
            piston2 = GridTerminalSystem.GetBlockWithName("piston 2 (main dock)") as IMyPistonBase;
            piston3 = GridTerminalSystem.GetBlockWithName("piston 3 (main dock)") as IMyPistonBase;
            piston4 = GridTerminalSystem.GetBlockWithName("piston 4 (main dock)") as IMyPistonBase;

            rotor1 = GridTerminalSystem.GetBlockWithName("advanced rotor 1 (main dock)") as IMyMotorStator;
            rotor2 = GridTerminalSystem.GetBlockWithName("advanced rotor 2 (main dock)") as IMyMotorStator;

            connector = GridTerminalSystem.GetBlockWithName("connector (main dock)") as IMyShipConnector;

            timer = GridTerminalSystem.GetBlockWithName("timer (main dock)") as IMyTimerBlock;
        }

        public void Main(string argument)
        {
            if (!String.IsNullOrEmpty(argument))
                timer.CustomData = argument + "1";

            switch (timer.CustomData)
            {
                case "extend1":
                    piston1.Extend();
                    piston2.Extend();
                    Sleep(1f);
                    timer.CustomData = "extend2";
                    break;

                case "extend2":
                    if (rotor1.TargetVelocityRPM > 0)
                        rotor1.TargetVelocityRPM *= -1;
                    Sleep(2f);
                    timer.CustomData = "extend3";
                    break;

                case "extend3":
                    piston3.Extend();
                    if (rotor2.TargetVelocityRPM > 0)
                        rotor2.TargetVelocityRPM *= -1;
                    Sleep(2f);
                    timer.CustomData = "extend4";
                    break;

                case "extend4":
                    piston4.Extend();
                    Sleep(1.5f);
                    timer.CustomData = "extend5";
                    break;

                case "extend5":
                    connector.Connect();
                    timer.CustomData = "";
                    break;

                case "retract1":
                    connector.Disconnect();
                    piston4.Retract();
                    if (rotor2.TargetVelocityRPM < 0)
                        rotor2.TargetVelocityRPM *= -1;
                    piston3.Retract();
                    piston2.Retract();
                    if (rotor1.TargetVelocityRPM < 0)
                        rotor1.TargetVelocityRPM *= -1;
                    Sleep(1f);
                    timer.CustomData = "retract2";
                    break;

                case "retract2":
                    piston1.Retract();
                    timer.CustomData = "";
                    break;

                default:
                    Echo("unknown timer customdata");
                    break;
            }
        }

        private void Sleep(float time)
        {
            timer.TriggerDelay = time;
            timer.StartCountdown();
        }
    }
}
