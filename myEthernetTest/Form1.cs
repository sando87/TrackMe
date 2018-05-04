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
        private const string MSG_SUCCESS_NEWID      = "회원가입 성공";
        private const string MSG_ALREADY_ID         = "이미 등록된 ID입니다";
        private const string MSG_SUCCESS_LOGIN      = "로그인 성공";
        private const string MSG_UNREGIST_ID        = "등록되지 않은 ID입니다";
        private const string MSG_WRONG_PW           = "잘못된 비밀번호입니다";
        private const string MSG_DIFF_PW            = "비밀번호가 서로 다릅니다"; 

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
                    OnRecv_NewUser(obj);
                    break;
                case ICD.COMMAND.Login:
                    OnRecv_Login(obj);
                    break;
                default:
                    break;
            }
        }

        private void OnRecv_NewUser(ICD.HEADER obj)
        {
            ICD.ERRORCODE curErr = (ICD.ERRORCODE)obj.error;
            switch(curErr)
            {
                case ICD.ERRORCODE.NOERROR:
                    MessageBox.Show(MSG_SUCCESS_NEWID);
                    break;
                case ICD.ERRORCODE.HaveID:
                    MessageBox.Show(MSG_ALREADY_ID);
                    break;
                default:
                    break;
            }
        }
        private void OnRecv_Login(ICD.HEADER obj)
        {
            ICD.ERRORCODE curErr = (ICD.ERRORCODE)obj.error;
            switch (curErr)
            {
                case ICD.ERRORCODE.NOERROR:
                    MessageBox.Show(MSG_SUCCESS_LOGIN);
                    {
                        this.Visible = false;
                        Home home = new Home();
                        home.ShowDialog();
                    }
                    break;
                case ICD.ERRORCODE.NoID:
                    MessageBox.Show(MSG_UNREGIST_ID);
                    break;
                case ICD.ERRORCODE.WorngPW:
                    MessageBox.Show(MSG_WRONG_PW);
                    break;
                default:
                    break;
            }
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
                if(textBox2.Text == textBox3.Text)
                {
                    ICD.User obj = new ICD.User();
                    ICD.HEADER.HeadBuilder(obj, ICD.COMMAND.NewUser, ICD.TYPE.REQ);
                    obj.userID = textBox1.Text;
                    obj.userPW = textBox2.Text;
                    sendMsgToServer(obj);
                }
                else
                {
                    MessageBox.Show(MSG_DIFF_PW);
                }
                
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
