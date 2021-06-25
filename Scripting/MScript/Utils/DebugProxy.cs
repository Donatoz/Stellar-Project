using System;

namespace Metozis.Scripting.MScript.Utils
{
    public class DebugProxy
    {
        public Action<string> OnMessageReceived = UnityEngine.Debug.Log;

        internal void Debug(string msg)
        {
            OnMessageReceived?.Invoke(msg);
        }
    }
}