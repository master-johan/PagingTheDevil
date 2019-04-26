using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paging_the_devil.Manager;
using Microsoft.Xna.Framework.Graphics;

namespace Paging_the_devil
{
    class MainMenuBackground
    {
        List<Vector2> cloudOneTex, cloudTwoTex, cloudThreeTex, birdTex;
        int cloudOneSpacing, cloudTwoSpacing, cloudThreeSpacing, birdSpacing;
        int frame;

        double timer, interval;

        float cloudOneSpeed, cloudTwoSpeed, cloudThreeSpeed;
        Rectangle Size;
        Rectangle srcRect;

        public MainMenuBackground()
        {
            Size = new Rectangle(0, 0, TextureManager.WindowSizeX, TextureManager.WindowSizeY);
            srcRect = new Rectangle(0, 0, 94, 92);

            InitializeCloudOne();
            InitializeCloudTwo();
            InitializeCloudThree();

        }

        public void Update(GameTime gameTime)
        {
            UpdateHighClouds();

            UpdateMidClouds();

            UpdateLowClouds();
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
    }
}
