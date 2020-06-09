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
    [Display("Saving", Group: "S_Series_Oscilloscope", Description: "Insert a description here")]
    public class Saving : TestStep
    {
        #region Settings
        [Display("Instrument", Group: "Instrument Setting", Description: "Configure Network Analyzer", Order: 1.1)]
        public Oscilloscope MyInst { get; set; }

        [DisplayAttribute("directory", "", "Input Parameters", 2)]
        public string Directory { get; set; } = "C:\\temp";

        [DisplayAttribute("cp_source_file", "", "Input Parameters", 2)]
        public string Cp_source_file { get; set; } = "abc";

        [DisplayAttribute("cp_dest_file", "", "Input Parameters", 2)]
        public string Cp_dest_file { get; set; } = "acd";

        [DisplayAttribute("delete_file", "", "Input Parameters", 2)]
        public string Delete_file { get; set; } = "abc";

        [DisplayAttribute("load_file_name", "", "Input Parameters", 2)]
        public string Load_file_name { get; set; } = "asc";

        [DisplayAttribute("load_destination", "{WMEMORY1, WMEMORY2, WMEMORY3, WMEMORY4}", "Input Parameters", 2)]
        public string Load_destination { get; set; } = "WMEMORY1";

        [DisplayAttribute("create_directory", "", "Input Parameters", 2)]
        public string Create_directory { get; set; } = "C:\\temp\\ac";

        [DisplayAttribute("sagmentedName", "{ALL | CURRent}", "Input Parameters", 2)]
        public string SagmentedName { get; set; } = "ALL";

        [DisplayAttribute("CompositeName", ".osc", "Input Parameters", 2)]
        public string CompositeName { get; set; } = "c:/abc/Cmop001";

        [DisplayAttribute("Image_name", "", "Input Parameters", 2)]
        public string Image_name { get; set; } = "abc";

        [DisplayAttribute("ImageFormat", "{BMP | GIF | TIF | JPEG | PNG}", "Input Parameters", 2)]
        public string ImageFormat { get; set; } = "BMP";

        [DisplayAttribute("scrn_or_grat", "[,{SCReen | GRATicule}", "Input Parameters", 2)]
        public string Scrn_or_grat { get; set; } = "SCReen";

        [DisplayAttribute("CompressionOnOff", "", "Input Parameters", 2)]
        public bool CompressionOnOff { get; set; } = true;

        [DisplayAttribute("norm_or_inv", "[,{NORMal | INVert}", "Input Parameters", 2)]
        public string Norm_or_inv { get; set; } = "NORMal";

        [DisplayAttribute("Setupinfo_on_Off", "", "Input Parameters", 2)]
        public bool Setupinfo_on_Off { get; set; } = true;

        [DisplayAttribute("jitter_file_name", ".csv", "Input Parameters", 2)]
        public string Jitter_file_name { get; set; } = "file1";

        [DisplayAttribute("measurement_file", ".csv", "Input Parameters", 2)]
        public string Measurement_file { get; set; } = "FILE1";

        [DisplayAttribute("legacyMode", "", "Input Parameters", 2)]
        public bool LegacyMode { get; set; } = true;

        [DisplayAttribute("PRECprobe_name", "", "Input Parameters", 2)]
        public string PRECprobe_name { get; set; } = "file";

        [DisplayAttribute("probeChannel", "{CHANnel1, CHANnel2, CHANnel3, CHANnel4}", "Input Parameters", 2)]
        public string ProbeChannel { get; set; } = "CHANnel1";

        [DisplayAttribute("setup_file", ".set", "Input Parameters", 2)]
        public string Setup_file { get; set; } = "setup";

        [DisplayAttribute("WaveformSource", "{ALL | CHANnel(1-4) | CLOCk | FUNCtion(1-16) | HISTogram | MTRend | MSPectrum | E" +
            "QUalized | WMEMory(1-4) | BUS() | PODALL | POD1 | POD2}", "Input Parameters", 2)]
        public string WaveformSource { get; set; } = "CHANNEL1";

        [DisplayAttribute("Wave_file", "", "Input Parameters", 2)]
        public string Wave_file { get; set; } = "asc";

        [DisplayAttribute("wave_file_format", "With Bus, BIN, CSV, TSV, and TXT \r\n{BIN | CSV | INTernal | TSV | TXT | H5 | H5INt" +
            " | MATlab}", "Input Parameters", 2)]
        public string Wave_file_format { get; set; } = "TXT";

        [DisplayAttribute("Wave_file_header", "", "Input Parameters", 2)]
        public bool Wave_file_header { get; set; } = true;

        private String pwd;


        #endregion

        public Saving()
        {
            // ToDo: Set default values for properties / settings.
        }

        public override void Run()
        {
            // ToDo: Add test case code.
            RunChildSteps(); //If the step supports child steps.

            MyInst.ScpiCommand(":DISK:CDIRectory {0}", Directory);
            MyInst.ScpiCommand(":DISK:COPY {0},{1}", Cp_source_file, Cp_dest_file);
            MyInst.ScpiCommand(":DISK:DELete {0}", Delete_file);
            MyInst.ScpiCommand(":DISK:LOAD {0},{1}", Load_file_name, Load_destination);
            MyInst.ScpiCommand(":DISK:MDIRectory {0}", Create_directory);
            pwd = MyInst.ScpiQuery<System.String>(Scpi.Format(":DISK:PWD?"), true);
            MyInst.ScpiCommand(":DISK:SEGMented {0}", SagmentedName);
            MyInst.ScpiCommand(":DISK:SAVE:COMPosite {0}", CompositeName);
            MyInst.ScpiCommand(":DISK:SAVE:IMAGe {0},{1},{2},{3},{4},{5}", Image_name, ImageFormat, Scrn_or_grat, CompressionOnOff, Norm_or_inv, Setupinfo_on_Off);
            MyInst.ScpiCommand(":DISK:SAVE:JITTer {0}", Jitter_file_name);
            MyInst.ScpiCommand(":DISK:SAVE:MEASurements {0},{1}", Measurement_file, LegacyMode);
            MyInst.ScpiCommand(":DISK:SAVE:PRECprobe {0},{1}", PRECprobe_name, ProbeChannel);
            MyInst.ScpiCommand(":DISK:SAVE:SETup {0}", Setup_file);
            MyInst.ScpiCommand(":DISK:SAVE:WAVeform {0},{1},{2},{3}", WaveformSource, Wave_file, Wave_file_format, Wave_file_header);
            // UpgradeVerdict(Verdict.Pass);
        }
    }
}
