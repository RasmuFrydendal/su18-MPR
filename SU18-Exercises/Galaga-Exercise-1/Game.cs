using DIKUArcade;

namespace Galaga_Exercise_1
{
    public class Game
    {
        private Window win;

        public Game()
        {

            win = new Window("Galaga", 500, AspectRatio.R1X1);

        }



        public void GameLoop()
        {
            while (win.IsRunning())
            {
                win.PollEvents();
                win.Clear();
            }


            win.SwapBuffers();
        }
    }
}
