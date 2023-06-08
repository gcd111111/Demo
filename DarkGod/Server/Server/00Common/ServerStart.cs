/****************************************************
	文件：ServerStart.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/02 12:43   
	功能：服务器入口
*****************************************************/

using System.Threading;

class ServerStart {
    static void Main(string[] args) {
        ServerRoot.Instance.Init();

        while (true) {
            ServerRoot.Instance.Update();
            Thread.Sleep(20);
        }
    }
}
