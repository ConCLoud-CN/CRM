using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Xpo;
using System.Net.Mail;
using System.Windows.Forms;
using System.Windows;

namespace CRM.Module.BusinessObjects.Sys_Management
{
    public class CommUtilities
    {
        public static string AddOrdinal(int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }
        }

        public static string OrdinalMonth(int m)
        {
            switch (m)
            {
                case 1:
                    return "January";
                case 2:
                    return "Feburary";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return "-";
            }
        }

        public static string AddOrdinalDate(DateTime dt)
        {
            if (dt == DateTime.MinValue) return "-";
            int y = dt.Year;
            int m = dt.Month;
            int d = dt.Day;
            string ttd = AddOrdinal(d) + " " + OrdinalMonth(m) + " " + y.ToString();
            return ttd;
        }

        public static string AddOrdinalDateTime(DateTime dt)
        {
            if (dt == DateTime.MinValue) return "-";
            int yr = dt.Year;
            int mt = dt.Month;
            int da = dt.Day;
            int hr = dt.Hour;
            int mn = dt.Minute;
            string ttd = hr.ToString("D2") + ":" + mn.ToString("D2") + " " + AddOrdinal(da) + " " + OrdinalMonth(mt) + "" + yr.ToString();
            return "-";
        }

        public static DateTime GetCurrentServerTime(Session _session)
        {
            CriteriaOperator funcNow = new FunctionOperator(FunctionOperatorType.Now);
            var serverTime = (DateTime)_session.Evaluate(typeof(XPObjectType), funcNow, null);
            return serverTime;
        }

        public static SysUser GetCurrentUser(Session _session)
        {
            return (SysUser)_session.GetObjectByKey<SysUser>(SecuritySystem.CurrentUserId);
        }


        public static bool 校验身份证号码(string Id)
        {

            if (Id.Length == 18)
            {

                bool check = CheckIDCard18(Id);

                return check;

            }

            else if (Id.Length == 15)
            {

                bool check = CheckIDCard15(Id);

                return check;

            }

            else
            {

                return false;

            }

        }

        public static bool CheckIDCard18(string Id)
        {

            long n = 0;

            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {

                return false;//数字验证

            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {

                return false;//省份验证

            }

            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");

            DateTime time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {

                return false;//生日验证

            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');

            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

            char[] Ai = Id.Remove(17).ToCharArray();

            int sum = 0;

            for (int i = 0; i < 17; i++)
            {

                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

            }

            int y = -1;

            Math.DivRem(sum, 11, out y);

            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {

                return false;//校验码验证

            }

            return true;//符合GB11643-1999标准

        }

        public static bool CheckIDCard15(string Id)
        {

            long n = 0;

            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {

                return false;//数字验证

            }

            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

            if (address.IndexOf(Id.Remove(2)) == -1)
            {

                return false;//省份验证

            }

            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");

            DateTime time = new DateTime();

            if (DateTime.TryParse(birth, out time) == false)
            {

                return false;//生日验证

            }

            return true;//符合15位身份证标准

        }

        public static void CascadeCommit(IObjectSpace os, object current, bool reload)
        {
            if (os is INestedObjectSpace)
            {
                IObjectSpace parent = ((INestedObjectSpace)os).ParentObjectSpace;
                parent.CommitChanges();
                CascadeCommit(parent, parent.GetObject(current), reload);
            }
            if (reload && current != null)
            {
                os.ReloadObject(current);
            }
        }
        //public static void 添加为待办任务(BaseObject 业务, SysUser 人员, string 任务信息)
        //{
        //    var type = 业务.GetType();
        //    var f = 业务.Session.Query<待办任务>().Where(w => w.任务用户.Oid == 人员.Oid && w.任务类型 == type.FullName && w.任务编号 == 业务.Oid&&w.任务信息 == (type.Name + " - " + 任务信息));
        //    if (f.Count() == 0)
        //    {
        //        IObjectSpace os = XPObjectSpace.FindObjectSpaceByObject(业务);
        //        待办任务 dbrw = new 待办任务(业务.Session);
        //        dbrw.任务信息 = type.Name + " - " + 任务信息;
        //        dbrw.任务用户 = os.FindObject<SysUser>(new BinaryOperator("Oid", 人员.Oid));
        //        dbrw.任务类型 = type.FullName;
        //        dbrw.任务编号 = 业务.Oid;
        //    }
        //   os.CommitChanges();

        // }
        public static void SendMailUse(string strto, string subject, string body)
        {
            string host = "smtp.163.com";// 邮件服务器 
            string userName = "rycon0430@163.com";// 发送端账号   
            string password = "19890430//";// 发送端密码(这个客户端重置后的密码)
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
            client.Host = host;//邮件服务器
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(userName, password);//用户名、密码
            string strfrom = userName;
            //string strto = "yikai.yang@con-cloud.cn";
            //string strcc = "2605625733@qq.com";//抄送
            //string subject = "您有来自一条电子化的通知，请注意查收！";//邮件的主题             
            //string body = "XXDepartment XXX 发起了一个问题需要您协助！";//发送的邮件正文  
            MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(strfrom, "任务管理系统");
            msg.To.Add(strto);
            //msg.CC.Add(strcc);
            msg.Subject = subject;//邮件Title   
            msg.Body = body;//邮件内容   
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
            msg.IsBodyHtml = true;//是否是HTML邮件   
            msg.Priority = MailPriority.High;//邮件优先级   
            try
            {
                client.Send(msg);
                Console.WriteLine("发送成功");
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message, "发送邮件出错");
                System.Windows.MessageBox.Show("发送邮件出错");
            }
        }
    }

    /// <summary>
    /// 计算日期的间隔(静态类)
    /// </summary>
    public static class dateTimeDiff
    {

        #region 返回时间差
        public static string 时间差(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
                return dateDiff;
            }
            catch
            {

            }
            return dateDiff;
        }
        #endregion

        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期字符串</param>
        /// <param name="d2">要参与计算的另一个日期字符串</param>
        /// <returns>一个表示日期间隔的TimeSpan类型</returns>
        public static TimeSpan toResult(string d1, string d2)
        {
            try
            {
                var date1 = DateTime.Parse(d1);
                var date2 = DateTime.Parse(d2);
                return toResult(date1, date2);
            }
            catch
            {
                throw new Exception("字符串参数不正确!");
            }
        }
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期</param>
        /// <param name="d2">要参与计算的另一个日期</param>
        /// <returns>一个表示日期间隔的TimeSpan类型</returns>
        public static TimeSpan toResult(DateTime d1, DateTime d2)
        {
            TimeSpan ts;
            if (d1 > d2)
            {
                ts = d1 - d2;
            }
            else
            {
                ts = d2 - d1;
            }
            return ts;
        }
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="d1">要参与计算的其中一个日期字符串</param>
        /// <param name="d2">要参与计算的另一个日期字符串</param>
        /// <param name="drf">决定返回值形式的枚举</param>
        /// <returns>一个代表年月日的int数组，具体数组长度与枚举参数drf有关</returns>
        public static int[] toResult(string d1, string d2, diffResultFormat drf)
        {
            try
            {
                var date1 = DateTime.Parse(d1);
                var date2 = DateTime.Parse(d2);
                return toResult(date1, date2, drf);
            }
            catch
            {
                throw new Exception("字符串参数不正确!");
            }
        }
        /// <summary>
        /// 计算日期间隔
        /// </summary>
        /// <param name="last">要参与计算的其中一个日期</param>
        /// <param name="far">要参与计算的另一个日期</param>
        /// <param name="drf">决定返回值形式的枚举</param>
        /// <returns>一个代表年月日的int数组，具体数组长度与枚举参数drf有关</returns>
        public static int[] toResult(DateTime last, DateTime far, diffResultFormat drf)
        {
            DateTime max;
            DateTime min;
            int year;
            int month;
            int tempYear, tempMonth;
            max = far;
            min = last;
            tempYear = max.Year;
            tempMonth = max.Month;
            if (max.Month < min.Month)
            {
                tempYear--;
                tempMonth = tempMonth + 12;
            }
            year = tempYear - min.Year;
            month = tempMonth - min.Month;
            if (drf == diffResultFormat.dd)
            {
                var ts = max - min;
                return new int[] { ts.Days };
            }
            if (drf == diffResultFormat.mm)
            {
                return new int[] { month + year * 12 };
            }
            if (drf == diffResultFormat.yy)
            {
                return new int[] { year };
            }
            return new int[] { year, month };
        }
    }
    /// <summary>
    /// 关于返回值形式的枚举
    /// </summary>
    public enum diffResultFormat
    {
        /// <summary>
        /// 天数
        /// </summary>
        dd,
        /// <summary>
        /// 天数
        /// </summary>
        mm,
        /// <summary>
        /// 年数
        /// </summary>
        yy,
        /// <summary>
        /// 年数和天数
        /// </summary>
        yymm
    }



}
