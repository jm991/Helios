using Helios.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helios.Model
{
    [TypeConverter(typeof(ClipConverter))]
    public class Clip : ModelBase
    {
        #region Property-behind variables (private)

        private TimeSpan _clipDuration;
        private TimeSpan _startTime;
        private TimeSpan _endTime;

        #endregion

        #region Properties

        /// <summary>
        /// Duration of video
        /// </summary>
        public TimeSpan ClipDuration
        {
            get { return _clipDuration; }
            set
            {
                if (value != _clipDuration)
                {
                    _clipDuration = value;
                    PropChanged("ClipDuration");
                }
            }
        }

        /// <summary>
        /// End trim point for a clip
        /// </summary>
        public TimeSpan StartTime
        {
            get { return _startTime; }
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    PropChanged("StartTime");
                }
            }
        }

        /// <summary>
        /// End trim point for a clip
        /// </summary>
        public TimeSpan EndTime
        {
            get { return _endTime; }
            set
            {
                if (value != _endTime)
                {
                    _endTime = value;
                    PropChanged("EndTime");
                }
            }
        }

        #endregion


        #region Constructors

        // Default constructor is required for usage as sample data 
        // in the WPF and Silverlight Designer.
        public Clip()
        {
            this.ClipDuration = new TimeSpan();
            this.StartTime = new TimeSpan();
            this.EndTime = new TimeSpan();
        }

        public Clip(double start, double end, double duration)
        {
            StartTime = TimeSpan.FromSeconds(start);
            EndTime = TimeSpan.FromSeconds(end);
            ClipDuration = TimeSpan.FromSeconds(duration);
        }

        public Clip(string value)
        {
            // TODO: add better error handling
            string[] timeStrs = ((string)value).Split(new char[] { ',' });
            TimeSpan[] times = new TimeSpan[3];

            for (int i = 0; i < timeStrs.Length; i++)
            {
                times[i] = TimeSpan.Parse(timeStrs[i]);
            }

            StartTime = times[0];
            EndTime = times[1];
            ClipDuration = times[2];
        }

        #endregion


        #region Methods

        public override string ToString()
        {
            return StartTime.ToString() + " " + EndTime.ToString() + " " + ClipDuration.ToString();
        }

        #endregion
    }

    // The ClipCollection class defines a simple collection
    // for Clip business objects.
    public class ClipCollection : List<Clip>
    {
        // Default constructor is required for usage in the WPF Designer.
        public ClipCollection() { }
    }
}
