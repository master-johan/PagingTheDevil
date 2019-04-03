using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Paging_the_devil
{
    enum States { GoingDown, GoingUp, None}
    public class MenuManager
    {
        Game1 game;
        Button playBtn, exitBtn;
        public int selectedBtn;
        Pointer pointer;
        List<Button> buttonList = new List<Button>();
        GamePadState controller;
        Vector2 pointerPos;

    

        States current, previous;

        public MenuManager(GraphicsDevice graphicsDevice, Game1 game)
        {
            this.game = game;

          
            
           
            buttonList.Add(new Button(TextureManager.menuTextureList[0], graphicsDevice, new Vector2(400,500)));
            
           
            buttonList.Add(new Button(TextureManager.menuTextureList[2], graphicsDevice, new Vector2(400,300)));

            pointerPos = new Vector2(buttonList[0].GetPos.X - 200, buttonList[0].GetPos.Y + 10);
            pointer = new Pointer(TextureManager.menuTextureList[5], pointerPos);
            selectedBtn = 0;
        }

        public void Update(GameTime gameTime)
        {
            
            switch (GameManager.currentState)
            {
                case GameState.MainMenu:
                    foreach (var b in buttonList)
                    {
                        b.Update();
                    }

                    
                    previous = current;

                    if (controller.ThumbSticks.Left.Y < 0.5)
                        current = States.GoingUp;
                    else if (controller.ThumbSticks.Left.Y > -0.5)
                        current = States.GoingDown;
                    else
                        current = States.None;

                    for (int i = 0; i < buttonList.Count; i++)
                    {
                        if (current == States.GoingDown && previous != States.GoingDown && selectedBtn < (buttonList.Count -1))
                        {
                            selectedBtn++;
                            
                        }
                        buttonList[i].activeButton = true;
                    }



                    break;
                case GameState.PlayerSelect:
                    break;
                case GameState.InGame:
                    break;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            switch (GameManager.currentState)
            {
                case GameState.MainMenu:
                    spriteBatch.Draw(TextureManager.menuTextureList[3], Vector2.Zero, Color.White);
                    spriteBatch.Draw(TextureManager.menuTextureList[4], new Vector2(100, 0), Color.White);

                    foreach (var b in buttonList)
                    {
                        b.Draw(spriteBatch);
                    }
                    //playBtn.Draw(spriteBatch);
                    //exitBtn.Draw(spriteBatch);
                    pointer.Draw(spriteBatch);
                    break;
                case GameState.PlayerSelect:
                    break;
                case GameState.InGame:
                    break;

            }

        }
        public void GetController(GamePadState c)
        {
            controller = c;
        }

        public void ButtonClick()
        {

            if (exitBtn.isClicked == true)
            {
                game.Exit();
            }
        }
    }
}
