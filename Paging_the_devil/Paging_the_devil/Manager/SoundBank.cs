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

            SoundEffectList.Add(Content.Load<SoundEffect>("ArrowSound"));//0
            SoundEffectList.Add(Content.Load<SoundEffect>("ArrowHitSound"));//1
            SoundEffectList.Add(Content.Load<SoundEffect>("SlashSound"));//2
            SoundEffectList.Add(Content.Load<SoundEffect>("SlashMissSound"));//3
            SoundEffectList.Add(Content.Load<SoundEffect>("CleaveSound"));//4
            SoundEffectList.Add(Content.Load<SoundEffect>("CleaveMiss"));//5
            SoundEffectList.Add(Content.Load<SoundEffect>("DoorSound"));//6
            SoundEffectList.Add(Content.Load<SoundEffect>("TauntSound"));//7
            SoundEffectList.Add(Content.Load<SoundEffect>("DashSound"));//8
            SoundEffectList.Add(Content.Load<SoundEffect>("TrapSound"));//9
            SoundEffectList.Add(Content.Load<SoundEffect>("TrapHitSound"));//10

        }
    }
}
