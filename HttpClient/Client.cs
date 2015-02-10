using System.IO;
using System.Net;
using System.Text;

namespace HttpClient
{
  public class Client
  {
    // Адрес
    private readonly string _url;
    // Порт
    private readonly string _port;

    // Конструктор
    public Client(string url, string port)
    {
      _url = url;
      _port = port;
    }

    // Отдать ответ задачи серверу
    public bool WriteAnswer(string serializedObject)
    {
      var response = (HttpWebResponse)GetResponse("writeAnswer", serializedObject);
      return response != null;
    }

    // Получить входные данные для задачи
    public string GetInputData()
    {
      var response = (HttpWebResponse)GetResponse("getInputData", null);
      return response == null ? string.Empty : new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
    }

    // Проверить, находится ли сервер в рабочем состоянии
    public bool Ping()
    {
      var response = (HttpWebResponse)GetResponse("ping", null);
      return response != null && response.StatusCode == HttpStatusCode.OK;
    }

    // Получаем ответ
    private HttpWebResponse GetResponse(string method, string body)
    {
      body = body ?? string.Empty;
      var request = WebRequest.Create(string.Format("{0}:{1}/{2}", _url, _port, method));
      request.Timeout = 200;
      request.ContentLength = Encoding.UTF8.GetByteCount(body);
      request.Method = (body == string.Empty) ? "GET" : "POST";

      if (body.Length > 0)
      {
        using (var stream = request.GetRequestStream())
        {
          stream.Write(Encoding.UTF8.GetBytes(body), 0, (int)request.ContentLength);
        }
      }

      try
      {
        return (HttpWebResponse)request.GetResponse();
      }
      catch
      {
        return null;
      }
    }
  }
}
