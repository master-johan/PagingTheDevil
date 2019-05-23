using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paging_the_devil.Manager
{
    enum Characters { Knight, Barbarian, Druid, Ranger}

    class CharacterInfoManager
    {
        Controller[] controllerArray;

        Characters currentCharacter;

        Texture2D charTex;
        Texture2D roleTex;
        Texture2D infoTex;

        Rectangle characterPos;
        Rectangle characterInfoPos;
        Rectangle AbilityPos;

        Vector2 logoPos;
        Vector2 goBackTextPos;

        MainMenuBackground background;

        public CharacterInfoManager()
        {
            currentCharacter = Characters.Knight;

            logoPos = new Vector2(ValueBank.WindowSizeX /2 - TextureBank.menuTextureList[8].Width / 2, 50);
            goBackTextPos = new Vector2(15, 15);

            characterPos = new Rectangle(270, 380, 250, 300);
            characterInfoPos = new Rectangle(190, 730, 380, 350);
            AbilityPos = new Rectangle(1050, 500, 450, 450);
            
            background = new MainMenuBackground();  
        }

        public void Update(GameTime gameTime)
        {
            background.Update(gameTime);
            DecidingTextures();
            BrowseCharacters();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            DrawBackground(spritebatch);
            DrawCharacterAndAbilities(spritebatch);
        }

        /// <summary>
        /// Den här metoden ritar ut en karaktär, en roll och karaktärens abilities
        /// </summary>
        /// <param name="spritebatch"></param>
        private void DrawCharacterAndAbilities(SpriteBatch spritebatch)
        {
            if (infoTex != null && charTex != null && roleTex != null)
            {
                spritebatch.Draw(infoTex, AbilityPos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                spritebatch.Draw(charTex, characterPos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
                spritebatch.Draw(roleTex, characterInfoPos, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            }
        }
        /// <summary>
        /// Den här metoden ritar ut en rörlig backgrund samt "CharacterInfo" och Muren.
        /// </summary>
        /// <param name="spritebatch"></param>
        private void DrawBackground(SpriteBatch spritebatch)
        {
            background.Draw(spritebatch);
            spritebatch.Draw(TextureBank.characterInfoList[0], Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spritebatch.Draw(TextureBank.characterInfoList[1], logoPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spritebatch.Draw(TextureBank.menuTextureList[21], goBackTextPos, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
        /// <summary>
        /// Den här metoden väljer textur till karaktär, abilities och roll.
        /// </summary>
        private void DecidingTextures()
        {
            switch (currentCharacter)
            {
                case Characters.Knight:
                    charTex = TextureBank.characterInfoList[2];
                    roleTex = TextureBank.characterInfoList[6];
                    infoTex = TextureBank.characterInfoList[10];
                    break;
                case Characters.Barbarian:
                    charTex = TextureBank.characterInfoList[5];
                    roleTex = TextureBank.characterInfoList[9];
                    infoTex = TextureBank.characterInfoList[12];
                    break;
                case Characters.Druid:
                    charTex = TextureBank.characterInfoList[3];
                    roleTex = TextureBank.characterInfoList[7];
                    infoTex = TextureBank.characterInfoList[11];
                    break;
                case Characters.Ranger:
                    charTex = TextureBank.characterInfoList[4];
                    roleTex = TextureBank.characterInfoList[8];
                    infoTex = TextureBank.characterInfoList[13];
                    break;
            }
        }
        /// <summary>
        /// Den här metoden gör så att användaren kan bläddra mellan de 4 karaktärerna.
        /// </summary>
        private void BrowseCharacters()
        {
            if (controllerArray[0].ButtonPressed(Buttons.DPadLeft))
            {
                if (currentCharacter == 0)
                {
                    currentCharacter = Characters.Ranger;
                }

                else
                {
                    currentCharacter--;
                }
            }
            else if (controllerArray[0].ButtonPressed(Buttons.DPadRight))
            {
                if (currentCharacter == Characters.Ranger)
                {
                    currentCharacter = 0;
                }

                else
                {
                    currentCharacter++;
                }
            }
        }
        public void GetController(Controller[] controllerArray)
        {
            this.controllerArray = controllerArray;
        }
    }
}
