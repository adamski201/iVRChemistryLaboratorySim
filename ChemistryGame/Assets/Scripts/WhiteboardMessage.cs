using System;
using System.Reflection;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{

    class WhiteboardMessageAttr : Attribute
    {
        internal WhiteboardMessageAttr(bool isError, string key)
        {
            this.isError = isError;
            this.key = key;
        }
        public bool isError { get; private set; }
        public string key { get; private set; }
    };

    public static class WhiteboardMessages
    {
        public static bool IsError(this WhiteboardMessage m)
        {
            WhiteboardMessageAttr attr = GetAttr(m);
            return attr.isError;
        } 

        public static string Key(this WhiteboardMessage m)
        {
            WhiteboardMessageAttr attr = GetAttr(m);
            return attr.key;
        }

        private static WhiteboardMessageAttr GetAttr( WhiteboardMessage m)
        {
            return (WhiteboardMessageAttr)Attribute.GetCustomAttribute(ForValue(m), typeof(WhiteboardMessageAttr));  
        }

        private static MemberInfo ForValue(WhiteboardMessage m)
        {
            return typeof(WhiteboardMessage).GetField(Enum.GetName(typeof(WhiteboardMessage), m));
        }
    }
    public enum WhiteboardMessage
    {
        // fields: isError, i18n text label
        [WhiteboardMessageAttr(true, "Wire Error")] WIRE_ERROR,
        [WhiteboardMessageAttr(true, "Water Error")] WATER_ERROR,
        [WhiteboardMessageAttr(false, "Success Message")] SUCCESS,
        [WhiteboardMessageAttr(false, "Welcome Message")] WELCOME,
        [WhiteboardMessageAttr(true, "Granules Error")] GRANULE_ERROR,
        [WhiteboardMessageAttr(true, "Clamp Error")] CLAMP_ERROR,
        [WhiteboardMessageAttr(true, "Too Hot Error")] HEAT_ERROR,
        [WhiteboardMessageAttr(true, "Stopper Error")] STOPPER_ERROR,
        [WhiteboardMessageAttr(true, "Invalid Error")] INVALID_ERROR
    }
}