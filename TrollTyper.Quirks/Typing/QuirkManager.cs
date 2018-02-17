using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TrollTyper.Quirks.Logging;
using TrollTyper.Quirks.Scripting;
using TrollTyper.Quirks.Typing;

namespace TrollTyper.Quirks.Typing
{
    public static class QuirkManager
    {
        public static ObservableCollection<TypingQuirk> TypingQuirks { get; set; }

        static QuirkManager()
        {
            TypingQuirks = new ObservableCollection<TypingQuirk>();
        }

        public static TypingQuirk LoadQuirk(string luaData)
        {
            try
            {
                Script script = new Script();
                UserData.RegisterType<Utilities>();
                UserData.RegisterType<ValueReplacement>();
                UserData.RegisterType<Color>();
                script.Globals["TT"] = typeof(Utilities);
                script.Globals["Color"] = typeof(Color);
                script.DoString(luaData);
                return new TypingQuirk(script);

            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
                return null;
            }
        }
    }
}
