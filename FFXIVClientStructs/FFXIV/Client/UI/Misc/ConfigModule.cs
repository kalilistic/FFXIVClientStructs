﻿using System.Runtime.InteropServices;
using FFXIVClientStructs.Attributes;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Component.GUI;

namespace FFXIVClientStructs.FFXIV.Client.UI.Misc {
    // Client::UI::Misc::ConfigModule
    // ctor E8 ?? ?? ?? ?? 48 8B 97 ?? ?? ?? ?? 48 8D 8F ?? ?? ?? ?? 4C 8B CF

    [StructLayout(LayoutKind.Explicit, Size = 0xD8A8)]
    public unsafe partial struct ConfigModule {
        public const int ConfigOptionCount = 681;
        [FieldOffset(0x28)] public UIModule* UIModule;
        [FieldOffset(0x2C8)] private fixed byte options[Option.Size * ConfigOptionCount];
        [FieldOffset(0x57F8)] private fixed byte values[0x10 * ConfigOptionCount];

        public static ConfigModule* Instance() => Framework.Instance()->GetUiModule()->GetConfigModule();

        [MemberFunction("E8 ?? ?? ?? ?? C6 47 4D 00")]
        public partial bool SetOption(uint index, int value, int a4 = 2, bool a5 = true, bool a6 = false);

        public void SetOption(ConfigOption option, int value) {
            for (uint i = 0; i < ConfigOptionCount; i++) {
                var o = GetOption(i);
                if (o->OptionID != option) continue;
                SetOption(i, value);
                return;
            }
        }

        public void SetOptionById(short optionId, int value) => SetOption((ConfigOption)optionId, value);

        public Option* GetOption(uint index) {
            fixed (byte* p = options) {
                var o = (Option*)p;
                return o + index;
            }
        }

        public Option* GetOption(ConfigOption option) {
            for (uint i = 0; i < ConfigOptionCount; i++) {
                var o = GetOption(i);
                if (o->OptionID == option) return o;
            }

            return null;
        }

        public Option* GetOptionById(short optionId) => GetOption((ConfigOption)optionId);

        public AtkValue* GetValue(uint index) {
            fixed (byte* p = values) {
                var v = (AtkValue*)p;
                return v + index;
            }
        }

        public AtkValue* GetValue(ConfigOption option) {
            for (uint i = 0; i < ConfigOptionCount; i++) {
                var o = GetOption(i);
                if (o->OptionID == option) return GetValue(i);
            }

            return null;
        }

        public AtkValue* GetValueById(short optionId) => GetValue((ConfigOption)optionId);

        [StructLayout(LayoutKind.Explicit, Size = Size)]
        public struct Option {
            public const int Size = 0x20;
            [FieldOffset(0x00)] public void* Unk00;
            [FieldOffset(0x08)] public ulong Unk08;
            [FieldOffset(0x10)] public ConfigOption OptionID;
            [FieldOffset(0x14)] public uint Unk14;
            [FieldOffset(0x18)] public uint Unk18;
            [FieldOffset(0x1C)] public ushort Unk1C;
        }
    }

    public enum ConfigOption : short {
        Invalid = -1,
        None = 0,
        CustomResolutionWidth = 17,
        CustomResolutionHeight = 18,
        ScreenMode = 19,
        GamepadMode = 89,
        LegacyMovement = 304,
        DisplayItemHelp = 716,
        DisplayActionHelp = 721,
        OwnDisplayNameSettings = 443,
        PartyDisplayNameSettings = 456,
        AllianceDisplayNameSettings = 465,
        OtherPCsDisplayNameSettings = 472,
        FriendsDisplayNameSettings = 517,
    }


}
