using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JumpNRunShared.WorldObjects;

namespace JumpNRunShared.TimeTravel
{
    public class TimeStampEnumerator:IEnumerator<TimeStamp>
    {
        private TimeStampList _timeStampList;
        private LinkedListNode<TimeStamp> index;

        public TimeStampEnumerator(TimeStampList timeStampList)
        {
            this._timeStampList = timeStampList;
            index = timeStampList.First;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if(index != null)
            {
                if(index.Next != null)
                {
                    index = index.Next;
                    return true;
                }
            }
            

            return false;
        }

        public void Reset()
        {
            index = _timeStampList.First;
        }

        public TimeStamp Current
        {
            get { return index.Value; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }
    }

    public class TimeStampList: IEnumerable<TimeStamp>
    {
        MovingObject _timeTravelObject;
        private LinkedList<TimeStamp> timeStamps;
        private LinkedListNode<TimeStamp> index;

        private int max;

        public TimeStampList(int max)
        {
            this.max = max;
            this.timeStamps = new LinkedList<TimeStamp>();
        }

        public TimeStampList():this(1000){}

        public LinkedListNode<TimeStamp> Index
        {
            get
            {
                if (index != null)
                {
                    return index;
                }

                return null;
            }

            set
            {
                this.index = value;
            }
        }

        public LinkedListNode<TimeStamp> First
        {
            get { return timeStamps.First; }
        }

        public bool Contains(MovingObject obj)
        {
            if(_timeTravelObject.Equals(obj))
            {
                return true;
            }

            return false;
        }

        public TimeStamp Pop()
        {

            TimeStamp timeStamp = timeStamps.First.Value;
            timeStamps.RemoveFirst();

            return timeStamp;
        }

        public TimeStamp Peek()
        {
            return timeStamps.First.Value;
        }

        public void AddFirstBefore(TimeStamp timeStamp, int time)
        {
            this.BatchPop(time);
            Push(timeStamp);
        }

        public TimeStamp BatchPop(int time)
        {
            TimeStamp timeStamp = this.Pop();

            while (timeStamp.Time <= time)
            {
                timeStamp = this.Pop();
            }
            index = timeStamps.First;

            return timeStamp;
        }

        public TimeStamp ElementAt(int index)
        {
            return timeStamps.ElementAt(index);
        }

        public void Push(TimeStamp timeStamp)
        {
            if (timeStamps.Count > max)
            {
                timeStamps.RemoveLast();
            }

            timeStamps.AddFirst(timeStamp);
            index = timeStamps.First;
        }


        public TimeStamp ReplaceTimeStamp(TimeStamp timeStamp, int time)
        {
            LinkedListNode<TimeStamp> toReplace = FindTimeStamp(time);

            if (toReplace != null)
            {
                timeStamps.AddBefore(toReplace, timeStamp);

                if (timeStamps.Remove(toReplace.Value))
                {
                    return toReplace.Value;
                }
            }


            throw new Exception("Could not replace timestamp");

        }

        public LinkedListNode<TimeStamp> FindTimeStamp(int time)
        {
            LinkedListNode<TimeStamp> timeStamp = timeStamps.First;
            int index = 0;

            while (timeStamp.Value.Time <= time)
            {
                index++;
                timeStamp = new LinkedListNode<TimeStamp>(timeStamps.ElementAt(index));
            }

            return timeStamp;
        }

        public void SwapFirstTimeStamp(TimeStamp timeStamp)
        {
            if (timeStamps.Count > 0)
            {
                TimeStamp returnStamp = timeStamps.First.Value;
                timeStamps.RemoveFirst();


            }

            timeStamps.AddFirst(timeStamp);
        }

        public TimeStamp Navigate(int direction)
        {
            if (direction > 0)
            {
                if (index.Previous != null)
                {
                    LinkedListNode<TimeStamp> next = index.Previous;
                    index = next;

                    return index.Value;
                }

            }
            else if (direction < 0)
            {
                if (index.Next != null)
                {
                    LinkedListNode<TimeStamp> next = index.Next;
                    index = next;
                    return index.Value;
                }

            }

            return index.Value;
        }

        public void BatchPush(params TimeStamp[] timeStamps)
        {
            foreach (var timeStamp in timeStamps)
            {
                this.Push(timeStamp);
            }
        }

        public LinkedListNode<TimeStamp> Index1()
        {
            if (index != null)
            {
                return index;
            }

            return null;
        }

        public IEnumerator<TimeStamp> GetEnumerator()
        {
            return new TimeStampEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
