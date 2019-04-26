using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Paging_the_devil.Manager
{
    class SoundBank
    {
        public static List<Song> BgMusicList;
        public static List<SoundEffect> SoundEffectList;

        public static void LoadSound(ContentManager Content)
        {
            BgMusicList = new List<Song>();
            SoundEffectList = new List<SoundEffect>();

            BgMusicList.Add( Content.Load<Song>("ThemeSong"));
            BgMusicList.Add(Content.Load<Song>("AltSong"));
            BgMusicList.Add(Content.Load<Song>("BossFightSong"));
        }
    }
}
