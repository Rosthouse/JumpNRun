using System.Collections.Generic;
using JumpNRunShared.WorldObjects;

namespace JumpNRunShared.TimeTravel
{
    

    public class TimeStampManager
    {
        private LinkedList<TimeStamp> timeStamps;
        private int max;
        private LinkedListNode<TimeStamp> index;

        private Dictionary<MovingObject, TimeStampList> timeStampObjects;

        public TimeStampManager(int max)
        {
            this.max = max;
            this.timeStamps = new LinkedList<TimeStamp>();
            timeStampObjects = new Dictionary<MovingObject, TimeStampList>();
        }

        /// <summary>
        /// Default constructor, creates a TimeStampManager with the size 100
        /// </summary>
        public TimeStampManager():this(100){}
    }
}
