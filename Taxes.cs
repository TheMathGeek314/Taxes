using Modding;
using System.Collections.Generic;
using UnityEngine;
using Satchel;
using HutongGames.PlayMaker;

namespace Taxes {
    public class Taxes: Mod {
        new public string GetName() => "Taxes";
        public override string GetVersion() => "1.0.0.0";

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            On.PlayMakerFSM.OnEnable += editFSM;
        }

        private void editFSM(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self) {
            orig(self);
            if(self.gameObject.name.Contains("Fountain Donation") && self.FsmName == "Conversation Control") {
                self.GetValidState("Full Donation").InsertAction(new takeAllGeo(self), 0);
            }
        }
    }

    public class takeAllGeo: FsmStateAction {
        private PlayMakerFSM self;
        public takeAllGeo(PlayMakerFSM self) {
            this.self = self;
        }
        public override void OnEnter() {
            self.FsmVariables.GetFsmInt("Amount to Donate").Value = PlayerData.instance.geo;
        }
    }
}