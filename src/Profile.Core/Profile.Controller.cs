﻿using ExtensibleSaveFormat;
using KKAPI;
using KKAPI.Chara;
using KKAPI.Maker;

namespace KK_Plugins
{
    public class ProfileController : CharaCustomFunctionController
    {
        private string ProfileText;

        protected override void OnCardBeingSaved(GameMode currentGameMode)
        {
            if (MakerAPI.InsideAndLoaded)
                ProfileText = Profile.ProfileTextbox.Value;

            if (ProfileText == "")
            {
                SetExtendedData(null);
            }
            else
            {
                var data = new PluginData();
                data.data.Add(nameof(ProfileText), ProfileText);
                SetExtendedData(data);
            }
        }

        protected override void OnReload(GameMode currentGameMode, bool maintainState)
        {
            ProfileText = null;

            var data = GetExtendedData();
            if (data != null && data.data.TryGetValue(nameof(ProfileText), out var loadedProfileText))
                ProfileText = loadedProfileText?.ToString();

            if (MakerAPI.InsideMaker && Profile.ProfileTextbox != null)
                Profile.ProfileTextbox.Value = ProfileText ?? "";
        }
    }
}
