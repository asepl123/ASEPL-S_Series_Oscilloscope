// Author: MyName
// Copyright:   Copyright 2020 Keysight Technologies
//              You have a royalty-free right to use, modify, reproduce and distribute
//              the sample application files (and/or any modified version) in any way
//              you find useful, provided that you agree that Keysight Technologies has no
//              warranty, obligations or liability for any sample application files.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

namespace S_Series_Oscilloscope
{
    #region enum

    public enum EUnits {VOLT, AMPere, WATT, UNKNown}

    #endregion

    [Display("Vertical Settings", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class VSettings : TestStep
    {
        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("ChannelNo", "", Groups: new[] { "Display Setting", "Vertical Setting" }, Order: 2)]
        public uint ChannelNo { get; set; } = 1u;

        [DisplayAttribute("Units", "{VOLT, AMPere, WATT, UNKNown}", Groups: new[] { "Display Setting", "Vertical Setting" }, Order: 2)]
        public EUnits Units { get; set; } = EUnits.VOLT;

        [DisplayAttribute("Vertical Scale", "", Groups: new[] { "Display Setting", "Vertical Setting" }, Order: 2)]
        public double VScale { get; set; } = 0.5D;

        [DisplayAttribute("Vertical Range", "", Groups: new[] { "Display Setting", "Vertical Setting" }, Order: 2)]
        public double VRange { get; set; } = 0.5D;

        [DisplayAttribute("Vertical Offset", "", Groups: new[] { "Display Setting", "Vertical Setting" }, Order: 2)]
        public double VOffset { get; set; } = 0D;

        //[DisplayAttribute("VLable", "", "Input Parameters", 2)]
        //public string VLable { get; set; } = "Data";

        [DisplayAttribute("Vertical Invert", "", Groups: new[] { "Display Setting", "Vertical Setting" }, Order: 2)]
        public bool VInvert { get; set; } = true;
        #endregion

        public VSettings()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.VerticalSetting(ChannelNo, Units, VScale, VRange, VOffset, VInvert);

            // If no verdict is used, the verdict will default to NotSet.
            // You can change the verdict using UpgradeVerdict() as shown below.
            // UpgradeVerdict(Verdict.Pass);s
        }

        
    }
}
