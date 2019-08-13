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
        IMyCameraBlock camera;
        IMyMotorStator rotor1, rotor2;
        IMyShipController cockpit;
        List<IMyGyro> gyros;
        
        public Program()
        {
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
            camera = GridTerminalSystem.GetBlockWithName("camera (atmo miner)") as IMyCameraBlock;
            rotor1 = GridTerminalSystem.GetBlockWithName("rotor 1 (cam)") as IMyMotorStator;
            rotor2 = GridTerminalSystem.GetBlockWithName("rotor 2 (cam)") as IMyMotorStator;
            cockpit = GridTerminalSystem.GetBlockWithName("Industrial Cockpit (atmo miner)") as IMyShipController;
            gyros = new List<IMyGyro>();
            GridTerminalSystem.GetBlocksOfType<IMyGyro>(gyros);
        }

        public void Main(string argument)
        {
            if (camera.IsActive)
            {
                setGyrosStatus(false);
                rotor1.TargetVelocityRPM = (cockpit.RotationIndicator.X * 2);
                rotor2.TargetVelocityRPM = (cockpit.RotationIndicator.Y * 2);
            }
            else
                setGyrosStatus(true);
        }

        private void setGyrosStatus(bool status)
        {
            foreach (IMyGyro gyro in gyros)
                gyro.Enabled = status;
        }
    }
}
