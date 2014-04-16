using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Helios.Model
{
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

        public Clip()
        {
            this.ClipDuration = new TimeSpan();
            this.StartTime = new TimeSpan();
            this.EndTime = new TimeSpan();
        }

        #endregion
    }
}
