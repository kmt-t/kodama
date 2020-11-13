//------------------------------------------------------------------------------
// <自動生成>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:2.0.40607.16
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、損失したりします:
//     コードは再生成されました。
// </自動生成>
//------------------------------------------------------------------------------

namespace Kodama.Extension.Function.Properties {
    
    
    sealed partial class Settings : System.Configuration.ApplicationSettingsBase {
        
        private static Settings m_Value;
        
        private static object m_SyncObject = new object();
        
        [System.Diagnostics.DebuggerNonUserCode()]
        public static Settings Value {
            get {
                if ((Settings.m_Value == null)) {
                    System.Threading.Monitor.Enter(Settings.m_SyncObject);
                    if ((Settings.m_Value == null)) {
                        try {
                            Settings.m_Value = new Settings();
                        }
                        finally {
                            System.Threading.Monitor.Exit(Settings.m_SyncObject);
                        }
                    }
                }
                return Settings.m_Value;
            }
        }
    }
}
