using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Domain;
using Serealization;

namespace Http_Server
{
  public class Server : IDisposable
  {
    // Слушатель протокола HTTP.
    private readonly HttpListener _listener;
    // Предоставляет доступ к объектам запросов и ответов.
    private readonly HttpListenerContext _context;
    // Тело запроса.
    private string _requestBody;
    // Тело ответа.
    private string _answerBody;
    
    // Служит признком того, что сервер работает.
    private void Ping()
    {
      SendResponse(null);
    }

    // Остановить сервер.
    private void Stop()
    {
      SendResponse(null);
      Dispose();
    }

    // Запросить ответ задачи.
    private void GetAnswer()
    {
      if (_requestBody != string.Empty)
        SendResponse(_answerBody);
      else
        SendResponse(null);
    }

    // Послать входные данные.
    private void PostInputData()
    {
      using (var streamReader = new StreamReader(_context.Request.InputStream, _context.Request.ContentEncoding))
      {
        _requestBody = streamReader.ReadToEnd();
        const string type = "json";
        var inputSerializer = Serializer<Input>.CreateSerializer(type);
        var outputSerializer = Serializer<Output>.CreateSerializer(type);
        var input = inputSerializer.Deserialize(_requestBody);
        _answerBody = outputSerializer.Serialize(new Calculations().Calc(input));
      }
      SendResponse(null);
    }

    // Отправить ответ.
    private void SendResponse(string body)
    {
      //Костыль   
      var response = _context.Response;
      body = body ?? string.Empty;
      response.StatusCode = (int) HttpStatusCode.OK;
      response.ContentEncoding = Encoding.UTF8;
      response.ContentLength64 = Encoding.UTF8.GetByteCount(body);
      using (var stream = response.OutputStream)
      {
        stream.Write(Encoding.UTF8.GetBytes(body), 0, (int)response.ContentLength64);
      }
    }

    // Ошибка.
    private void SendInfo()
    {
      //Костыль   
      var response = _context.Response;
      string body = string.Empty;
      response.StatusCode = (int)HttpStatusCode.NotFound;
      response.ContentEncoding = Encoding.UTF8;
      response.ContentLength64 = Encoding.UTF8.GetByteCount(body);
      using (var stream = response.OutputStream)
      {
        stream.Write(Encoding.UTF8.GetBytes(body), 0, (int)response.ContentLength64);
      }
    }    

    private void GetMethod(string methodName)
    {
      var method = GetType()
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
        .FirstOrDefault(m => m.Name.ToLower() == methodName);
      if (method != null)
        method.Invoke(this, new object[] { });
      else
        SendInfo();
    }

    // Конструктор.
    public Server(string url, bool local)
    {
      _listener = new HttpListener();
      _listener.Prefixes.Add(url);
      _listener.Start();
      _requestBody = string.Empty;

      while (_listener.IsListening)
      {
        _context = _listener.GetContext();
        string methodName = _context.Request.Url.LocalPath.Substring(1).ToLower();
        GetMethod(methodName);
      }
    }

    // Освобождаем ресурсы для потребителей HttpServer
    public void Dispose()
    {
        _listener.Stop();
        GC.SuppressFinalize(this);
    }
  }
}