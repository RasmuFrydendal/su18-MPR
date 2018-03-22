namespace Galaga_Exercise_3 {
    internal class Program {
        public static void Main(string[] args) {
            MainMenu gameMenu = new MainMenu();
            gameMenu.InitializeGameState();
            gameMenu.GameLoop();
        }
    }
}