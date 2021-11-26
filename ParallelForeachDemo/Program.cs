using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForeachApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> urls = new List<string>() {
                "http://intuit.ru",
                "http://rbc.ru",
                "http://ozon.ru",
                "http://google.com",
                "http://mail.ru",
                "http://lenta.ru"
            };

            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            foreach (string url in urls)
            {
                WebClient client = new WebClient();
                Console.WriteLine("Скачиваем : {0}", url);
                try
                {
                    client.DownloadString(url);
                } catch (WebException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }

            long elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine("Затраченное время в миллисекундах: {0}", elapsed);
            sw.Stop();

            Console.WriteLine("============================");
            Thread.Sleep(1000); // Приостановить главный поток на 1 сек.

            sw.Restart();
            Parallel.ForEach(urls, url =>
            {
                WebClient client = new WebClient();
                Console.WriteLine("Скачиваем : " + url);
                try
                {
                    client.DownloadString(url);
                }
                catch (WebException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            });
            elapsed = sw.ElapsedMilliseconds;
            Console.WriteLine("Затраченное время в миллисекундах: {0}", elapsed);
            sw.Stop();

            Console.ReadLine();
        }
    }
}

