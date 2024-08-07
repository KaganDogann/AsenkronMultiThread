using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ForEachParallel
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int total = 0;
            // buradaki mevzu ForEach metodunu daha performanslı şekilde çalıştırmak.
            // 1+..100 yacağım mesela 4 tane thread'im olsun ilk thread tek başına 1 den 25 e kadar toplasın,
            // diğer thread 25 den 50 ye kadar olanı toplasın diğeri de 50 den 75 e kadar toplasın,
            // son olarak son thread 75 den 100 e kadar olanları toplasın ve en son bunları kendi aralarında toplasınlar.


            // x => range içinde ki değeri temsil ediyor.
            // loop => döngünün kendisini temsil ediyor.
            // subtotal => o thread'in kendi içerisinde hesaplayacağı değer.
            // y => subtotal'i ifade ediyor
            Parallel.ForEach(Enumerable.Range(1, 100).ToList(), () => 0, (x, loop, subtotal) =>
            {
                subtotal += x;
                return subtotal;
            },(y) => Interlocked.Add(ref total, y));

            Console.WriteLine(total);

            #region Race Condition Interlocked
            ////Race conditon bir değişken üzerinde birden fazla thread işlem yaparsa bu değişkenin bayat veri ile çalışması demek.
            ////sayi = 1; değerinde iken 2tane thread buna +5 eklemek istesin, sıra sıra eklemeleri gerek yoksa değer 11 değil 6 olur patlarız.
            //long FilesByte = 0;

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //string picturePath = @"C:\Users\pc\Desktop\Pictures";

            //var files = Directory.GetFiles(picturePath);

            //#region Parallel.ForEach-2

            //Parallel.ForEach(files, file => // İşlem bitti. 102ms
            //{
            //    Console.WriteLine("thread no: " + Thread.CurrentThread.ManagedThreadId);

            //    FileInfo f = new FileInfo(file);

            //    Interlocked.Add(ref FilesByte, f.Length); // Bu arkadaş çalışırken FilesByte'a yanında ki değer eklenene kadar başka bir thread'in FilesByte'a erişmesini engelliyor.
            //});

            //#endregion

            #endregion

            #region Parallel ForEach MultiThread Çalışma

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //string picturePath = @"C:\Users\pc\Desktop\Pictures";

            //var files = Directory.GetFiles(picturePath);

            ////İçerisine almış olduğüğu bir array i farklı thread'lerde çalıştırarak farklı threadlerde işlemi gerçekleştirir.
            ////İlgili array i parçalara ayırıp her parçalara belirli thread atayıp işlemi yaptırıyor.
            ////Düşük hacimli arraylerde daha yavaş çalışabilir. hacim şart. Mutlaka test etmemiz gerek. (her bir thread i parçalaması arralere dağıtması daha masraflı olursa daha yavaş çalışabilir multithread çalışma.)

            //#region Parallel.ForEach
            //Parallel.ForEach(files, file => // İşlem bitti. 102ms
            //{
            //    Console.WriteLine("thread no: " + Thread.CurrentThread.ManagedThreadId);

            //    Image img = new Bitmap(file);

            //    var thumbnail = img.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);

            //    thumbnail.Save(Path.Combine(picturePath, "thumbnail", Path.GetFileName(file)));
            //});

            //sw.Stop();

            //Console.WriteLine("İşlem bitti. " + sw.ElapsedMilliseconds);
            //#endregion


            //sw.Reset();

            //sw.Start();
            //#region Senkron çalışma
            //files.ToList().ForEach(x => // İşlem bitti. 180ms
            //{
            //    Console.WriteLine("thread no: " + Thread.CurrentThread.ManagedThreadId);

            //    Image img = new Bitmap(x);

            //    var thumbnail = img.GetThumbnailImage(50, 50, () => false, IntPtr.Zero);

            //    thumbnail.Save(Path.Combine(picturePath, "thumbnail", Path.GetFileName(x)));
            //});
            //sw.Stop();
            //Console.WriteLine("İşlem bitti. " + sw.ElapsedMilliseconds);
            //#endregion

            #endregion



        }
    }
}
