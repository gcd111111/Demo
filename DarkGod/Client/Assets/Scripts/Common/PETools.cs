/****************************************************
	文件：PETools.cs
	作者：gcd
	邮箱: 2042484168@qq.com
	日期：2023/05/13 14:01   	  	
	功能：工具类
*****************************************************/


public class PETools {
    public static int RDInt(int min, int max, System.Random rd = null) {
        if (rd == null) {
            rd = new System.Random();
        }
        int val = rd.Next(min, max + 1);
        return val;
    }
}
