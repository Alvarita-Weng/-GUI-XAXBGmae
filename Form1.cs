using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace 視窗版本的XAXB_遊戲
{
    public partial class Form1 : Form
    {
        private XaXbEngine xaxb; //xaxb遊戲引擎
        private int counter;     //猜測次數計時器

        public Form1()  //Form1的建構函式
        {
            InitializeComponent();
            xaxb = new XaXbEngine(); //初始化xaxb遊戲引擎
            counter = 0;             //初始化猜測次數
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string GuessNum = textBox1.Text.Trim(); // 使用者輸入的數字
            if (xaxb.IsLegal(GuessNum)) // 檢查輸入的數字是否合法
            {
                counter++; // 猜測次數加一
                string result = xaxb.GetResult(GuessNum); // 獲取猜測結果
                Results.Items.Add($"{GuessNum}: {result}， 猜測次數: {counter}"); // 將猜測結果顯示在列表框中
                if (result == "3A0B") // 如果猜對了
                {
                    Results.Items.Add("恭喜你，猜對了!"); // 顯示恭喜訊息
                    btnGuess.Enabled = false; // 禁用猜測按鈕
                }
            }
            else
            {
                MessageBox.Show("輸入的資料不對，或長度不對!!請重新輸入!!"); // 提示用戶重新輸入
            }
            textBox1.Clear(); // 清空輸入框
        }
    }

    public class XaXbEngine   //XaXb引擎類別
    {
        string luckynum;      //幸運數字
        public XaXbEngine()   //隨機生成幸運數字(建構函式)
        {
            Random random = new Random();    
            int[] tem = new int[3];
            tem[0] = random.Next(0, 9);
            tem[1] = random.Next(0, 9);
            tem[2] = random.Next(0, 9);
            while (tem[0] == tem[1] ^ tem[1] == tem[2] ^ tem[0] == tem[2])
            {
                tem[1] = random.Next(0, 9);
                tem[2] = random.Next(0, 9);
            }
            luckynum = $"{tem[0]}{tem[1]}{tem[2]}";
        }
       
        public Boolean IsLegal(String theNumber)   //檢查數字是否合法
        {
            char[] tem = theNumber.ToCharArray();
            if (tem.Length == 3)
            {
                if (tem[0] != tem[1] ^ tem[1] != tem[2] ^ tem[0] != tem[2])
                {
                    return true;   //數字不重複--> 合法
                }
                else
                {
                    return false;  //數字重複--> 不合法
                }
            }
            else
            {
                return false;      //數字長度!=3 --> 不合法
            }
        }
        
        //幸運數字與猜測數字的比對結果
        public string GetResult(String GuessNum)   //GuessNum和luckynum每個數字配對
        {
            char[] user = GuessNum.ToCharArray();
            char[] ans = this.luckynum.ToCharArray();
            int a = 0;
            int b = 0;
            for (int i = 0; i < user.Length; i++)
            {
                for (int j = 0; j < ans.Length; j++)
                {
                    if (user[i] == ans[j])
                    {
                        if (i == j)
                        {
                            a++;    //猜中數字且位置相同 --> A加一
                        }
                        else
                        {
                            b++;    //猜中數字但位置不同 --> B加一
                        }
                    }
                }
            }
            return $"{a}A{b}B";
        }
        public Boolean IsGameover(String GuessNum) //判斷遊戲是否要結束
        {
            return GetResult(GuessNum) == "3A0B"; // 判斷是否猜中全部數字和位置
        }
    }

}
