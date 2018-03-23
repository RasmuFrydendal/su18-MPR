using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga_Exercise_3.GalagaState {

    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        
        private Entity backgroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        private Vec3I textColour;
        private Vec3I textColourSelected;
        
        public MainMenu() {
            InitializeGameState();
        }
        
        public void GameLoop() {} //This aint no game yet!

        public void InitializeGameState() {
            
            backgroundImage = 
                new Entity(new StationaryShape(new Vec2F(0,0), new Vec2F(1,1)), 
                    new Image( "Assets\\Images\\TitleImage.png"));
            
            textColour = new Vec3I(255,255,255);
            textColourSelected = new Vec3I(255,0,0);
            
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
            menuButtons[0].SetColor(textColour);
            menuButtons[1].SetColor(textColourSelected);
            activeMenuButton = 0;
            maxMenuButtons = menuButtons.Length-1;

        }

        public void UpdateGameLogic() {}

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
                                menuButtons[activeMenuButton].SetColor(textColour);
                                activeMenuButton += 1;
                                menuButtons[activeMenuButton].SetColor(textColourSelected);
                            }
                            break;
                        case "KEY_UP":
                            if (activeMenuButton > 0) {
                                menuButtons[activeMenuButton].SetColor(textColour);
                                activeMenuButton -= 1;
                                menuButtons[activeMenuButton].SetColor(textColourSelected);
                            }
                            break;
                        case "KEY_ENTER":
                            GalagaBus.GetBus().RegisterEvent(
                                GameEventFactory<object>.CreateGameEventForAllProcessors(
                                    GameEventType.GameStateEvent, this, "GAME_STATE", "GAME_RUNNING", ""));
                            break;
                        default:
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