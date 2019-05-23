using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Paging_the_devil.GameObject;
using Microsoft.Xna.Framework.Media;
using Paging_the_devil.GameObject.EnemyFolder;
using Paging_the_devil.GameObject.Characters;
using Paging_the_devil.GameObject.Abilities;

namespace Paging_the_devil.Manager
{
    public enum GameState { StoryScreen, MainMenu, Controls, PlayerSelect, InGame, Win, GameOver, CharacterInfo }

    class GameManager
    {
        public static GameState currentState;

        GraphicsDevice graphicsDevice;

        GraphicsDeviceManager graphics;

        MenuManager menuManager;

        Game1 game;

        RoomManager roomManager;

        LevelManager levelManager;

        Room currentRoom;

        Controller[] controllerArray;

        Player[] playerArray;

        List<Enemy> enemyList;

        InfoBox infoBox;

        int nrOfPlayers;

        bool blockSound;
        bool roomManagerCreated;
        bool hudManagerCreated;

        bool[] connectedController;

        public HUDManager HUDManager { get; set; }

        public GameManager(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphics, Game1 game)
        {
            this.graphicsDevice = graphicsDevice;
            this.graphics = graphics;
            this.game = game;

            SetWindowSize(graphics);

            menuManager = new MenuManager(graphicsDevice, game);
            levelManager = new LevelManager();

            enemyList = new List<Enemy>();
            blockSound = false;

            currentState = GameState.MainMenu;

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(SoundBank.BgMusicList[1]);

            ArrayGenerator();

            menuManager = new MenuManager(graphicsDevice, game);

            ConnectController();

            SetWindowSize(graphics);

            infoBox = new InfoBox(TextureBank.hudTextureList[19], new Vector2(768, 0), enemyList);
        }
        public void Update(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.StoryScreen:
                    ConnectController();
                    for (int i = 0; i < nrOfPlayers; i++)
                    {
                        if (controllerArray[i].ButtonPressed(Buttons.X))
                        {
                            currentState = GameState.InGame;
                            MediaPlayer.Play(SoundBank.BgMusicList[0]);
                        }
                    }
                    if (controllerArray[0] != null)
                    {
                        SendControllerToMenu();
                        controllerArray[0].Update();
                        menuManager.Update(gameTime);
                    }
                    break;
                case GameState.MainMenu:

                    ConnectController();

                    if (controllerArray[0] != null)
                    {
                        SendControllerToMenu();
                        controllerArray[0].Update();
                        menuManager.Update(gameTime);
                    }

                    DisconnectController();
                    break;
                case GameState.Controls:
                    menuManager.Update(gameTime);
                    controllerArray[0].Update();
                    break;
                case GameState.PlayerSelect:
                    ConnectController();
                    UpdateController();
                    SendPlayerToMenu();
                    playerArray = menuManager.PlayerSelectManager.PlayerArray;
                    menuManager.Update(gameTime);
                    DisconnectController();

                    break;
                case GameState.CharacterInfo:
                    menuManager.Update(gameTime);
                    UpdateController();
                    break;
                case GameState.InGame:
                    infoBox.Update(gameTime);
                    if (!menuManager.gamePaused)
                    {
                        if (HUDManager == null)
                        {
                            HUDManager = menuManager.PlayerSelectManager.HUDManager;
                            hudManagerCreated = true;
                        }

                        if (roomManager == null)
                        {
                            CreateRoomManager();
                            currentRoom = roomManager.CurrentRoom;
                        }
                        //Ta bort i slutprodukten 
                        if (Keyboard.GetState().IsKeyDown(Keys.A))
                        {
                            for (int i = 0; i < nrOfPlayers; i++)
                            {
                                playerArray[i].HealthPoints -= 0.5f;
                            }
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.S))
                        {
                            for (int i = 0; i < nrOfPlayers; i++)
                            {
                                playerArray[i].HealthPoints += 0.5f;
                            }
                        }

                        for (int i = 0; i < nrOfPlayers; i++)
                        {
                            CheckPlayerAbilites(playerArray[i].abilityList, playerArray[i]);
                        }

                        for (int i = 0; i < nrOfPlayers; i++)
                        {
                            if (controllerArray[i].ButtonPressed(Buttons.Start))
                            {
                                menuManager.gamePaused = true;
                            }
                        }

                        foreach (var e in enemyList)
                        {
                            if (e is SmallDevil)
                            {
                                CheckEnemiesAbilites((e as SmallDevil).enemyAbilityList);
                            }
                            if (e is WallSpider)
                            {
                                CheckEnemiesAbilites((e as WallSpider).enemyAbilityList);
                            }
                            if (e is Slime)
                            {
                                CheckSlimeCollision(e as Slime);
                            }
                            if (e is Devil)
                            {
                                CheckEnemiesAbilites((e as Devil).enemyAbilityList);
                            }
                        }

                        HUDManager.Update(gameTime);
                        UpdatePlayersDirection();
                        CharacterÚpdate(gameTime);
                        DeleteAbilities();
                        roomManager.Update(gameTime);
                        CheckIfGameOver();
                        CheckIfWin();
                    }
                    else
                    {
                        controllerArray[0].Update();
                        menuManager.Update(gameTime);
                    }
                    break;
                case GameState.Win:
                    UpdateController();
                    StartToRestart();
                    menuManager.Update(gameTime);
                    break;
                case GameState.GameOver:
                    UpdateController();
                    StartToRestart();
                    menuManager.Update(gameTime);
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (currentState)
            {
                case GameState.StoryScreen:
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.MainMenu:
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.PlayerSelect:
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.CharacterInfo:
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.Controls:
                    menuManager.Draw(spriteBatch);
                    break;
                case GameState.InGame:

                    if (roomManagerCreated)
                    {
                        roomManager.Draw(spriteBatch);
                    }

                    DrawCharacters(spriteBatch);

                    if (hudManagerCreated)
                    {
                        HUDManager.Draw(spriteBatch);
                    }

                    if (menuManager.gamePaused)
                    {
                        menuManager.Draw(spriteBatch);
                    }

                    infoBox.Draw(spriteBatch);
                    SendEnemyList();

                    break;
                case GameState.Win:
                    graphicsDevice.Clear(Color.Black);
                    menuManager.Draw(spriteBatch);

                    spriteBatch.Draw(TextureBank.menuTextureList[22], new Vector2(ValueBank.WindowSizeX / 2 - TextureBank.menuTextureList[22].Width / 2,
                    ValueBank.WindowSizeY / 2 - TextureBank.menuTextureList[22].Height / 2), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                    spriteBatch.Draw(TextureBank.menuTextureList[23], new Vector2(ValueBank.WindowSizeX / 2 - TextureBank.menuTextureList[23].Width / 2,
                    (ValueBank.WindowSizeY / 2 - TextureBank.menuTextureList[23].Height / 2) + 300), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                    break;
                case GameState.GameOver:
                    graphicsDevice.Clear(Color.Black);
                    menuManager.Draw(spriteBatch);

                    spriteBatch.Draw(TextureBank.menuTextureList[21], new Vector2(ValueBank.WindowSizeX / 2 - TextureBank.menuTextureList[21].Width / 2,
                    ValueBank.WindowSizeY / 2 - TextureBank.menuTextureList[21].Height / 2), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                    spriteBatch.Draw(TextureBank.menuTextureList[23], new Vector2(ValueBank.WindowSizeX / 2 - TextureBank.menuTextureList[23].Width / 2,
                    (ValueBank.WindowSizeY / 2 - TextureBank.menuTextureList[23].Height / 2) + 200), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0.3f);
                    break;
            }
        }
        /// <summary>
        /// Den här metoden sätter fönstrets storlek
        /// </summary>
        /// <param name="graphics"></param>
        private static void SetWindowSize(GraphicsDeviceManager graphics)
        {
            graphics.PreferredBackBufferHeight = ValueBank.WindowSizeY = 1080;
            graphics.PreferredBackBufferWidth = ValueBank.WindowSizeX = 1920;
            graphics.ApplyChanges();
            ValueBank.GameWindowStartY = 135;
        }
        /// <summary>
        /// Den här metoden anger värden till olika Arrays och skapar portaler.
        /// </summary>
        private void ArrayGenerator()
        {
            controllerArray = new Controller[4];
            connectedController = new bool[4];
            playerArray = new Player[4];
        }

        /// <summary>
        /// Den här metoden uppdaterar spelare samt enemies samt tar bort enemies vid död.
        /// </summary>
        private void CharacterÚpdate(GameTime gameTime)
        {
            Enemy toRemoveEnemy = null;

            foreach (var e in enemyList)
            {
                e.Update(gameTime);

                if (e.toRevome)
                {
                    toRemoveEnemy = e;
                }

                if (e.TrapTimer < 0)
                {
                    e.HitBySlowTrap = false;
                    e.MovementSpeed = e.BaseMoveSpeed;
                }
            }

            for (int i = 0; i < nrOfPlayers; i++)
            {
                controllerArray[i].Update();
                playerArray[i].Update(gameTime);

            }

            if (toRemoveEnemy != null)
            {
                //Gör så att när spindeln dör så återställs spelarnas speed till den vanliga.
                if (toRemoveEnemy is WallSpider)
                {
                    for (int i = 0; i < toRemoveEnemy.enemyAbilityList.Count; i++)
                    {
                        for (int j = 0; j < (toRemoveEnemy.enemyAbilityList[i] as WebBall).playerList.Count; j++)
                        {
                            (toRemoveEnemy.enemyAbilityList[i] as WebBall).playerList[j].movementSpeed = ValueBank.PlayerSpeed;
                        }
                    }
                }
                enemyList.Remove(toRemoveEnemy);
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar spelarens riktning samt senaste riktning.
        /// </summary>
        private void UpdatePlayersDirection()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (controllerArray[i].GetDirection() != Vector2.Zero)
                {
                    playerArray[i].LastInputDirection(controllerArray[i].GetDirection());
                }

                playerArray[i].InputDirection(controllerArray[i].GetDirection());
            }
        }
        /// <summary>
        /// Den här metoden uppdaterar kontrollerna.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void UpdateController()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                controllerArray[i].Update();
            }
        }
        /// <summary>
        /// Den här metoden ritar ut spelare samt enemies.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawCharacters(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                playerArray[i].Draw(spriteBatch);

                DrawPlayerRing(spriteBatch);
            }

            foreach (var e in enemyList)
            {
                e.Draw(spriteBatch);
            }
        }
        /// <summary>
        /// Denna metod ritar ut en player ring under spelaren.
        /// </summary>
        /// <param name="spriteBatch"></param>
        private void DrawPlayerRing(SpriteBatch spriteBatch)
        {
            if (playerArray[0] is Player)
            {
                spriteBatch.Draw(TextureBank.playerTextureList[4], playerArray[0].GetRect, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
            if (playerArray[1] is Player)
            {
                spriteBatch.Draw(TextureBank.playerTextureList[4], playerArray[1].GetRect, null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
            if (playerArray[2] is Player)
            {
                spriteBatch.Draw(TextureBank.playerTextureList[4], playerArray[2].GetRect, null, Color.LimeGreen, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
            if (playerArray[3] is Player)
            {
                spriteBatch.Draw(TextureBank.playerTextureList[4], playerArray[3].GetRect, null, Color.Yellow, 0, Vector2.Zero, SpriteEffects.None, 0.4f);
            }
        }

        /// <summary>
        /// Den här metoden ansluter en kontroll.
        /// </summary>
        private void ConnectController()
        {
            for (int i = 0; i < GamePad.MaximumGamePadCount; i++)
            {
                if (GamePad.GetState(i).IsConnected && !connectedController[i])
                {
                    PlayerIndex index = (PlayerIndex)i;

                    controllerArray[i] = new Controller(index);
                    nrOfPlayers++;
                    connectedController[i] = true;
                }
            }
        }
        /// <summary>
        /// Denna metoden kopplar från kontrollerna
        /// </summary>
        private void DisconnectController()
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (!GamePad.GetState(i).IsConnected && connectedController[i])
                {
                    controllerArray[i] = null;
                    nrOfPlayers--;
                    connectedController[i] = false;
                }
            }
        }
        /// <summary>
        /// Den här metoden skapar en roomManager
        /// </summary>
        public void CreateRoomManager()
        {
            roomManager = new RoomManager(playerArray, nrOfPlayers, enemyList, levelManager);
            roomManagerCreated = true;
        }
        /// <summary>
        /// Den här metoden uppdaterar menumanagerns controllerArray.
        /// </summary>
        private void SendControllerToMenu()
        {
            menuManager.GetController(controllerArray);
        }
        /// <summary>
        /// Den här metoden uppdaterar menumanagerns playerArray.
        /// </summary>
        private void SendPlayerToMenu()
        {
            menuManager.GetPlayer(nrOfPlayers);
        }
        /// <summary>
        /// Den här metoden tar bort abilities vid interaktion med enemies.
        /// </summary>
        private void DeleteAbilities()
        {
            Ability toRemove = null;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                foreach (var a in playerArray[i].abilityList)
                {
                    foreach (var w in currentRoom.GetWallRectList())
                    {
                        if (a.GetRect.Intersects(w))
                        {
                            if (a.ToRemove == true)
                            {
                                toRemove = a;
                            }

                            if (a is Healharm || a is Arrow)
                            {
                                a.ToRemove = true;
                            }
                            else
                            {
                                a.ToRemove = false;
                            }
                        }

                        if (a.ToRemove == true)
                        {
                            toRemove = a;
                        }
                    }
                }

                if (toRemove != null)
                {
                    playerArray[i].abilityList.Remove(toRemove);
                }
            }
        }
        /// <summary>
        /// Den här metoden kollar interaktion mellan player-abilities och enemies
        /// </summary>
        /// <param name="abilityList"></param>
        /// <param name="player"></param>
        private void CheckPlayerAbilites(List<Ability> abilityList, Player player)
        {
            Ability toRemove = null;

            foreach (var a in abilityList)
            {
                foreach (var e in enemyList)
                {
                    if (a.GetRect.Intersects(e.GetRect))
                    {
                        a.HitCharacter = e;

                        if (a is Root)
                        {
                            (a as Root).enemyList.Add(e);
                        }

                        if (a is FlowerPower)
                        {

                        }
                        else
                        {
                            (a.HitCharacter as Enemy).Hit = true;
                        }
                    }
                }
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    if (a.GetRect.Intersects(playerArray[i].GetRect))
                    {
                        if (a is Healharm)
                        {
                            if (player == playerArray[i])
                            {
                                continue;
                            }
                            a.HitCharacter = playerArray[i];
                        }
                        else if (a is Block)
                        {
                            if (player == playerArray[i])
                            {
                                a.HitCharacter = playerArray[i];
                            }
                        }
                        else if (a is FlowerPower)
                        {
                            a.HitCharacter = playerArray[i];
                            //Lägger till i listan för att få med alla spelare till flowerpower-klassen.
                            (a as FlowerPower).playerList.Add(playerArray[i]);
                        }
                    }
                }

                if (a.ToRemove)
                {
                    toRemove = a;
                }
            }

            if (toRemove != null)
            {
                abilityList.Remove(toRemove);
            }
        }
        /// <summary>
        /// Den här metoden kollar interaktion mellan enemy-abilities och players
        /// </summary>
        /// <param name="abilityList"></param>
        private void CheckEnemiesAbilites(List<Ability> abilityList)
        {
            Ability toRemove = null;

            foreach (var a in abilityList)
            {
                for (int i = 0; i < nrOfPlayers; i++)
                {
                    if (a.GetRect.Intersects(playerArray[i].GetRect))
                    {
                        if (a is DevilCleave)
                        {
                            (a as DevilCleave).playerList.Add(playerArray[i]);
                        }

                        a.HitCharacter = playerArray[i];
                        (a.HitCharacter as Player).Hit = true;

                        if (a.HitCharacter is Player)
                        {
                            for (int j = 0; j < (a.HitCharacter as Player).abilityList.Count; j++)
                            {
                                if ((a.HitCharacter as Player).abilityList[j] is Block)
                                {
                                    if (!blockSound && !(a is WebBall))
                                    {
                                        //SoundBank.SoundEffectList[13].Play();
                                        blockSound = true;
                                    }
                                    else if ((a as WebBall).hasHit)
                                    {
                                        //SoundBank.SoundEffectList[13].Play();
                                        (a as WebBall).hasHit = false;
                                    }
                                }
                            }
                        }
                    }
                    blockSound = false;

                }


                foreach (var w in currentRoom.GetWallRectList())
                {
                    if (a.GetRect.Intersects(w))
                    {
                        a.ToRemove = true;
                    }
                }

                if (a.ToRemove == true)
                {
                    toRemove = a;
                }
            }



            if (toRemove != null)
            {
                if (toRemove is WebBall)
                {
                    if ((toRemove as WebBall).HitCharacter != null)
                    {
                        ((toRemove as WebBall).HitCharacter as Player).movementSpeed = ValueBank.PlayerSpeed;
                    }
                }
                abilityList.Remove(toRemove);
            }
        }
        /// <summary>
        /// Kollar kollisionen för slime och spelare.
        /// </summary>
        /// <param name="slime"></param>
        private void CheckSlimeCollision(Slime slime)
        {
            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (slime.GetRect.Intersects(playerArray[i].GetRect))
                {
                    playerArray[i].HealthPoints -= slime.Damage;
                }
            }
        }
        /// <summary>
        /// Den här metoden kollar ifall alla är döda
        /// </summary>
        private void CheckIfGameOver()
        {
            int hp = 0;

            for (int i = 0; i < nrOfPlayers; i++)
            {
                if (playerArray[i].HealthPoints != 0)
                {
                    hp++;
                }
            }

            if (hp == 0)
            {
                currentState = GameState.GameOver;
            }
        }
        /// <summary>
        /// Den här metoden kollar ifall man har vunnit
        /// </summary>
        private void CheckIfWin()
        {
            if (roomManager.CheckIfBossRoomDefeated())
            {
                currentState = GameState.Win;
            }
        }
        /// <summary>
        /// Den här metoden gör så att man kan starta om spelet
        /// </summary>
        private void StartToRestart()
        {
            if (controllerArray[0].ButtonPressed(Buttons.Start))
            {

                currentState = GameState.MainMenu;
                menuManager.PlayerSelectManager.Reset();
                roomManager.Reset();

                for (int j = 0; j < nrOfPlayers; j++)
                {
                    HUDManager.playerHudArray[j].Reset();
                }
                HUDManager.Reset();
                hudManagerCreated = false;
                HUDManager = null;
            }
        }
        private void SendEnemyList()
        {
            infoBox.GetEnemyList(enemyList);
        }
    }
}

