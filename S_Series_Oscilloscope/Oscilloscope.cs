using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using OpenTap;

//Note this template assumes that you have a SCPI based instrument, and accordingly
//extends the ScpiInstrument base class.

//If you do NOT have a SCPI based instrument, you should modify this instance to extend
//the (less powerful) Instrument base class.

namespace S_Series_Oscilloscope
{
    [Display("Oscilloscope", Group: "S_Series_Oscilloscope", Description: "Adds Keysight Oscilloscope Instruments")]
    public class Oscilloscope : ScpiInstrument
    {

        #region Settings

        private string modelNo;

        #endregion

        public Oscilloscope()
        {
            Name = "Oscillocope";
            VisaAddress = "Simulte";
            // ToDo: Set default values for properties / settings.
        }

        /// <summary>
        /// Open procedure for the instrument.
        /// </summary>
        public override void Open()
        {

            base.Open();
            // TODO:  Open the connection to the instrument here

            modelNo = ScpiQuery<System.String>(Scpi.Format(":MODel?"), true);
            Log.Info("Instrument Detected: " + modelNo);
            //if (!IdnString.Contains("Instrument ID"))
            //{
            //    Log.Error("This instrument driver does not support the connected instrument.");
            //    throw new ArgumentException("Wrong instrument type.");
            // }

        }

        /// <summary>
        /// Close procedure for the instrument.
        /// </summary>
        public override void Close()
        {
            // TODO:  Shut down the connection to the instrument here.
            base.Close();
        }
        public void AutoScale()
        {
            ScpiCommand(":AUToscale");
        }

        public void ResetInstrument()
        {
            ScpiCommand("*RST");
            ScpiCommand(":SYSTem:PRESet DEF");
        }


        public void VerticalSetting(uint ChannelNo, EUnits Units, double VScale, double VRange, double VOffset, bool VInvert)
        {
            ScpiCommand(":CHANnel{0}:UNITs {1}", ChannelNo, Units);
            ScpiCommand(":CHANnel{0}:SCALe {1}", ChannelNo, VScale);
            ScpiCommand(":CHANnel{0}:RANGe {1}", ChannelNo, VRange);
            ScpiCommand(":CHANnel{0}:OFFSet {1}", ChannelNo, VOffset);
            //ScpiCommand(":CHANnel{0}:LABel {1}", ChannelNo, VLable);
            ScpiCommand(":CHANnel{0}:INVert {1}", ChannelNo, VInvert);
        }


        public void VerticalSetting(double TimeRange, ETimeReference TimeReference, double TimeReferencePercent,
            bool TimeRollMode, double TimeScale, double TimeWindowDelay, double TimeWindowPosition,
            double TimeWindowRange, double TimeWindowScale)
        {
            ScpiCommand(":TIMebase:RANGe {0}", TimeRange);
            if (TimeReference != ETimeReference.PERCent)
            {
                ScpiCommand(":TIMebase:REFerence {0}", TimeReference);
            }
            else
            {
                ScpiCommand(":TIMebase:REFerence:PERCent {0}", TimeReferencePercent);
            }
            ScpiCommand(":TIMebase:ROLL:ENABle {0}", TimeRollMode);
            ScpiCommand(":TIMebase:SCALe {0}", TimeScale);
            ScpiCommand(":TIMebase:WINDow:DELay {0}", TimeWindowDelay);
            ScpiCommand(":TIMebase:WINDow:POSition {0}", TimeWindowPosition);
            ScpiCommand(":TIMebase:WINDow:RANGe {0}", TimeWindowRange);
            ScpiCommand(":TIMebase:WINDow:SCALe {0}", TimeWindowScale);
        }
    }
}
