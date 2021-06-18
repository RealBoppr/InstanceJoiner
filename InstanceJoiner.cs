using System.Collections;
using System.Windows.Forms;
using MelonLoader;
using UnityEngine;
using RubyButtonAPI;

[assembly: MelonInfo(typeof(InstanceJoiner.Main), "InstanceJoiner", "1.2", "Boppr")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace InstanceJoiner
{
    public class Main : MelonMod
    {
        public static MelonPreferences_Entry<int> ButtonsX;
        public static MelonPreferences_Entry<int> ButtonsY;
        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(CheckUIManager());
            MelonPreferences_Category Category = MelonPreferences.CreateCategory("InstanceJoiner", "InstanceJoiner");
            ButtonsX = Category.CreateEntry("ButtonsX", 0, "Button Position X");
            ButtonsY = Category.CreateEntry("ButtonsY", 1, "Button Position Y");
        }
        private IEnumerator CheckUIManager()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) { yield return null; }
            OnUIInit();
        }
        private void OnUIInit()
        {
            QMSingleButton CopyIDButton = new QMSingleButton(
               "ShortcutMenu",
               ButtonsX.Value, ButtonsY.Value,
               "Copy\nInstance ID",
               delegate ()
               {
                   Clipboard.SetText($"{RoomManager.field_Internal_Static_ApiWorld_0.id}:{RoomManager.field_Internal_Static_ApiWorldInstance_0.instanceId}");
               },
               "Copy the ID of the current instance."
               );
            QMSingleButton JoinInstanceButton = new QMSingleButton(
               "ShortcutMenu",
               ButtonsX.Value, ButtonsY.Value,
               "Join\nInstance",
               delegate ()
               {
                   new PortalInternal().Method_Private_Void_String_String_PDM_0(Clipboard.GetText().Split(':')[0], Clipboard.GetText().Split(':')[1]);
               },
               "Join an instance via your clipboard."
               );
            Misc.ChangeButtonSize(CopyIDButton.getGameObject(), 420, 210);
            Misc.ChangeButtonSize(JoinInstanceButton.getGameObject(), 420, 210);
            Misc.MoveButton(CopyIDButton.getGameObject(), CopyIDButton.getGameObject().GetComponent<RectTransform>().localPosition.x, CopyIDButton.getGameObject().GetComponent<RectTransform>().localPosition.y + 105);
            Misc.MoveButton(JoinInstanceButton.getGameObject(), JoinInstanceButton.getGameObject().GetComponent<RectTransform>().localPosition.x, JoinInstanceButton.getGameObject().GetComponent<RectTransform>().localPosition.y - 105);
        }
    }
}
