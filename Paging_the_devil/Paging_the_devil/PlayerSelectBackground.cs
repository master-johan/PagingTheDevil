using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;

namespace Paging_the_devil
{
    class PlayerSelectBackground
    {
        List<Vector2> cloudOneTex, cloudTwoTex, cloudThreeTex, birdTex;
        int cloudOneSpacing, cloudTwoSpacing, cloudThreeSpacing, birdSpacing;
        int frame;

        double timer, interval;

        float cloudOneSpeed, cloudTwoSpeed, cloudThreeSpeed, birdSpeed;
        Rectangle Size;
        Rectangle srcRect;

        public PlayerSelectBackground()
        {
            Size = new Rectangle(0, 0, TextureManager.WindowSizeX, TextureManager.WindowSizeY);
            srcRect = new Rectangle(0, 0, 94, 92);

            InitializeCloudOne();
            InitializeCloudTwo();
            InitializeCloudThree();
            InitializeBird();
        }

        public void Update(GameTime gameTime)
        {
            UpdateHighClouds();

            UpdateMidClouds();

            UpdateLowClouds();

            BirdAnimation(gameTime);

            UpdateBird();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.playerSelectBackgroundList[0], Size, Color.White);

            foreach (Vector2 v in cloudOneTex)
            {
                spriteBatch.Draw(TextureManager.playerSelectBackgroundList[1], v, Color.White);
            }

            foreach (Vector2 v in cloudTwoTex)
            {
                spriteBatch.Draw(TextureManager.playerSelectBackgroundList[2], v, Color.White);
            }

            foreach (Vector2 v in cloudThreeTex)
            {
                spriteBatch.Draw(TextureManager.playerSelectBackgroundList[6], v, Color.White);
            }

            spriteBatch.Draw(TextureManager.playerSelectBackgroundList[4], Size, Color.White);

            foreach (Vector2 v in birdTex)
            {
                spriteBatch.Draw(TextureManager.playerSelectBackgroundList[7], v, srcRect, Color.White);
            }

            spriteBatch.Draw(TextureManager.playerSelectBackgroundList[3], Size, Color.White);
            spriteBatch.Draw(TextureManager.playerSelectBackgroundList[5], Size, Color.White);
            spriteBatch.Draw(TextureManager.playerSelectBackgroundList[8], Size, Color.White);
        }

        private void InitializeCloudOne()
        {
            cloudOneTex = new List<Vector2>();
            cloudOneSpacing = TextureManager.WindowSizeX;
            cloudOneSpeed = 0.2F;

            for (int i = 0; i < (TextureManager.WindowSizeX) + 2; i++)
            {
                cloudOneTex.Add(new Vector2(i * cloudOneSpacing - 500, 50));
            }
        }
        private void InitializeCloudTwo()
        {
            cloudTwoTex = new List<Vector2>();
            cloudTwoSpacing = TextureManager.WindowSizeX;
            cloudTwoSpeed = 0.05f;

            for (int i = 0; i < (TextureManager.WindowSizeX) + 2; i++)
            {
                cloudTwoTex.Add(new Vector2(i * cloudTwoSpacing - 500, 370));
            }
        }
        private void InitializeCloudThree()
        {
            cloudThreeTex = new List<Vector2>();
            cloudThreeSpacing = TextureManager.WindowSizeX;
            cloudThreeSpeed = 0.1f;

            for (int i = 0; i < (TextureManager.WindowSizeX) + 2; i++)
            {
                cloudThreeTex.Add(new Vector2(i * cloudTwoSpacing - 500, 230));
            }
        }
        private void InitializeBird()
        {
            birdTex = new List<Vector2>();
            birdSpacing = TextureManager.WindowSizeX * 2;
            birdSpeed = 2f;

            for (int i = 0; i < (TextureManager.WindowSizeX) + 2; i++)
            {
                birdTex.Add(new Vector2(i * birdSpacing, 650));
            }
        }

        private void UpdateLowClouds()
        {
            for (int i = 0; i < cloudTwoTex.Count; i++)
            {
                cloudTwoTex[i] = new Vector2(cloudTwoTex[i].X - cloudTwoSpeed, cloudTwoTex[i].Y);

                if (cloudTwoTex[i].X <= -cloudTwoSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = cloudTwoTex.Count - 1;
                    }
                    cloudTwoTex[i] = new Vector2(cloudTwoTex[j].X + cloudTwoSpacing - 1, cloudTwoTex[i].Y);
                }
            }
        }
        private void UpdateHighClouds()
        {
            for (int i = 0; i < cloudOneTex.Count; i++)
            {
                cloudOneTex[i] = new Vector2(cloudOneTex[i].X - cloudOneSpeed, cloudOneTex[i].Y);

                if (cloudOneTex[i].X <= -cloudOneSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = cloudOneTex.Count - 1;
                    }

                    cloudOneTex[i] = new Vector2(cloudOneTex[j].X + cloudOneSpacing - 1, cloudOneTex[i].Y);
                }
            }
        }
        private void UpdateMidClouds()
        {
            for (int i = 0; i < cloudThreeTex.Count; i++)
            {
                cloudThreeTex[i] = new Vector2(cloudThreeTex[i].X - cloudThreeSpeed, cloudThreeTex[i].Y);

                if (cloudThreeTex[i].X <= -cloudThreeSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = cloudThreeTex.Count - 1;
                    }

                    cloudThreeTex[i] = new Vector2(cloudThreeTex[j].X + cloudThreeSpacing - 1, cloudThreeTex[i].Y);
                }
            }
        }
        private void UpdateBird()
        {
            for (int i = 0; i < birdTex.Count; i++)
            {
                birdTex[i] = new Vector2(birdTex[i].X + birdSpeed, birdTex[i].Y);

                if (birdTex[i].X >= birdSpacing)
                {
                    int j = i - 1;

                    if (j < 0)
                    {
                        j = birdTex.Count - 1;
                    }
                    birdTex[i] = new Vector2(birdTex[j].X - birdSpacing, birdTex[i].Y);
                }
            }
        }

        public void BirdAnimation(GameTime gameTime)
        {
            timer -= (float)gameTime.ElapsedGameTime.TotalDays;

            if (timer <= 0)
            {
                timer = interval; frame++;
                srcRect.X = (frame % 9) * 100;
            }
        }
    }
}
