using Modding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PressGToDab
{
    // Token: 0x02000002 RID: 2
    public class Emoter : MonoBehaviour
    {
        private void AddToCustomKnight(string name)
        {
            if (!CustomKnight.SkinManager.Skinables.ContainsKey(name))
            {
                CustomKnight.SkinManager.Skinables.Add(name, new SkinableEmote(name));
            }
        }
        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        private void Awake()
        {
            this._anim = HeroController.instance.gameObject.GetComponent<tk2dSpriteAnimator>();
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
        private void Start()
        {
            tk2dSpriteCollectionData collection = HeroController.instance.GetComponent<tk2dSprite>().Collection;
            List<tk2dSpriteDefinition> list = collection.spriteDefinitions.ToList<tk2dSpriteDefinition>();
            foreach (tk2dSpriteDefinition tk2dSpriteDefinition in PressGToDab.EmotesBundle.LoadAsset<GameObject>("EmotesCollection").GetComponent<tk2dSpriteCollection>().spriteCollection.spriteDefinitions)
            {
                tk2dSpriteDefinition.material.shader = list[0].material.shader;
                list.Add(tk2dSpriteDefinition);
            }
            collection.spriteDefinitions = list.ToArray();
            List<tk2dSpriteAnimationClip> list2 = this._anim.Library.clips.ToList<tk2dSpriteAnimationClip>();
            foreach (tk2dSpriteAnimationClip item in PressGToDab.EmotesBundle.LoadAsset<GameObject>("EmotesAnim").GetComponent<tk2dSpriteAnimation>().clips)
            {
                list2.Add(item);
                if (item.name.Length > 0 && ModHooks.GetMod("CustomKnight") is Mod)
                {
                    AddToCustomKnight(item.name);
                }
            }
            this._anim.Library.clips = list2.ToArray();
        }

        // Token: 0x06000003 RID: 3 RVA: 0x0000216C File Offset: 0x0000036C
        private void Update()
        {
            bool flag = Input.GetKeyDown(KeyCode.G) && HeroController.instance.CheckTouchingGround() && HeroController.instance.acceptingInput;
            if (flag)
            {
                base.StartCoroutine(this.Emote());
            }
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021AF File Offset: 0x000003AF
        private IEnumerator Emote()
        {
            HeroController.instance.RelinquishControl();
            HeroController.instance.StopAnimationControl();
            this._anim.Play("Dab");
            yield return new WaitWhile(() => this._anim.IsPlaying("Dab"));
            HeroController.instance.RegainControl();
            HeroController.instance.StartAnimationControl();
            yield break;
        }

        // Token: 0x04000001 RID: 1
        private tk2dSpriteAnimator _anim;
    }
}
