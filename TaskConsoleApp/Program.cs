using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace TaskConsoleApp
{
    public class Content
    {
        public string Site { get; set; }
        public int Len { get; set; }
    }

    public class Status
    {
        public int ThreadId { get; set; }
        public DateTime Date { get; set; }
    }



    internal class Program
    {

        public static string CacheData { get; set; }
        public async static Task Main(string[] args)
        {
            


            Console.Out.WriteLine("Main Thread: " + Thread.CurrentThread.ManagedThreadId);

            List<string> urlsList = new List<string>()
            {
            "https://www.google.com",
            "https://www.microsoft.com",
            "https://www.amazon.com",
            "https://www.google.com",
            "https://www.microsoft.com",
            "https://www.amazon.com",
            };



            List<Task<Content>> taskList = new List<Task<Content>>();

            urlsList.ToList().ForEach(x =>
            {
                taskList.Add(GetContentAsync(x));
            });



            #region FromResult 
            // parametre olarak bir obje alır. bu obje değerini geriye bir task nesne örneği ile beraber dönmektedir.
            // eğerki bir metotan geriye daha önce almış olduğumuz statik bir datayı dönmek istiyorsam fromResult ı kullanabiliriz.
            // genellikle bu metodu cachelenmiş bir datayı dönmek için kullanırız.Yani bir ifadeyi değeri task olarak geriye dönmemi sağlar.

            //CacheData = await GetDataAsync();

            //Console.WriteLine(CacheData);

            #endregion


            #region StartNew

            //var myTask = Task.Factory.StartNew((Obj) => // StartNew() => Run() metodu ile aynı işlemi gerçekleştiriyor.
            //                                            // içerisine yazmış olduğum kodları aytı bir thread üzerinde çalıştırıyor.
            //                                            // farkı nedir peki? burada metoduma obje geçebiliyorum.
            //                                            // Task işlemi bittiğinde burada geçmiş olduğum objeii alabiliyorum.
            //{
            //    Console.WriteLine("myTask çalıştı.");

            //    var status = Obj as Status; // as => eğer Obj yi status e çevirirse çevirir çeviremezse null döner. is kullanırsakta çevirirse true çeviremezse false döner.

            //    status.ThreadId = Thread.CurrentThread.ManagedThreadId;

            //}, new Status() { Date = DateTime.Now});


            //await myTask;

            //Status s = myTask.AsyncState as Status;

            //Console.WriteLine(s.Date.ToString());
            //Console.WriteLine(s.ThreadId);

            #endregion



            #region WaitAny

            //var firstTaskIndex = Task.WaitAny(taskList.ToArray()); //  => UI Thread'i bloklar. vermiş olduğumuz tasklerden herhangi birisi tamamlanınca bir intdeğer döner. Dönmüş olduğu int değer ise tamamlanan taskin index numarasıdır. Bu index numarası ile ilgili task'i alabilirim
            //                                                       // mevzu UI Thread'i bloklaması.
            //Console.WriteLine($"{taskList[firstTaskIndex].Result.Site} - {taskList[firstTaskIndex].Result.Len}");
            #endregion


            #region WaitAll

            //Console.WriteLine("WaitAll metodundan önce");

            //Task.WaitAll(taskList.ToArray()); // WaitAll => UI Thread'ini bloklar. Gördüğün gibi bekledi ve alttaki kodu çalıştırmadı output'da

            //Console.WriteLine("WaitAll metodundan sonra");

            //Console.WriteLine($"{taskList.First().Result.Site} - {taskList.First().Result.Len}");
            #endregion

            #region WhenAny 
            //var firstData = await Task.WhenAny(taskList); // WhenAny => içine gönderdiğim tasklar arasında en hızlı cevap döneni döndürür, sadece birisini yani.
            //Console.WriteLine($"{firstData.Result.Site} - {firstData.Result.Len}");
            #endregion



            #region WhenAll
            //var contents = await Task.WhenAll(taskList.ToArray()); // WhenAll => task array i alır hepsi tamamlanıncaya kadar bekler.

            //contents.ToList().ForEach(x =>
            //{
            //    Console.WriteLine($"{x.Site} boyut:{x.Len}");
            //});
            #endregion

        }

        public static async Task<Content> GetContentAsync(string url)
        {
            Content content = new Content();
            var data = await new HttpClient().GetStringAsync(url);

            content.Site = url;
            content.Len = data.Length;
            Console.WriteLine("GetContentAsync: " + Thread.CurrentThread.ManagedThreadId);

            return content;
        }

        public static Task<string> GetDataAsync()
        {


            if (String.IsNullOrEmpty(CacheData))
            {
                return File.ReadAllTextAsync("dosya.txt");

            }
            else
            {
                return Task.FromResult<string>(CacheData);
            }
        }
    }
}



//Console.WriteLine("Hello World!");

//var myTask = new HttpClient().GetStringAsync("https://www.google.com").ContinueWith(async (data) => // CountinueWith => bu işlemden sonra diğer işlemi yap. hemen devamında çalış yani. bunun yerine await altında da yazabilirsin. 
//{

//    Console.WriteLine("data uzunluk" + data.Result.Length.ToString());
//});

//Console.WriteLine("Arada yapılacak işler");

//await myTask;