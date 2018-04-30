using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Database;
using Shared.Models;

namespace Shared.WebService
{
    public class WSHttpClient
    {
        private readonly string _basicAuth;

        private readonly string _webApiUrl = "http://shilak96-001-site1.btempurl.com/";

        public string ContentType = "application/json";

        public WSHttpClient()
        {
            var ldb = LocalDb.Instance;
            var localUser = ldb.GetFirstItem<LocalUserModel>();
            _basicAuth = localUser?.Token;
        }

        public (T Answer, Error Error) GetData<T>(string url, string data = null, bool isToken = true)
            where T : class
        {
            var client = _client;
            if (isToken)
                client.DefaultRequestHeaders.Add("authorization", "Bearer " + _basicAuth);
            Console.WriteLine("Bearer " + _basicAuth);
            var request = client.GetAsync(_webApiUrl + url + (string.IsNullOrEmpty(data) ? "" : "?") + data);

            Console.WriteLine(data);
            return HttpResponceWork<T>(request, url);
        }

        public (T Answer, Error Error) GetData<T>(string url, object dataSet = null, bool isToken = true)
            where T : class
        {
            var dataString = dataSet?.DataString();

            return GetData<T>(url, dataString, isToken);
        }

        private (T Anser, Error Error) HttpResponceWork<T>(Task<HttpResponseMessage> message, string url)
            where T : class
        {
            HttpResponseMessage responce = null;
            string answer = null;
            Stopwatch timer = null;

            try
            {
                Console.WriteLine(url);

                timer = Stopwatch.StartNew();

                message.Wait();
                message.Dispose();

                timer.Stop();

                responce = message.Result;
                Console.WriteLine("responce " + responce);
                var tryAnswer = responce.Content.ReadAsStringAsync();
                tryAnswer.Wait();
                answer = tryAnswer.Result;

                Console.WriteLine(answer);
                if (responce.StatusCode == HttpStatusCode.OK)
                {
                    var js = JsonConvert.DeserializeObject<T>(answer);
                    return (js, null);
                }

                return ErrorServerWork<T>(answer, responce, url);
            }
            catch (HttpRequestException requestException)
            {
                return (null, null);
            }
            catch (AggregateException aggregate)
            {
                if (aggregate.InnerExceptions.Any(w => w is HttpRequestException || w is TaskCanceledException))
                    return (null, null);

                return ErrorServerWork<T>(answer, responce, url);
            }
            catch (Exception e)
            {
                return ErrorServerWork<T>(answer, responce, url);
            }
            finally
            {
                timer?.Stop();
            }
        }

        private (T Anser, Error Error) ErrorServerWork<T>(string message, HttpResponseMessage responce, string url)
            where T : class
        {
            try
            {
                var serializedForm = JsonConvert.DeserializeObject<Error>(message);

                if (serializedForm == null)
                {
                    return (null, new Error
                    {
                        ErrorCode = responce?.StatusCode.ToString(),
                        ErrorDescription = responce?.ToString()
                    });
                }

                return (null, serializedForm);
            }
            catch
            {
                return (null, new Error
                {
                    ErrorCode = responce?.StatusCode.ToString(),
                    ErrorDescription = message
                });
            }
        }

        public (T Answer, Error Error) PostData<T>(string url, string data = null, bool isToken = true)
            where T : class
        {
            Console.WriteLine("data" + data);
            var client = _client;
            if (isToken)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _basicAuth);
            
            var content = new StringContent(data, Encoding.UTF8, ContentType);

            var request = client.PostAsync(_webApiUrl + url, content);

            Console.WriteLine(data);
            return HttpResponceWork<T>(request, url);
        }

        public (T Answer, Error Error) PostData<T>(string url, object dataSet = null, bool isToken = true)
            where T : class
        {
            var dataString = dataSet?.DataString();

            return PostData<T>(url, dataString, isToken);
        }

        public (T Answer, Error Error) PutData<T>(string url, object dataSet = null, bool isToken = true)
            where T : class
        {
            var dataString = dataSet?.DataString();

            return PutData<T>(url, dataString, isToken);
        }

        public (T Answer, Error Error) PutData<T>(string url, string data = null, bool isToken = true)
            where T : class
        {
            var client = _client;
            if (isToken)
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _basicAuth);
            
            var content = new StringContent(data ?? "", Encoding.UTF8, ContentType);

            var request = client.PutAsync(_webApiUrl + url, content);

            Console.WriteLine(data);
            return HttpResponceWork<T>(request, url);
        }

        public (string isOk, Error Error) DeleteData(string url, object bodyDataset = null, bool isToken = true)
        {
            var client = _client;
            if (isToken)
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _basicAuth);
            }

            var dataString = bodyDataset?.DataString();
            var request = client.DeleteAsync(_webApiUrl + url + (string.IsNullOrEmpty(dataString) ? "" : "?") + dataString);

            return HttpResponceWork<string>(request, url);
        }

        public (bool isOk, Error Error) RecoveryPassword(string url, object bodyDataset = null, bool isToken = true)
        {
            var client = _client;

            var dataString = bodyDataset?.DataString();
            var stringContent = new StringContent(dataString, Encoding.UTF8, ContentType);
            var request = new HttpRequestMessage(HttpMethod.Delete, _webApiUrl + url) { Content = stringContent };

            try
            {
                var answer = client.SendAsync(request);
                answer.Wait();

                if (answer.Result.IsSuccessStatusCode)
                    return (true, null);

                var answerStr = answer.Result.Content.ReadAsStringAsync().Result;

                try
                {
                    var err = JsonConvert.DeserializeObject<Error>(answerStr);
                    return (false, err);
                }
                catch (Exception e)
                {
                    return (false, new Error
                    {
                        ErrorCode = answer.Result.StatusCode.ToString(),
                        ErrorDescription = answerStr
                    });
                }
            }
            catch (Exception e)
            {
                return (false, null);
            }
        }

        private System.Net.Http.HttpClient _client => new System.Net.Http.HttpClient
        {
            DefaultRequestHeaders =
            {
                Accept = {new MediaTypeWithQualityHeaderValue(ContentType) },
                AcceptCharset = {new StringWithQualityHeaderValue("utf-8")}
            },
            Timeout = new TimeSpan(0, 0, 0, 30)
        };
    }
}