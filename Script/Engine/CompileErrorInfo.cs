using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// コンパイルエラー情報です
    /// </summary>
    public class CompileErrorInfo
    {
        /// <summary>
        /// エラーの発生したソース名
        /// </summary>
        private string sourceName;

        /// <summary>
        /// エラーの説明文
        /// </summary>
        private string description;

        /// <summary>
        /// エラーの発生した行
        /// </summary>
        private int errorLine;

        /// <summary>
        /// エラーの発生した行のテキスト
        /// </summary>
        private string errorText; 

        /// <summary>
        /// エラーの発生したソース名
        /// </summary>
        public string SourceName
        {
            get { return sourceName; }
        }

        /// <summary>
        /// エラーの説明文
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// エラーの発生した行
        /// </summary>
        public int ErrorLine
        {
            get { return errorLine; }
        }

        /// <summary>
        /// エラーの発生した行のテキスト
        /// </summary>
        public string ErrorText
        {
            get { return errorText; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">ソース名</param>
        /// <param name="desc">エラーの説明文</param>
        /// <param name="text">エラーの発生した行のテキスト</param>
        /// <param name="line">エラーの発生した行</param>
        public CompileErrorInfo(
            string name,
            string desc,
            int    line,
            string text )
        {
            sourceName  = name;
            description = desc;
            errorLine   = line;
            errorText   = text; 
        }
    }
}
