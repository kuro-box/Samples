using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApi.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteAction(CallWebApiTest);
            //ExecuteAction(CallWebApi);
        }

        private static void ExecuteAction(Action action)
        {
            try { action.Invoke(); }
            catch (Exception ex) { Console.WriteLine(JsonConvert.SerializeObject(ex)); }
            Console.Read();
        }

        private static void Print()
        {
            Console.WriteLine("Helloworld");
        }

        private const string stream_get_url = "http://localhost:8087/api/test/stream";

        private static void CallWebApi()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(stream_get_url).Result;
            //var content = response.Content.ReadAsStringAsync();
            //var result = JsonConvert.SerializeObject(content);
            var result = response.Content.ReadAsStreamAsync().Result;

            var buffer = new byte[result.Length];
            result.Read(buffer, 0, buffer.Length);
            result.Seek(0, SeekOrigin.Begin);

            var msg = Encoding.ASCII.GetString(buffer);

            Console.WriteLine(msg);
        }

        private static void CallWebApiTest()
        {
            var result = new Result<byte[]>();
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(stream_get_url);
            #region init request
            //request.Proxy = new WebProxy(pWebProxyString); //WebProxy.GetDefaultProxy(); 
            //request.Method = pMethod; //"post";
            //request.Timeout = timeOutSecond * 1000;         // 90秒超时时间
            //request.ContentType = pContentType; //"application/vnd.ngtp.org.if1.message+asn1";
            //request.Accept = pAccept; //"application/vnd.ngtp.org.if1.message+asn1";
            //request.KeepAlive = false;
            //request.Headers.Add(pHeader);
            //post数据
            //if (pPostBody != null)
            //{
            //    using (var stream = request.GetRequestStream())
            //    {
            //        stream.Write(pPostBody, 0, pPostBody.Length);
            //    }
            //}
            #endregion
            try
            {
                var response = (System.Net.HttpWebResponse)request.GetResponse();
                result.Location = response.Headers["Location"];
                result.Code = (int)response.StatusCode;
                result.Message = response.StatusDescription;
                var mstream = new MemoryStream();
                var tmp = new byte[2048];
                using (var stream = response.GetResponseStream())
                {
                    if (stream != null)
                    {
                        int count;
                        try
                        {
                            do
                            {
                                count = stream.Read(tmp, 0, tmp.Length);
                                mstream.Write(tmp, 0, count);
                            } while (count != 0);
                        }
                        catch { }
                    }
                }
                result.Data = mstream.ToArray();
            }
            catch (System.Net.WebException ex)
            {
                var response = (System.Net.HttpWebResponse)ex.Response;
                if (response != null)
                {
                    var statusCode = (int)response.StatusCode;
                    var json = "";
                    var stream = response.GetResponseStream();
                    if (stream != null)
                    {
                        using (var sr = new StreamReader(stream))
                            json = sr.ReadToEnd();
                    }
                    var error = FromJson<ErrorResponse>(json, Encoding.UTF8);
                    if (error != null)
                        result.Message = error.errorLabel + "(" + error.errorDescription + ")";
                    else
                        result.Message = json;  //"Http request error!";
                }
                else
                {
                    result.Message = ex.Message;
                    result.Code = 999;
                }
            }
            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        public class Result
        {
            public int Code { get; set; }

            public string Location { get; set; }

            public string Message { get; set; }
        }

        public class Result<T> : Result
        {
            public T Data { get; set; }
        }

        public class ErrorResponse
        {
            /// <summary>
            /// Label
            /// </summary>
            [System.Xml.Serialization.XmlElement(ElementName = "errorLabel")]
            public string errorLabel { get; set; }
            /// <summary>
            /// Message
            /// </summary>
            [System.Xml.Serialization.XmlElement(ElementName = "errorDescription")]
            public string errorDescription { get; set; }
        }

        public static T FromJson<T>(string data, Encoding encoding)
        {
            try
            {
                T ot;
                using (var ms = new MemoryStream(encoding.GetBytes(data)))
                {
                    var serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
                    ot = (T)serializer.ReadObject(ms);
                }
                return ot;
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch { }
            // ReSharper restore EmptyGeneralCatchClause
            return default(T);
        }
    }
}
