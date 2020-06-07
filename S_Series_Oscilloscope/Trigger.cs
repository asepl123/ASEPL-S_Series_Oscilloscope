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

    public enum ETriggerMode { EDGE, GLITch, PATTern, STATe, PWIDth /*DELay, TIMeout, TV, COMM, RUNT, SEQuence, SHOLd, TRANsition, WINDow, ADVanced, SBUS<N>*/}
    public enum EListOfSource { CHANnel1, CHANnel2, CHANnel3, CHANnel4, DIGital0, DIGital1, DIGital2,
        DIGital3, DIGital4, DIGital5, DIGital6, DIGital7, DIGital8, DIGital9, DIGital10, DIGital11, DIGital12,
        DIGital13, DIGital14, DIGital15 /*AUX, LINE*/ }
    public enum EEdgeSlope {POSitive, NEGative, EITHer}
    public enum EEdgeCoupling {AC, DC, LFReject, HFReject}
    public enum EPolarity {POSitive, NEGative}
    public enum EPatternLogic {HIGH, LOW, DONTcare, RISing, FALLing}
    public enum EStateSlope {RISing, FALLing, EITHer}
    public enum EStateLogicType {AND, NAND}
    public enum EStateLogic {LOW, HIGH, DONTcare, RISing, FALLing}

#endregion

[Display("Trigger", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class Trigger : TestStep
    {
        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: -100000)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("TriggerMode", "{EDGE, GLITch, PATTern, STATe, DELay, TIMeout, TV, COMM, RUNT, SEQuence, SHOLd, T" +
            "RANsition, WINDow, PWIDth, ADVanced, SBUS<N>}", "Input Parameters", -1000)]
        public ETriggerMode TriggerMode { get; set; } = ETriggerMode.EDGE;

        // Edge

        [DisplayAttribute("EdgeNo", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.EDGE, HideIfDisabled = true)]
        public uint EdgeNo { get; set; } = 1u;

        [DisplayAttribute("EdgeSource", "{ CHANnel<N>, DIGital<M>, AUX, LINE }", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.EDGE, HideIfDisabled = true)]
        public EListOfSource EdgeSource { get; set; } = EListOfSource.CHANnel1;

        //private bool checkChannels = false;
        [DisplayAttribute("checkChannels", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.EDGE, HideIfDisabled = true)]
        public bool CheckChannels => (EdgeSource == EListOfSource.CHANnel1 || EdgeSource == EListOfSource.CHANnel2 || EdgeSource == EListOfSource.CHANnel3 || EdgeSource == EListOfSource.CHANnel4);

        [DisplayAttribute("EdgeCoupling", "COUPling {AC, DC, LFReject, HFReject}\r\nEnableIf Channel is selected", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.EDGE, HideIfDisabled = true)]
        [EnabledIf("CheckChannels", true, HideIfDisabled = true)]
        public EEdgeCoupling EdgeCoupling { get; set; } = EEdgeCoupling.DC;

        [DisplayAttribute("EdgeSlope", "{POSitive, NEGative, EITHer}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.EDGE, HideIfDisabled = true)]
        public EEdgeSlope EdgeSlope { get; set; } = EEdgeSlope.POSitive;

        // Glitch

        [DisplayAttribute("GlitchNo", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.GLITch, HideIfDisabled = true)]
        public uint GlitchNo { get; set; } = 1u;

        [DisplayAttribute("GlitchSource", "{CHANnel<N> | DIGital<M>}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.GLITch, HideIfDisabled = true)]
        public EListOfSource GlitchSource { get; set; } = EListOfSource.CHANnel1;

        [DisplayAttribute("GlitchWidth", "upto 10s", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.GLITch, HideIfDisabled = true)]
        public double GlitchWidth { get; set; } = 0.1D;

        [DisplayAttribute("GlitchPolarity", "{POSitive | NEGative}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.GLITch, HideIfDisabled = true)]
        public EPolarity GlitchPolarity { get; set; } = EPolarity.POSitive;

        //Pattern

        [DisplayAttribute("PatternNo", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PATTern, HideIfDisabled = true)]
        public uint PatternNo { get; set; } = 1u;

        [DisplayAttribute("PatternSource", "{CHANnel<N>, DIGital<M>}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PATTern, HideIfDisabled = true)]
        public EListOfSource PatternSource { get; set; } = EListOfSource.CHANnel1;

        [DisplayAttribute("PatternLogic", "{HIGH, LOW, DONTcare, RISing, FALLing}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PATTern, HideIfDisabled = true)]
        public EPatternLogic PatternLogic { get; set; } = EPatternLogic.DONTcare;

        [DisplayAttribute("PatternCondition", "[:TRIGger:PATTern:CONDition] {ENTered|EXITed\r\n                             | {GT," +
            "<time>[,PEXits|TIMeout]}\r\n                             | {LT,<time>}\r\n          " +
            "                   | {RANGe,<gt_time>,<lt_time>}}<NL>", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PATTern, HideIfDisabled = true)]
        public string PatternCondition { get; set; } = "ENTered";

        [DisplayAttribute("time1", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PATTern, HideIfDisabled = true)]
        public double Time1 { get; set; } = 0D;

        [DisplayAttribute("time2", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PATTern, HideIfDisabled = true)]
        public string Time2 { get; set; } = "PEXits";

        //State

        [DisplayAttribute("StateNo", "1,2", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.STATe, HideIfDisabled = true)]
        public uint StateNo { get; set; } = 1u;

        [DisplayAttribute("StateSlope", "{RISing | FALLing | EITHer}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.STATe, HideIfDisabled = true)]
        public EStateSlope StateSlope { get; set; } = EStateSlope.EITHer;

        [DisplayAttribute("StateLogicType", "{AND, NAND}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.STATe, HideIfDisabled = true)]
        public EStateLogicType StateLogicType { get; set; } = EStateLogicType.AND;

        [DisplayAttribute("StateSource", "{CHANnel<N> | DIGital<M>}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.STATe, HideIfDisabled = true)]
        public EListOfSource StateSource { get; set; } = EListOfSource.CHANnel1;

        [DisplayAttribute("StateLogic", "{LOW, HIGH, DONTcare, RISing, FALLing}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.STATe, HideIfDisabled = true)]
        public EStateLogic StateLogic { get; set; } = EStateLogic.HIGH;

        [DisplayAttribute("StateClock", "{CHANnel<N> | DIGital<M>}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.STATe, HideIfDisabled = true)]
        public EListOfSource StateClock { get; set; } = EListOfSource.CHANnel1;

        //PWidth

        [DisplayAttribute("PwidthNo", "", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PWIDth, HideIfDisabled = true)]
        public uint PwidthNo { get; set; } = 1u;

        [DisplayAttribute("PwidthDirection", "{GTHan, LTHan}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PWIDth, HideIfDisabled = true)]
        public string PwidthDirection { get; set; } = "LTHan";

        [DisplayAttribute("PwidthPolarity", "{NEGative, POSitive}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PWIDth, HideIfDisabled = true)]
        public string PwidthPolarity { get; set; } = "POSitive";

        [DisplayAttribute("PwidthSource", "{CHANnel<N> | DIGital<M>}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PWIDth, HideIfDisabled = true)]
        public EListOfSource PwidthSource { get; set; } = EListOfSource.CHANnel1;

        [DisplayAttribute("PwidthTpoint", "{EPULse, TIMeout}", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PWIDth, HideIfDisabled = true)]
        public string PwidthTpoint { get; set; } = "TIMeout";

        [DisplayAttribute("PwidthWidth", "from 250 ps to 10 s.", "Input Parameters", 2)]
        [EnabledIf("TriggerMode", ETriggerMode.PWIDth, HideIfDisabled = true)]
        public double PwidthWidth { get; set; } = 0.2D;

        #endregion

        public Trigger()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":TRIGger:MODE {0}", TriggerMode);
            
            if(TriggerMode == ETriggerMode.EDGE)
            {
                // Edge
                MyInst.ScpiCommand(":TRIGger:EDGE{0}:SOURce {1}", EdgeNo, EdgeSource);
                MyInst.ScpiCommand(":TRIGger:EDGE{0}:COUPling {1}", EdgeNo, EdgeCoupling);
                MyInst.ScpiCommand(":TRIGger:EDGE{0}:SLOPe {1}", EdgeNo, EdgeSlope);
            }
            else if(TriggerMode == ETriggerMode.GLITch)
            {
                // Glitch
                MyInst.ScpiCommand(":TRIGger:GLITch{0}:SOURce {1}", GlitchNo, GlitchSource);
                MyInst.ScpiCommand(":TRIGger:GLITch{0}:WIDTh {1}", GlitchNo, GlitchWidth);
                MyInst.ScpiCommand(":TRIGger:GLITch{0}:POLarity {1}", GlitchNo, GlitchPolarity);
            }
            else if(TriggerMode == ETriggerMode.PATTern)
            {
                // Pattern
                MyInst.ScpiCommand(":TRIGger:PATTern{0}:LOGic {1},{2}", PatternNo, PatternSource, PatternLogic);
                MyInst.ScpiCommand(":TRIGger:PATTern{0}:CONDition {1},{2},{3}", PatternNo, PatternCondition, Time1, Time2);
            }
            else if(TriggerMode == ETriggerMode.STATe)
            {
                // State
                MyInst.ScpiCommand(":TRIGger:STATe{0}:SLOPe {1}", StateNo, StateSlope);
                MyInst.ScpiCommand(":TRIGger:STATe{0}:LTYPe {1}", StateNo, StateLogicType);
                MyInst.ScpiCommand(":TRIGger:STATe{0}:LOGic {1},{2}", StateNo, StateSource, StateLogic);
                MyInst.ScpiCommand(":TRIGger:STATe{0}:CLOCk {1}", StateNo, StateClock);
            }
            else if(TriggerMode == ETriggerMode.PWIDth)
            {
                // PWidth
                MyInst.ScpiCommand(":TRIGger:PWIDth{0}:DIRection {1}", PwidthNo, PwidthDirection);
                MyInst.ScpiCommand(":TRIGger:PWIDth{0}:POLarity {1}", PwidthNo, PwidthPolarity);
                MyInst.ScpiCommand(":TRIGger:PWIDth{0}:SOURce {1}", PwidthNo, PwidthSource);
                MyInst.ScpiCommand(":TRIGger:PWIDth{0}:TPOint {1}", PwidthNo, PwidthTpoint);
                MyInst.ScpiCommand(":TRIGger:PWIDth{0}:WIDTh {1}", PwidthNo, PwidthWidth);
            }
        }
    }
}
