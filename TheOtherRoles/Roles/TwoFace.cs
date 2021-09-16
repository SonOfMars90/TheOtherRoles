using System.Net;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using TheOtherRoles.Patches;

namespace TheOtherRoles.Roles {
    public static class TwoFace {
        public static PlayerControl twoFace;

        public static Color color = Palette.ImpostorRed;
        private static Sprite morphSprite;

        public static float cooldown = 0f;
        public static float duration = 10f;

        public static PlayerControl morphTarget;
        public static float morphTimer = 0f;

        public static byte pos = 1;
        public static byte active = 1;

        public static float xPosition = 0;

        public static void resetMorph() {
            morphTarget = null;
            morphTimer = 0f;
            if(twoFace == null) return;
            twoFace.SetName(twoFace.Data.PlayerName);
            twoFace.SetHat(twoFace.Data.HatId, (int)twoFace.Data.ColorId);
            Helpers.setSkinWithAnim(twoFace.MyPhysics, twoFace.Data.SkinId);
            twoFace.SetPet(twoFace.Data.PetId);
            twoFace.CurrentPet.Visible = twoFace.Visible;
            twoFace.SetColor(twoFace.Data.ColorId);
        }

        public static void noMorph() {
            twoFace.SetName(twoFace.Data.PlayerName);
            twoFace.SetHat(twoFace.Data.HatId, (int)twoFace.Data.ColorId);
            Helpers.setSkinWithAnim(twoFace.MyPhysics, twoFace.Data.SkinId);
            twoFace.SetPet(twoFace.Data.PetId);
            twoFace.CurrentPet.Visible = twoFace.Visible;
            twoFace.SetColor(twoFace.Data.ColorId);
        }

        public static void clearAndReload() {
            resetMorph();
            twoFace = null;
            morphTarget = null;
            morphTimer = 0f;
            cooldown = CustomOptionHolder.twoFaceCooldown.getFloat();
            duration = CustomOptionHolder.twoFaceDuration.getFloat();
        }

        public static Sprite getMorphSprite() {
            if(morphSprite) return morphSprite;
            morphSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.MorphButton.png", 115f);
            return morphSprite;
        }

        // Call in RPC.cs
        public static void twoFaceMorph(byte playerId) {
            if(TwoFace.twoFace == null) return;
            TwoFace.morphTimer = TwoFace.duration;
            TwoFace.morphTarget = Helpers.playerById(playerId);
        }

        public static void setMorphTarget(byte playerId) {
            if(playerId == 0) TwoFace.noMorph();
            else TwoFace.morphTarget = Helpers.playerById(playerId);
            //TwoFace.active = active;
        }

        public static void setTwoFacePos(byte pos) {
            TwoFace.pos = pos;
        }

    }
}
