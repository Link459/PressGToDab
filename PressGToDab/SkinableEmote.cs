using CustomKnight;
using UnityEngine;

namespace PressGToDab
{
    internal class SkinableEmote : Skinable_Tk2d
    {
        public SkinableEmote(string name) : base(name)
        {
        }
        public override Material GetMaterial()
        {
            var clip = HeroController.instance.gameObject.GetComponent<tk2dSpriteAnimator>().GetClipByName(this.name);
            if (clip.frames.Length > 0)
            {
                return clip.frames[0].spriteCollection.spriteDefinitions[0].material;
            }
            return null;
        }
    }
}
