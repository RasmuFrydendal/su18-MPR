using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GalagaStates {

    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        
        private Entity backgroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        private Game game;

        public MainMenu() {
            InitializeGameState();
        }
        
        public void GameLoop() {} //This aint no game yet!

        public void InitializeGameState() {
            MainMenu.instance = this;
            game = new Game();
            
            backgroundImage = 
                new Entity(new StationaryShape(new Vec2F(0,0), new Vec2F(1,1)), 
                    new Image( "Assets\\Images\\TitleImage.png"));
            
            menuButtons = new Text[]{
                new Text(
                    "Start",                //name
                    new Vec2F(0.5f,0.5f),  //pos
                    new Vec2F(0.2f,0.2f)  //ext
                ),
                new Text(
                    "Quit",
                    new Vec2F(0.5f,0.4f),
                    new Vec2F(0.2f,0.2f)
                )
            };
            maxMenuButtons = menuButtons.Length-1;

        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {
            backgroundImage.RenderEntity();
            foreach (var t in menuButtons) {
                t.RenderText();
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyAction) {
                case "KEY_PRESS":
                    switch (keyValue) {
                        case "KEY_DOWN":
                            if (activeMenuButton < maxMenuButtons) {
                                activeMenuButton += 1;
                            }
                            break;
                        case "KEY_UP":
                            if (activeMenuButton > 0) {
                                activeMenuButton -= 1;
                            }
                            break;
                        case "ENTER":
                            GalagaBus.GetBus();
                            break;
                    }
                    break;
                case "KEY_RELEASE":
                    break;
            }
        }

        public static MainMenu GetInstance() {
            return MainMenu.instance ?? new MainMenu();
        }
    }
}