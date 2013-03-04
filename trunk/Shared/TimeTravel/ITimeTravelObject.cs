using Microsoft.Xna.Framework;

namespace JumpNRunShared.TimeTravel
{
    public interface ITimeTravelObject
    {
        Vector3 TimePosition
        {
             get;  set;
        }

        TimeStampList TimeStamps
        {
            get; set;
        }
    }
}
