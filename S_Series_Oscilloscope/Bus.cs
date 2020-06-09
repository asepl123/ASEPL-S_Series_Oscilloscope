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
    [Display("Bus", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class Bus : TestStep
    {
        #region Settings

        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("BusNo", "1-4", "Input Parameters", 2)]
        public uint BusNo { get; set; } = 1u;

        [DisplayAttribute("stateBus", "", "Input Parameters", 2)]
        public bool StateBus { get; set; } = true;

        [DisplayAttribute("quoted_string_bus", "", "Input Parameters", 2)]
        public string Quoted_string_bus { get; set; } = "Bus";

        [DisplayAttribute("readoutBus", "READout {DECimal | HEX | SIGNed | SYMBol}", "Input Parameters", 2)]
        public string ReadoutBus { get; set; } = "DECimal";

        [DisplayAttribute("clock", "{CHANnel<O> | DIGital<M> | NONE}", "Input Parameters", 2)]
        public string Clock { get; set; } = "CHANNEL1";

        [DisplayAttribute("slope", "{RISing | FALLing | EITHer}", "Input Parameters", 2)]
        public string Slope { get; set; } = "FALLing";

        [DisplayAttribute("channel_list", "", "Input Parameters", 2)]
        public string Channel_list { get; set; } = "@1,2,5";

        [DisplayAttribute("bitsStatus", "", "Input Parameters", 2)]
        public bool BitsStatus { get; set; } = true;

        [DisplayAttribute("bitNo", "", "Input Parameters", 2)]
        public uint BitNo { get; set; } = 1u;

        [DisplayAttribute("bitStatus", "", "Input Parameters", 2)]
        public bool BitStatus { get; set; } = true;

        [DisplayAttribute("busProtocol", @"{A429 | BRR | CAN | CPHY | DDR | E10GBASEKR | EPSI | FLEXray | GENRaw | I3C | IIC | JTAG | LIN | MAN | M1553 | MIPI | RFFE | SPI | SPMI | SPW | SVID | UART | USB2 | USBPD | XAUI}{CSI3 | DIGRf | DVI | FIBRechannel | {GEN8B10B | GENeric} | HOTLink | INFiniband | JESD204B | LLI | PCI3 | PCIexpress | SAS | SATA | SSIC | UFS | UNIPro | USB3 | USB31}", "Input Parameters", 2)]
        public string BusProtocol { get; set; } = "INFiniband";

        #endregion

        public Bus()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            // Bus
            MyInst.ScpiCommand(":BUS{0}:DISPlay {1}", BusNo, StateBus);
            MyInst.ScpiCommand(":BUS{0}:LABel {1}", BusNo, Quoted_string_bus);
            MyInst.ScpiCommand(":BUS{0}:READout {1}", BusNo, ReadoutBus);
            MyInst.ScpiCommand(":BUS{0}:CLOCk {1}", BusNo, Clock);
            MyInst.ScpiCommand(":BUS{0}:CLOCk:SLOPe {1}", BusNo, Slope);
            MyInst.ScpiCommand(":BUS{0}:CLEar", BusNo);
            MyInst.ScpiCommand(":BUS{0}:BITS {1},{2}", BusNo, Channel_list, BitsStatus);
            MyInst.ScpiCommand(":BUS{0}:BIT{1} {2}", BusNo, BitNo, BitStatus);
            MyInst.ScpiCommand(":BUS:B{0}:TYPE {1}", BusNo, BusProtocol);
        }
    }
}
