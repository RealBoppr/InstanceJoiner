using System.Windows.Forms;
using UnityEngine;
using MelonLoader;
using VRC.Core;
using ComfyUtils;

[assembly: MelonInfo(typeof(InstanceJoiner.Main), "InstanceJoiner", "2", "Boppr")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace InstanceJoiner
{
    public class Main : MelonMod
    {
        private ConfigHelper<Config> Helper;
        private Config Config => Helper.Config;
        private ApiWorld World => RoomManager.field_Internal_Static_ApiWorld_0;
        private ApiWorldInstance Instance => RoomManager.field_Internal_Static_ApiWorldInstance_0;
        public override void OnApplicationStart()
        {
            Helper = new($"{MelonUtils.UserDataDirectory}\\InstanceJoinerConfig.json");
        }
        public override void OnGUI()
        {
            if (World != null && Instance != null)
            {
                if (GUI.Button(new(Config.CopyX, Config.CopyY, 150, 20), "Copy Instance"))
                {
                    Clipboard.SetText($"{World.id}:{Instance.id}");
                }
                if (GUI.Button(new(Config.JoinX, Config.JoinY, 150, 20), "Join Instance"))
                {
                    new PortalInternal().Method_Private_Void_String_String_PDM_0(Clipboard.GetText().Split(':')[0], Clipboard.GetText().Split(':')[1]);
                }
            }
        }
    }
}
