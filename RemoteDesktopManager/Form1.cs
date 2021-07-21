using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MSTSCLib;

namespace RemoteDesktopManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DESEncrypt.Key = "hghg";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load("servicesInfo.xml");
            //获取节点列表 
            XmlNodeList topM = xmldoc.SelectNodes("//Service");
            int index = 0;
            AxMSTSCLib.AxMsTscAxNotSafeForScripting axIndex = null;
            foreach (XmlElement element in topM)
            {
                string ip = element.GetElementsByTagName("IP")[0].InnerText;
                string domain = element.GetElementsByTagName("Domain")[0].InnerText;
                string username = element.GetElementsByTagName("UserName")[0].InnerText;
                string password = element.GetElementsByTagName("PassWord")[0].InnerText;
                string isMD5 = element.GetElementsByTagName("PassWordIsUseMD5")[0].InnerText;
                if (isMD5 == "1")
                {
                    password = DESEncrypt.Decrypt(password);
                }
                TabPage tp = new TabPage();
                tp.Tag = password;
                tp.Text = ip;
                tp.Name = "tp" + index;
                AxMSTSCLib.AxMsTscAxNotSafeForScripting ax = new AxMSTSCLib.AxMsTscAxNotSafeForScripting();
                ((System.ComponentModel.ISupportInitialize)(ax)).BeginInit();

                tp.Controls.Add(ax);

                ((System.ComponentModel.ISupportInitialize)(ax)).EndInit();
                ax.Name = tp.Name + "ax";
                ax.Server = ip;
                ax.UserName = username;
                ax.Domain = domain;
                ax.Dock = DockStyle.Fill;
                if (index == 0)
                {
                    axIndex = ax;
                }

                IMsTscNonScriptable secured = (IMsTscNonScriptable)ax.GetOcx();
                secured.ClearTextPassword = password;

                tabControl1.TabPages.Add(tp);





                index++;
            }
            if (axIndex != null)
            {
                conn(axIndex);
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            AxMSTSCLib.AxMsTscAxNotSafeForScripting ax = (AxMSTSCLib.AxMsTscAxNotSafeForScripting)(e.TabPage.Controls.Find(e.TabPage.Name + "ax", false)[0]);

            conn(ax);
        }
        private void conn(AxMSTSCLib.AxMsTscAxNotSafeForScripting ax)
        {
            if (ax.Connected == 0)
            {
                ax.Connect();
            }
        }

        private void a_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            AxMSTSCLib.AxMsTscAxNotSafeForScripting ax = (AxMSTSCLib.AxMsTscAxNotSafeForScripting)(tabControl1.SelectedTab.Controls.Find(tabControl1.SelectedTab.Name + "ax", false)[0]);
            if (e.ClickedItem.Name == "start")
            {

                conn(ax);
            }
            else
            {
                ax.Disconnect();
            }
        }

        private void a_Opening(object sender, CancelEventArgs e)
        {
            AxMSTSCLib.AxMsTscAxNotSafeForScripting ax = (AxMSTSCLib.AxMsTscAxNotSafeForScripting)(tabControl1.SelectedTab.Controls.Find(tabControl1.SelectedTab.Name + "ax", false)[0]);

            if (ax.Connected == 1)
            {
                a.Items[0].Enabled = false;
                a.Items[1].Enabled = true;
            }
            else
            {
                a.Items[0].Enabled = true;
                a.Items[1].Enabled = false;
            }

        }
    }


    public static class DESEncrypt
    {
        public static string Key { get; set; }
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, Key);
        }
        public static string Encrypt(string Text, string sKey)
        {
            //byte[] keyBytes = Encoding.UTF8.GetBytes(sKey.Substring(0, 8));
            //byte[] keyIV = keyBytes;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        public static string Decrypt(string Text)
        {
            return Decrypt(Text, Key);
        }
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
