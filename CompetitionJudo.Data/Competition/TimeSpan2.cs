using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CompetitionJudo.Data
{
    [Serializable]
    public class TimeSpan2
    {
        // Local Variable
        private TimeSpan m_TimeSinceLastEvent;

        public TimeSpan2(int h,int m,int s)
        {
            m_TimeSinceLastEvent = new TimeSpan(h, m, s);
        }

        // Public Property - XmlIgnore as it doesn't serialize anyway
        [XmlIgnore]
        public TimeSpan TimeSinceLastEvent
        {
            get { return m_TimeSinceLastEvent; }
            set { m_TimeSinceLastEvent = value; }
        }

        public TimeSpan2()
        {

        }

        // Pretend property for serialization
        [XmlElement("Time")]
        public long TimeSinceLastEventTicks
        {
            get { return m_TimeSinceLastEvent.Ticks; }
            set { m_TimeSinceLastEvent = new TimeSpan(value); }
        }
    }
}
