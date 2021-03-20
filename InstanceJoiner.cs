using System;
using System.IO;
using System.Windows.Forms;
using MelonLoader;
using UnityEngine;
using RubyButtonAPI;

namespace InstanceJoiner
{
    public class Class : MelonMod
    {
        public override void VRChat_OnUiManagerInit()
        {
            QMSingleButton CopyIDButton = new QMSingleButton(
               "ShortcutMenu",
               5, -1,
               "Copy\nInstance ID",
               delegate ()
               {
                   Clipboard.SetText($"{RoomManager.field_Internal_Static_ApiWorld_0.id}:{RoomManager.field_Internal_Static_ApiWorldInstance_0.idWithTags}");
               },
               "Copy the ID of the current instance."
           );
            QMSingleButton JoinInstanceButton = new QMSingleButton(
               "ShortcutMenu",
               5, -1,
               "Join\nInstance",
               delegate ()
               {
                   new PortalInternal().Method_Private_Void_String_String_PDM_0(Clipboard.GetText().Substring(0, Clipboard.GetText().IndexOf(":")), Clipboard.GetText().Substring(Clipboard.GetText().IndexOf(":") + 1));
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
