﻿using System;
using System.Net.Mail;
using System.Net;
using System.Collections.Generic;

using System.Xml.Serialization;
using Com.BaseLibrary.Configuration;
using Com.BaseLibrary.Common.Cryptography;
using Com.BaseLibrary.Logging;
using System.ServiceModel;
using Com.BaseLibrary.Contract;
using System.Web;
using System.Xml;



namespace Com.BaseLibrary.Utility
{

    /// <summary> 
    /// 说明：在.net2.0以上版本中发送电子邮件的方法示例 
    /// 用到的类主要位于System.Net.Mail和System.Net命名空间下 
    /// </summary> 
    public class MobileUtil
    {
        private const string TaobaoMobileLocationAPI1 = "http://tcc.taobao.com/cc/json/mobile_tel_segment.htm?tel={0}";
        private const string YouDaoMobileLocationAPI = "http://www.youdao.com/smartresult-xml/search.s?type=mobile&q={0}";

        public static MobileLocationData GetMobileLocation(string phoneNO)
        {
            if (phoneNO.Length == 11)
            {
                string phoneNO7 = phoneNO.Substring(0, 7);
                MobileLocationData locationData = GetMobileLocationFromTaoBaoAPI(phoneNO, phoneNO7);
                //MobileLocationData locationData = GetMobileLocationFromYouDaoAPI(phoneNO, phoneNO7);
                if (locationData != null)
                {
                    return locationData;
                }
            }
            return null;
        }
        private static MobileLocationData GetMobileLocationFromTaoBaoAPI(string phoneNO, string phoneNO7)
        {
            try
            {
                string url = string.Format(TaobaoMobileLocationAPI1, phoneNO);
                string content = HttpUtil.GetRequestContent(url);
                content = content.Replace("\r\n", "");
                int startIndex = content.IndexOf('{');
                content = content.Substring(startIndex - 1, content.IndexOf('}') - startIndex);
                string[] datas = content.Split(',');
                if (datas.Length == 4)
                {
                    MobileLocationData locationData = new MobileLocationData();
                    locationData.Province = datas[1].Split(':')[1].Replace("'", "").Trim();
                    locationData.ISP = datas[2].Split(':')[1].Replace("'", "").Trim();
                    return locationData;
                }

            }
            catch (Exception ex)
            {
                
            }

            return null;
        }
        private static MobileLocationData GetMobileLocationFromYouDaoAPI(string phoneNO, string phoneNO7)
        {
            try
            {
                string url = string.Format(YouDaoMobileLocationAPI, phoneNO7);
                string content = HttpUtil.GetRequestContent(url);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(content);
                string[] locations = doc.SelectSingleNode("/smartresult/product/location").InnerText.Split(' ');
                MobileLocationData locationData = new MobileLocationData();
                locationData.Province = locations[0];
                if (locations.Length == 2)
                {
                    locationData.City = locations[1];
                }
                return locationData;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }

    public class MobileLocationData
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string County { get; set; }

        public string ISP { get; set; }
    }
}