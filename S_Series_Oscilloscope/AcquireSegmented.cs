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
    [Display("AcquireSegmented", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class AcquireSegmented : TestStep
    {
        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("AquireSegmAutoplay", "", "Input Parameters", 2)]
        public bool AquireSegmAutoplay { get; set; } = false;

        [DisplayAttribute("AquireSegmTtags", "", "Input Parameters", 2)]
        public bool AquireSegmTtags { get; set; } = false;

        [DisplayAttribute("AcquireSegmPrate", "", "Input Parameters", 2)]
        public double AcquireSegmPrate { get; set; } = 0.1D;

        [DisplayAttribute("AcquireSegmPlay", "", "Input Parameters", 2)]
        public bool AcquireSegmPlay { get; set; } = false;

        [DisplayAttribute("AcquireSegmIndex", "", "Input Parameters", 2)]
        public int AcquireSegmIndex { get; set; } = 1000;

        [DisplayAttribute("AcquireSegmCount", "", "Input Parameters", 2)]
        public int AcquireSegmCount { get; set; } = 1000;

        [DisplayAttribute("AcquireMode", "", "Input Parameters", 2)]
        public string AcquireMode { get; set; } = "SEGMented";

        #endregion

        public AcquireSegmented()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":ACQuire:MODE {0}", AcquireMode);
            MyInst.ScpiCommand(":ACQuire:SEGMented:AUToplay {0}", AquireSegmAutoplay);
            MyInst.ScpiCommand(":ACQuire:SEGMented:PLAY {0}", AcquireSegmPlay);
            MyInst.ScpiCommand(":ACQuire:SEGMented:PRATe {0}", AcquireSegmPrate);
            MyInst.ScpiCommand(":ACQuire:SEGMented:COUNt {0}", AcquireSegmCount);
            MyInst.ScpiCommand(":ACQuire:SEGMented:INDex {0}", AcquireSegmIndex);
            MyInst.ScpiCommand(":ACQuire:SEGMented:TTAGs {0}", AquireSegmTtags);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
