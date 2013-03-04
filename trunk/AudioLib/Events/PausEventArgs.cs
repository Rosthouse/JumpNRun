namespace AudioLib.Events
{
    public class PausEventArgs
    {
        public readonly double time;

        public PausEventArgs(double time)
        {
            this.time = time;
        }
    }
}
