using System;
using System.Collections;
using System.Reflection;
using System.IO;

namespace Kodama.DependencyInjection.Loader
{
    /// <summary>
    /// 指定されたパスのアセンブリから型をロードします
    /// </summary>
    public class TypeLoader : IDisposable
    {
        /// <summary>
        /// 子ローダ
        /// </summary>
        private ArrayList children = new ArrayList();

        /// <summary>
        /// 型をロードするアセンブリ
        /// </summary>
        private ArrayList assemblies = new ArrayList();

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public TypeLoader()
        {
            // 何もしない
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent">親ローダ</param>
        public TypeLoader(TypeLoader parent)
        {
            parent.AddChild(this);
        }

        /// <summary>
        /// 子ローダを追加する
        /// </summary>
        /// <param name="child">追加する子ローダ</param>
        public void AddChild(TypeLoader child)
        {
            children.Add(child);
        }

        /// <summary>
        /// 型を読み出すアセンブリを追加する
        /// </summary>
        /// <param name="filename">型を読み出すアセンブリのファイル名</param>
        public void AddAssemblyFile(string filename)
        {
            assemblies.Add(Assembly.LoadFile(filename));
        }

        /// <summary>
        /// 型を読み出すアセンブリを追加する
        /// </summary>
        /// <param name="folder">型を読み出すアセンブリが含まれるフォルダ</param>
        public void AddAssemblyFolder(string folder)
        {
            foreach (string name in Directory.GetFiles(folder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(Path.Combine(folder, name)));
            }
        }

        /// <summary>
        /// 指定された名前の型をアセンブリからロードします
        /// </summary>
        /// <param name="fullTypeName">ロードする型の名前</param>
        /// <returns>ロードされた型</returns>
        /// <exception cref="TypeNotFoundException">
        /// 指定された名前の型がみつからなかった場合に発生する例外
        /// </exception>
        public Type LoadType(string fullTypeName)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type type = assembly.GetType(fullTypeName);
                if (type != null)
                {
                    return type;
                }
            }
            foreach (TypeLoader child in children)
            {
                try
                {
                    return child.LoadType(fullTypeName);
                }
                catch (TypeNotFoundException)
                {
                }
            }
            throw new TypeNotFoundException();
        }

        /// <summary>
        /// ロードしたリソースを破棄する
        /// </summary>
        public void Dispose()
        {
            assemblies.Clear();

            foreach (TypeLoader child in children)
            {
                child.Dispose();
            }
        }
    }
}
