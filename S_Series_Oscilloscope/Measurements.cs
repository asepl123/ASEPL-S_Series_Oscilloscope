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
    [Display("Measurements", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class Measurements : TestStep
    {
        #region enum

        public enum SourceType { CHANnel/*<1-4>*/, FUNCtion/*<1-16>*/, WMEMory/*<1-4>*/, CLOCk, MTRend, MSPectrum, EQUalized}

        public enum DirectionType {RISing, FALLing}

        #endregion

        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: -100000)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("Source Type", "{CHANnel(1-4), FUNCtion(1-16), WMEMory(1-4), CLOCk, MTRend, MSPectrum, EQUalized}", "Input Parameters", 2)]
        public SourceType Source { get; set; } = SourceType.CHANnel;

        private string sourceNumber = "";
        [DisplayAttribute("Source Number", "{CHANnel(1-4), FUNCtion(1-16), WMEMory(1-4), CLOCk, MTRend, MSPectrum, EQUalized}", "Input Parameters", 2)]
        [EnabledIf("Source", SourceType.CHANnel, SourceType.FUNCtion, SourceType.WMEMory, HideIfDisabled = true)]
        public string SourceNumber
        {
            get
            {
                if (sourceNumber == "") sourceNumber = "0";
                int temp = Convert.ToInt16(sourceNumber);
                if(Source == SourceType.CHANnel)
                {
                    return (temp >= 1 && temp <= 4) ? sourceNumber : "1";
                }
                else if(Source == SourceType.FUNCtion)
                {
                    return (temp >= 1 && temp <= 16) ? sourceNumber : "1";
                }
                else if(Source == SourceType.WMEMory)
                {
                    return (temp >= 1 && temp <= 4) ? sourceNumber : "1";
                }
                else
                {
                    return "";
                }
            }
            set
            {
                sourceNumber = value;
            }
        }

        [DisplayAttribute("Direction of Frequency", "{RISing, FALLing}", "Input Parameters", 2)]
        [EnabledIf("enableFrequency", true, HideIfDisabled = true)]
        public DirectionType DirectionFrequency { get; set; } = DirectionType.RISing;

        [DisplayAttribute("Direction of Period", "", "Input Parameters", 2)]
        [EnabledIf("enablePeriod", true, HideIfDisabled = true)]
        public DirectionType DirectionPeriod { get; set; } = DirectionType.RISing;

        [DisplayAttribute("Rise Time", "", "Input Parameters", 2)]
        public bool enableRiseTime { get; set; }

        [DisplayAttribute("Fall Time", "", "Input Parameters", 2)]
        public bool enableFallTime { get; set; }

        [DisplayAttribute("Period", "", "Input Parameters", 2)]
        public bool enablePeriod { get; set; }

        [DisplayAttribute("Frequency", "", "Input Parameters", 2)]
        public bool enableFrequency { get; set; }

        [DisplayAttribute("Vpp", "", "Input Parameters", 2)]
        public bool enableVpp { get; set; }

        [DisplayAttribute("Direction of Period", "", "Input Parameters", 2)]

        /*
        [DisplayAttribute("sourceFrequency", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<3-4> | FUNCtion<3-4> | DIGital<0-15> | WMEM" +
            "ory<1-4> | CLOCk | MTRend | MSPectrum | EQUalized}", "Input Parameters", 2)]
        public string SourceFrequency { get; set; } = "CHANnel1";

        [DisplayAttribute("sourcePeriod", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<C> | FUNCtion<3-4> | DIGital<0-15> | WMEM" +
            "ory<1-4> | CLOCk | MTRend | MSPectrum | EQUalized}", "Input Parameters", 2)]
        public string SourcePeriod { get; set; } = "CHANnel1";

        [DisplayAttribute("sourceFallTime", "{CHANnel<1-4> | FUNCtion<1-16> | WMEMory<1-4> | CLOCk | MTRend | MSPectrum | EQUa" +
            "lized}", "Input Parameters", 2)]
        public string SourceFallTime { get; set; } = "CHANnel1";

        [DisplayAttribute("sourceRiseTime", "{CHANnel<1-4> | FUNCtion<1-16> | WMEMory<1-4> | CLOCk | MTRend | MSPectrum | EQUa" +
            "lized}", "Input Parameters", 2)]
        public string SourceRiseTime { get; set; } = "CHANnel1";

        [DisplayAttribute("sourceVpp", "{CHANnel<1-4> | FUNCtion<1-16> | WMEMory<1-4> | CLOCk | MTRend | MSPectrum | EQUa" +
            "lized}", "Input Parameters", 2)]
        public string SourceVpp { get; set; } = "CHANnel1";
        */

        #endregion

        public Measurements()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.
            
            if (enableRiseTime) MyInst.ScpiCommand(":MEASure:RISetime {0}", Source);
            if (enableFallTime) MyInst.ScpiCommand(":MEASure:FALLtime {0}", Source);
            if (enablePeriod) MyInst.ScpiCommand(":MEASure:PERiod {0},{1}", Source, DirectionPeriod);
            if (enableFrequency) MyInst.ScpiCommand(":MEASure:FREQuency {0},{1}", Source, DirectionFrequency);
            if (enableVpp) MyInst.ScpiCommand(":MEASure:VPP {0}", Source);
        }
    }
}
