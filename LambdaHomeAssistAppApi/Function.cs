using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using static LambdaHomeAssistAppApi.KawaguchiBinDay;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LambdaHomeAssistAppApi
{
    public class Function
    {
        readonly string ACCESSTOKEN = Environment.GetEnvironmentVariable("ACCESSTOKEN");
        readonly string LINEAPIURL = "https://api.line.me/v2/bot/message/broadcast";

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(object input, ILambdaContext context)
        {
            var today = DateTime.UtcNow.AddHours(9);    // Convert UTC to JST
            //var today = new DateTime(2021, 5, 13).AddHours(9);    // Convert UTC to JST

            // Get bin type for today and tomorrow.
            var binSchedule = KawaguchiBinDay.GetBinSchedule(today);

            // Create text for LINE message.
            StringBuilder messageText = GetMessageText(today, binSchedule);

            // Convert result text into Json to post LINE API.
            string messageBodyJson = ConvertMessageTextIntoMessageBodyJson(messageText);

            // post
            using (var client = new HttpClient())
            {
                var req = new HttpRequestMessage(HttpMethod.Post, LINEAPIURL);
                req.Headers.Add("ContentType", "application/json");
                req.Headers.Add("Authorization", "Bearer " + ACCESSTOKEN);
                req.Content = new StringContent(messageBodyJson, Encoding.UTF8, "application/json");

                var response = client.SendAsync(req).Result;
            }

            return new APIGatewayProxyResponse
            {
                IsBase64Encoded = false,
                StatusCode = 200,
                Body = messageText.ToString()
            };
        }

        private StringBuilder GetMessageText(DateTime today, RecycleCalendar binSchedule)
        {
            StringBuilder result = new StringBuilder();

            // bin for today
            result.AppendLine($"{today.Month}/{today.Day} {today.DayOfWeek}");
            binSchedule.TodayBin.ForEach(x => result.AppendLine($"・{x.BinInEnglish} {x.BinInJapanese}"));
            if (!binSchedule.TodayBin.Any())
            {
                result.AppendLine("　No bin");
            }

            result.AppendLine("");
            result.AppendLine("------------------------------------");

            // bin for tomorrow
            result.AppendLine($"{today.AddDays(1).Month}/{today.AddDays(1).Day} {today.AddDays(1).DayOfWeek}");
            binSchedule.TomorrowsBin.ForEach(x => result.AppendLine($"・{x.BinInEnglish} {x.BinInJapanese}"));
            if (!binSchedule.TomorrowsBin.Any())
            {
                result.AppendLine("　No bin");
            }

            return result;
        }

        private string ConvertMessageTextIntoMessageBodyJson(StringBuilder result)
        {
            var d = new Dictionary<string, string>
            {
                {"type", "text" },
                {"text", result.ToString() }
            };

            var messages = new object[1] { d };

            var body = new Dictionary<string, object>()
            {
                {
                    "messages", messages
                },
            };

            return JsonSerializer.Serialize(body);
        }
    }
}
