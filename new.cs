using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading;
class FileReadMgr
{
    public void FileRead()
    {
        string[] lines = System.IO.File.ReadAllLines(@"D:\Users\seven\Desktop\test.txt");
        System.Console.WriteLine("下载地址分别为 = ");

        foreach (string line in lines)
        {
            string[] s = line.Split('/');
            Console.WriteLine(line);
            ThreadMgr thread = new ThreadMgr();
            thread.Thread(line, s[s.Length - 1]);
        }
    }
}
class DownLoadMgr
{
    public void Download(string a, string b)
    {
        WebClient webClient = new WebClient();
        webClient.DownloadFile(a, @"D:\Users\seven\Desktop\" + b);
    }
}

class ThreadMgr
{
    public static int i = 0;
    public bool doit = false;
    Thread[] DownThread = new Thread[4];

    public void Thread(string c, string d)
    {
        DownLoadMgr DLM = new DownLoadMgr();
        ThreadStart startDownLoad = new ThreadStart(delegate () {DLM.Download(c,d); });
        Thread downLoadThread = new Thread(startDownLoad);

        if (i < 3)
        {
            DownThread[i] = downLoadThread;
            i++;
        }
        else
        {
            while (true)
            {
                for (i = 0; i <= 3; i++)
                {
                    if (!DownThread[i].IsAlive)
                    {
                        DownThread[i] = downLoadThread;
                        doit = true;
                    }
                }
                if (doit)
                {
                    doit = false;
                    break;
                }
            }
        }

        downLoadThread.Start();


    }

}
class ReadFromFile
{
    static void Main()
    {
        FileReadMgr a = new FileReadMgr();
        a.FileRead();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
    }
}