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
    public enum ETimeReference { LEFT, CENTer, RIGHt, PERCent }
    #endregion

    [Display("Horizontal Settings", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class HSettings : TestStep
    {
        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("Time Range", "", Groups: new[] { "Display Setting", "Horizontal Setting" }, Order: 2)]
        public double TimeRange { get; set; } = 0.05D;

        [DisplayAttribute("Time Reference", "{ LEFT, CENTer, RIGHt }", Groups: new[] { "Display Setting", "Horizontal Setting" }, Order: 2)]
        public ETimeReference TimeReference { get; set; } = ETimeReference.CENTer;

        [DisplayAttribute("Time Reference Percent", "", Groups: new[] { "Display Setting", "Horizontal Setting" }, Order: 2)]
        [EnabledIf("TimeReference", ETimeReference.PERCent, HideIfDisabled = true)]
        public double TimeReferencePercent { get; set; } = 10D;

        [DisplayAttribute("Roll Mode", "", Groups: new[] { "Display Setting", "Horizontal Setting" }, Order: 2)]
        public bool TimeRollMode { get; set; } = true;

        [DisplayAttribute("Time Scale", "5 ps/div to 20 s/div", Groups: new[] { "Display Setting", "Horizontal Setting" }, Order: 2)]
        public double TimeScale { get; set; } = 0.01D;

        [DisplayAttribute("Time Window Position", "", Groups: new[] { "Display Setting", "Horizontal Setting", "Window Delay" }, Order: 2)]
        public double TimeWindowPosition { get; set; } = 2E-08D;

        [DisplayAttribute("Time Window Delay", "", Groups: new[] { "Display Setting", "Horizontal Setting", "Window Delay" }, Order: 2)]
        public double TimeWindowDelay { get; set; } = 2E-08D;

        [DisplayAttribute("Time Window Range", "", Groups: new[] { "Display Setting", "Horizontal Setting", "Window Delay" }, Order: 2)]
        public double TimeWindowRange { get; set; } = 1E-07D;

        [DisplayAttribute("Time Window Scale", "", Groups: new[] { "Display Setting", "Horizontal Setting", "Window Delay" }, Order: 2)]
        public double TimeWindowScale { get; set; } = 0.002D;

        #endregion

        public HSettings()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.VerticalSetting( TimeRange, TimeReference, TimeReferencePercent, TimeRollMode, 
                TimeScale, TimeWindowDelay, TimeWindowPosition, TimeWindowRange, TimeWindowScale);
        }
    }
}
