using System.Security.Cryptography;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga_Exercise_3 {

    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        
        private Entity backgroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;

        Game game = new Game();
        
        public void GameLoop() {
            RenderState();
        }

        public void InitializeGameState() {
            
            backgroundImage = 
                new Entity(new StationaryShape(new Vec2F(0,0), new Vec2F(1,1)), 
                    new Image( "TitleImage.png"));
            
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

        }

        public void UpdateGameLogic() {
            throw new System.NotImplementedException();
        }

        public void RenderState() {
            backgroundImage.RenderEntity();

            
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            throw new System.NotImplementedException();
        }
    }
}