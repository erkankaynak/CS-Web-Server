using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
namespace WebServer
{
    class Program
    {
        public static string serverDirectory = "d:\\server-directory\\";

        static void Main(string[] args)
        {
            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://127.0.0.1:90/");
            server.Prefixes.Add("http://localhost:90/");
            server.Start();

            Console.WriteLine("server started...");

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                Console.WriteLine("a client connected...");

                StreamWriter response = new StreamWriter(context.Response.OutputStream);
                string pageRequest = context.Request.RawUrl;

                byte[] fileArray;

                if (File.Exists(serverDirectory + pageRequest))
                {
                    using (FileStream file = new FileStream(serverDirectory + pageRequest, FileMode.Open))
                    {
                        fileArray = new byte[file.Length];
                        file.Read(fileArray, 0, (int)file.Length);
                    }

                    response.BaseStream.Write(fileArray, 0, fileArray.Length);
                    response.Close();
                }

                context.Response.Close();
            }
        }
    }
}
