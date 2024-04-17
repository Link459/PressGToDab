using Modding;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PressGToDab
{
    // Token: 0x02000003 RID: 3
    public class PressGToDab : Mod
    {
        // Token: 0x06000007 RID: 7 RVA: 0x000021DC File Offset: 0x000003DC
        public override string GetVersion()
        {
            return "1.2.0";
        }

        // Token: 0x06000008 RID: 8 RVA: 0x000021F4 File Offset: 0x000003F4
        public PressGToDab()
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            foreach (string text in executingAssembly.GetManifestResourceNames())
            {
                using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
                {
                    bool flag = manifestResourceStream != null;
                    if (flag)
                    {
                        string key = Path.GetExtension(text).Substring(1);
                        this._bundles[key] = AssetBundle.LoadFromStream(manifestResourceStream);
                    }
                }
            }
            switch (SystemInfo.operatingSystemFamily)
            {
                case (OperatingSystemFamily)1:
                    PressGToDab.EmotesBundle = this._bundles["emotesmac"];
                    break;
                case (OperatingSystemFamily)2:
                    PressGToDab.EmotesBundle = this._bundles["emoteswin"];
                    break;
                case (OperatingSystemFamily)3:
                    PressGToDab.EmotesBundle = this._bundles["emoteslin"];
                    break;
            }
            On.HeroController.Awake += new On.HeroController.hook_Awake(this.OnHeroControllerAwake);
        }

        // Token: 0x06000009 RID: 9 RVA: 0x0000230C File Offset: 0x0000050C
        private void OnHeroControllerAwake(On.HeroController.orig_Awake orig, HeroController self)
        {
            orig.Invoke(self);
            self.gameObject.AddComponent<Emoter>();
        }


        // Token: 0x04000002 RID: 2
        private Dictionary<string, AssetBundle> _bundles = new Dictionary<string, AssetBundle>();

        // Token: 0x04000003 RID: 3
        public static AssetBundle EmotesBundle;
    }
}
