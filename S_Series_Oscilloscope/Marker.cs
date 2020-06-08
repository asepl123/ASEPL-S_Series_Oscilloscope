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
    [Display("Marker", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class Marker : TestStep
    {
        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("MarkMode", ":MARKer:MODE {OFF | MANual | WAVeform | MEASurement | XONLy | YONLy}", "Input Parameters", 2)]
        public string MarkMode { get; set; } = "MANual";

        [DisplayAttribute("Tstart", "", "Input Parameters", 2)]
        public double Tstart { get; set; } = 9E-08D;

        [DisplayAttribute("Tstop", "", "Input Parameters", 2)]
        public double Tstop { get; set; } = 1.9E-07D;

        [DisplayAttribute("Vstart", "", "Input Parameters", 2)]
        public double Vstart { get; set; } = -0.01D;

        [DisplayAttribute("Vstop", "", "Input Parameters", 2)]
        public double Vstop { get; set; } = 0.01D;

        [DisplayAttribute("X1Y1source", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<1-2>\r\n   | FUNCtion<1-16> | WMEMory<1-4> " +
            "| CLOCk | MTRend | MSPectrum | EQUalized\r\n   | HISTogram | DIGital<0-15> | BUS<1" +
            "-4>}", "Input Parameters", 2)]
        public string X1Y1source { get; set; } = "CHANnel1";

        [DisplayAttribute("X2Y2source", "", "Input Parameters", 2)]
        public string X2Y2source { get; set; } = "CHANnel1";

        [DisplayAttribute("deltaStatus", "", "Input Parameters", 2)]
        public bool DeltaStatus { get; set; } = true;

        [DisplayAttribute("cursor", "{DELTa | STARt | STOP}", "Input Parameters", 2)]
        public string Cursor { get; set; } = "DELTa";

        [DisplayAttribute("NameMeasurement", "", "Input Parameters", 2)]
        public string NameMeasurement { get; set; } = "MEAS1";

        private Double Xdelta;

        private String Ydelta;

        private Double CursorTime;

        private Double measurement;

        #endregion

        public Marker()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":MARKer:MODE {0}", MarkMode);
            MyInst.ScpiCommand(":MARKer:TSTArt {0}", Tstart);
            MyInst.ScpiCommand(":MARKer:TSTOp {0}", Tstop);
            MyInst.ScpiCommand(":MARKer:VSTArt {0}", Vstart);
            MyInst.ScpiCommand(":MARKer:VSTOp {0}", Vstop);
            MyInst.ScpiCommand(":MARKer:X1Position {0}", Tstart);
            MyInst.ScpiCommand(":MARKer:X2Position {0}", Tstop);
            MyInst.ScpiCommand(":MARKer:Y1Position {0}", Vstart);
            MyInst.ScpiCommand(":MARKer:Y2Position {0}", Vstop);
            MyInst.ScpiCommand(":MARKer:X1Y1source {0}", X1Y1source);
            MyInst.ScpiCommand(":MARKer:X2Y2source {0}", X2Y2source);
            Xdelta = MyInst.ScpiQuery<System.Double>(Scpi.Format(":MARKer:XDELta?"), true);
            Ydelta = MyInst.ScpiQuery<System.String>(Scpi.Format(":MARKer:YDELta?"), true);
            MyInst.ScpiCommand(":MARKer:DELTa {0}", DeltaStatus);
            measurement = MyInst.ScpiQuery<System.Double>(Scpi.Format(":MARKer:CURSor? {0}", Cursor), true);
            MyInst.ScpiCommand(":MARKer:MEASurement:MEASurement {0}", NameMeasurement);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
