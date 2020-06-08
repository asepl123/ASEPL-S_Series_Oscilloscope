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
    [Display("Math Functions", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class MathFunctions : TestStep
    {
        #region enum

        public enum FunctionType { Absolute, Add, Ademod, Average, Commonmode, Delay, Diff, HPF, LPF, 
            Smooth, Div, Max, Min, Mul, Offset, Sqrt, Square, Sub, XY, Invert, Integrate}

        public enum SourceType { CHANnel, DIFF, COMMonmode, FUNCtion, WMEMory, MTRend, MSPectrum }
        #endregion

            #region Settings
            [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("Math", "", "Input Parameters", 2.1)]
        public FunctionType FunctionTypeSelection { get; set; } = FunctionType.Add;

        [DisplayAttribute("Function Number", "", "Input Parameters", 2.2)]
        public uint Function { get; set; } = 1u;

        [DisplayAttribute("Operand 1", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<1-2> | FUNCtion<1-16> | WMEMory<1-4> " +
            "| MTRend | MSPectrum}", "Operand 1", 3.1)]
        public SourceType Operand1 { get; set; } = SourceType.CHANnel;

        private string operandTypeIndex1 = "";
        [DisplayAttribute("Opearand 1 index", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<1-2> | FUNCtion<1-16> | " +
            "WMEMory<1-4> | MTRend | MSPectrum}", "Operand 1", 3.2)]
        [EnabledIf("Operand1", SourceType.CHANnel, SourceType.FUNCtion, SourceType.WMEMory, HideIfDisabled = true)]
        public string OperandTypeIndex1
        {
            get
            {
                if (operandTypeIndex1 == "") operandTypeIndex1 = "0";
                int temp = Convert.ToInt16(operandTypeIndex1);
                if (Operand1 == SourceType.CHANnel)
                {
                    return (temp >= 1 && temp <= 40) ? operandTypeIndex1 : "1";
                }
                else if (Operand1 == SourceType.DIFF)
                {
                    return (temp >= 1 && temp <= 2) ? operandTypeIndex1 : "1";
                }
                else if (Operand1 == SourceType.COMMonmode)
                {
                    return (temp >= 1 && temp <= 2) ? operandTypeIndex1 : "1";
                }
                else if (Operand1 == SourceType.FUNCtion)
                {
                    return (temp >= 1 && temp <= 16) ? operandTypeIndex1 : "1";
                }
                else if (Operand1 == SourceType.WMEMory)
                {
                    return (temp >= 1 && temp <= 4) ? operandTypeIndex1 : "1";
                }
                else
                {
                    return "";
                }
            }
            set
            {
                operandTypeIndex1 = value;
            }
        }


        [DisplayAttribute("Operand 2", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<1-2> | FUNCtion<1-16> | WMEMory<1-4> " +
            "| MTRend | MSPectrum}", "Operand 2", 4.1)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Add, FunctionType.Sub, FunctionType.Mul, FunctionType.Div, FunctionType.Commonmode, FunctionType.XY, HideIfDisabled = true)]
        public SourceType Operand2 { get; set; } = SourceType.CHANnel;

        private string operandTypeIndex2 = "";
        [DisplayAttribute("Opearand 2 index", "{CHANnel<1-40> | DIFF<1-2> | COMMonmode<1-2> | FUNCtion<1-16> | " +
            "WMEMory<1-4> | MTRend | MSPectrum}", "Operand 2", 4.2)]
        [EnabledIf("Operand2", SourceType.CHANnel, SourceType.FUNCtion, SourceType.WMEMory, HideIfDisabled = true)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Add, FunctionType.Sub, FunctionType.Mul, FunctionType.Div, FunctionType.Commonmode, FunctionType.XY, HideIfDisabled = true)]
        public string OperandTypeIndex2
        {
            get
            {
                if (operandTypeIndex2 == "") operandTypeIndex2 = "0";
                int temp = Convert.ToInt16(operandTypeIndex2);
                if (Operand2 == SourceType.CHANnel)
                {
                    return (temp >= 1 && temp <= 40) ? operandTypeIndex2 : "1";
                }
                else if (Operand2 == SourceType.DIFF)
                {
                    return (temp >= 1 && temp <= 2) ? operandTypeIndex2 : "1";
                }
                else if (Operand2 == SourceType.COMMonmode)
                {
                    return (temp >= 1 && temp <= 2) ? operandTypeIndex2 : "1";
                }
                else if (Operand2 == SourceType.FUNCtion)
                {
                    return (temp >= 1 && temp <= 16) ? operandTypeIndex2 : "1";
                }
                else if (Operand2 == SourceType.WMEMory)
                {
                    return (temp >= 1 && temp <= 4) ? operandTypeIndex2 : "1";
                }
                else
                {
                    return "";
                }
            }
            set
            {
                operandTypeIndex2 = value;
            }
        }


        [DisplayAttribute("noOfAverages", "2 to 65534", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Average, HideIfDisabled = true)]
        public int NoOfAverages { get; set; } = 16;

        [DisplayAttribute("DisplayState", "", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Average, HideIfDisabled = true)]
        public bool DisplayState { get; set; } = true;

        [DisplayAttribute("offset_value", "", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Offset, HideIfDisabled = true)]
        public double Offset_value { get; set; } = 0D;

        [DisplayAttribute("bandwidthLPF", "50 to 50E9", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.LPF, HideIfDisabled = true)]
        public double BandwidthLPF { get; set; } = 50D;

        [DisplayAttribute("delay_time", "", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Delay, HideIfDisabled = true)]
        public double Delay_time { get; set; } = 1E-10D;

        [DisplayAttribute("bandwidthHPF", "", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.HPF, HideIfDisabled = true)]
        public double BandwidthHPF { get; set; } = 50D;

        [DisplayAttribute("pointsSmooth", "", "Other Parameters", 5)]
        [EnabledIf("FunctionTypeSelection", FunctionType.Smooth, HideIfDisabled = true)]
        public int PointsSmooth { get; set; } = 5;

        private String function;



        #endregion

        public MathFunctions()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            //function = MyInst.ScpiQuery<System.String>(Scpi.Format(":FUNCtion{0}?", Function), true);
            if (FunctionTypeSelection == FunctionType.Absolute) MyInst.ScpiCommand(":FUNCtion{0}:ABSolute {1}{2}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.Ademod) MyInst.ScpiCommand(":FUNCtion{0}:ADEMod {1}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.Average) MyInst.ScpiCommand(":FUNCtion{0}:AVERage {1}{2},{3}", Function, Operand1, OperandTypeIndex1, NoOfAverages);
            if (FunctionTypeSelection == FunctionType.Delay) MyInst.ScpiCommand(":FUNCtion{0}:DELay {1}{2},{3}", Function, Operand1, OperandTypeIndex1, Delay_time);
            if (FunctionTypeSelection == FunctionType.Diff) MyInst.ScpiCommand(":FUNCtion{0}:DIFF {1}{2}", Function, Operand1, OperandTypeIndex1);
            //if (enableDisplayState) MyInst.ScpiCommand(":FUNCtion{0}:DISPlay {1}", Function, DisplayState);
            if (FunctionTypeSelection == FunctionType.HPF) MyInst.ScpiCommand(":FUNCtion{0}:HIGHpass {1}{2},{3}", Function, Operand1, OperandTypeIndex1, BandwidthHPF);
            if (FunctionTypeSelection == FunctionType.Smooth) MyInst.ScpiCommand(":FUNCtion{0}:SMOoth {1}{2},{3}", Function, Operand1, OperandTypeIndex1, PointsSmooth);
            if (FunctionTypeSelection == FunctionType.Max) MyInst.ScpiCommand(":FUNCtion{0}:MAXimum {1}{2}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.Min) MyInst.ScpiCommand(":FUNCtion{0}:MINimum {1}{2}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.Offset) MyInst.ScpiCommand(":FUNCtion{0}:OFFSet {1}", Function, Offset_value);
            if (FunctionTypeSelection == FunctionType.Sqrt) MyInst.ScpiCommand(":FUNCtion{0}:SQRT {1}{2}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.Square) MyInst.ScpiCommand(":FUNCtion{0}:SQUare {1}{2}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.LPF) MyInst.ScpiCommand(":FUNCtion{0}:LOWPass {1}{2},{3}", Function, Operand1, OperandTypeIndex1, BandwidthLPF);
            if (FunctionTypeSelection == FunctionType.Invert) MyInst.ScpiCommand(":FUNCtion{0}:INVert {1}{2}", Function, Operand1, OperandTypeIndex1);
            if (FunctionTypeSelection == FunctionType.Integrate) MyInst.ScpiCommand(":FUNCtion{0}:INTegrate {1}{2}", Function, Operand1, OperandTypeIndex1);

            /*
             * Math Function with two operand
             */

            if (FunctionTypeSelection == FunctionType.Sub) MyInst.ScpiCommand(":FUNCtion{0}:SUBTract {1}{2},{3}{4}", Function, Operand1, OperandTypeIndex1, Operand2, OperandTypeIndex2);
            if (FunctionTypeSelection == FunctionType.XY) MyInst.ScpiCommand(":FUNCtion{0}:VERSus {1}{2},{3}{4}", Function, Operand1, OperandTypeIndex1, Operand2, OperandTypeIndex2);
            if (FunctionTypeSelection == FunctionType.Mul) MyInst.ScpiCommand(":FUNCtion{0}:MULTiply {1}{2},{3}{4}", Function, Operand1, OperandTypeIndex1, Operand2, OperandTypeIndex2);
            if (FunctionTypeSelection == FunctionType.Div) MyInst.ScpiCommand(":FUNCtion{0}:DIVide {1}{2},{3}{4}", Function, Operand1, OperandTypeIndex1, Operand2, OperandTypeIndex2);
            if (FunctionTypeSelection == FunctionType.Commonmode) MyInst.ScpiCommand(":FUNCtion{0}:COMMonmode {1}{2},{3}{4}", Function, Operand1, OperandTypeIndex1, Operand2, OperandTypeIndex2);
            if (FunctionTypeSelection == FunctionType.Add) MyInst.ScpiCommand(":FUNCtion{0}:ADD {1}{2},{3}{4}", Function, Operand1, OperandTypeIndex1, Operand2, OperandTypeIndex2);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
