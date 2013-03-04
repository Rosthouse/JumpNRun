namespace JumpNRunClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (JumpNRun game = new JumpNRun())
            {
                game.Run();
            }
        }
    }
}

