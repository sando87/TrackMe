using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackMe
{
    public partial class Form1 : Form
    {
        private const string IPADDR = "127.0.0.1";
        private const int PORTNUM = 7000;
        private ICDPacketMgr IcdMgr = null;
        private int clientID = -1;

        public Form1()
        {
            InitializeComponent();

        }

        private void InitServer()
        {
            IcdMgr = new ICDPacketMgr();

            IcdMgr.OnRecv += ICDMessage_Recv;

            //IcdMgr.StartServiceServer();

            clientID = IcdMgr.StartServiceClient(IPADDR, PORTNUM);
        }
        
        private void ICDMessage_Recv(int clientID, ICD.HEADER o)
        {
            ICD.HEADER obj = o as ICD.HEADER;
            switch((ICD.COMMAND)obj.id)
            {
                case ICD.COMMAND.NewUser:
                    break;
                case ICD.COMMAND.Login:
                    break;
                case ICD.COMMAND.Logout:
                    break;
                default:
                    break;
            }
        }

        private ICD.User NewIcd_User()
        {
            ICD.User obj = new ICD.User();
            obj.id = (uint)ICD.COMMAND.NewUser;
            obj.size = (uint)ICD.User.HeaderSize();
            obj.sof = (uint)ICD.MAGIC.SOF;
            obj.type = 123;
            byte[] buf = ICD.HEADER.Serialize(obj);
            NetworkMgr.GetInst().WriteToClient(clientID, buf);

            return obj;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                button1.Text = "회원가입";
                textBox3.Enabled = true;
            }
            else
            {
                button1.Text = "로그인";
                textBox3.Enabled = false;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //회원가입 요청
                ICD.User obj = new ICD.User();
                ICD.HEADER.HeadBuilder(obj, ICD.COMMAND.NewUser, ICD.TYPE.REQ);
                obj.userID = textBox1.Text;
                obj.userPW = textBox2.Text;
                sendMsgToServer(obj);
            }
            else
            {
                //로그인 요청
                ICD.User obj = new ICD.User();
                ICD.HEADER.HeadBuilder(obj, ICD.COMMAND.Login, ICD.TYPE.REQ);
                obj.userID = textBox1.Text;
                obj.userPW = textBox2.Text;
                sendMsgToServer(obj);
            }
        }

        private bool sendMsgToServer(object obj)
        {
            if (clientID == -1)
                return false;

            byte[] buf = ICD.HEADER.Serialize(obj);
            NetworkMgr.GetInst().WriteToClient(clientID, buf);
            return true;
        }
    }
}
