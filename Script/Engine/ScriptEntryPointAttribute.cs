using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// スクリプトのエントリーポイントに付ける属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ScriptEntryPointAttribute : Attribute
    {
    }
}
