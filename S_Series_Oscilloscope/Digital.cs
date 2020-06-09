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
    [Display("AcqireSegmented", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class Digital : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("DigitalNo", "", "Input Parameters", 2)]
        public uint DigitalNo { get; set; } = 1u;

        [DisplayAttribute("digitalDisplayState", "", "Input Parameters", 2)]
        public bool DigitalDisplayState { get; set; } = true;

        [DisplayAttribute("quoted_string", "", "Input Parameters", 2)]
        public string Quoted_string { get; set; } = "Digital";

        [DisplayAttribute("digitalVertcalSize", "{SMALl, MEDium, LARGe}", "Input Parameters", 2)]
        public string DigitalVertcalSize { get; set; } = "MEDium";

        [DisplayAttribute("threshold", "{CMOS50=205V | CMOS33=1.65V | CMOS25=1.25V | ECL=-1.3V | PECL=3.7V | TTL=1.4V\r\n  " +
            " | DIFFerential=0V | <value>}", "Input Parameters", 2)]
        public double Threshold { get; set; } = 1.5D;

        [DisplayAttribute("enableDigital", "", "Input Parameters", 2)]
        public string EnableDigital { get; set; } = "DIGital";

        [DisplayAttribute("disableDigital", "", "Input Parameters", 2)]
        public string DisableDigital { get; set; } = "DIGital";

        #endregion

        public Digital()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":DIGital{0}:DISPlay {1}", DigitalNo, DigitalDisplayState);
            MyInst.ScpiCommand(":DIGital{0}:LABel {1}", DigitalNo, Quoted_string);
            MyInst.ScpiCommand(":DIGital{0}:SIZE {1}", DigitalNo, DigitalVertcalSize);
            MyInst.ScpiCommand(":DIGital{0}:THReshold {1}", DigitalNo, Threshold);
            MyInst.ScpiCommand(":ENABle {0}", EnableDigital);
            MyInst.ScpiCommand(":DISable {0}", DisableDigital);
        }
    }
}
