using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;

namespace Utube.ConsoleTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //s_lock = new object();
            var video2 = new YoutubeVideo("bBmBPXS7xEA");
            var client = new WebClient();
            client.DownloadProgressChanged += DownloadProgressChange;
            client.DownloadFileCompleted += DownloadCompleted;
            s_startTime = DateTime.Now;
            s_lastUpdate = DateTime.Now;
            client.DownloadFileAsync(video2.FormatsAvailable[0].VideoUrl, CleanFileName(video2.Title) + video2.FormatsAvailable[0].Extension);
            Console.ReadLine();

            //Console.WriteLine("Downloading & parsing {0} playlist...", "PL4yXuCu4RM9zLDmDPSlBd32vmAL0AKq9O");
            //var playlist = new YoutubePlaylist("PL4yXuCu4RM9zLDmDPSlBd32vmAL0AKq9O");
            //Console.WriteLine("{0} videos in playlist \"{1}\".", playlist.Count, playlist.Title);

            //for (int i = 0; i < playlist.Count; i++)
            //{
            //    var video = playlist[i];
            //    video.Refresh();
            //    Console.WriteLine();
            //    Console.WriteLine(video.Url);
            //    Console.WriteLine("Title: {0}", video.Title);
            //    Console.WriteLine("Description:\n{0}", video.Description);
            //    Console.WriteLine("Length: {0}", video.Length);
            //    Console.WriteLine("Views: {0}", video.Views);
            //    Console.WriteLine();
            //    Console.ReadLine();
            //}

            Console.ReadLine();

            //-GVo0a3nnyg
        }

        public static void Main1()
        {
            while (true)
            {
                try
                {
                    Console.Write("Youtube Url: ");
                    var url = Console.ReadLine();
                    var video = new YoutubeVideo(new Uri(url), VideoRefreshFlags.None);

                    Console.WriteLine("Refreshing...");
                    Console.WriteLine();
                    video.Refresh();

                    //Console.WriteLine(video.Url);
                    Console.WriteLine("Title: {0}", video.Title);
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Description:\n{0}", video.Description);
                    Console.WriteLine("-------------------------");
                    Console.WriteLine("Length: {0}", video.Length);
                    Console.WriteLine("Views: {0}", video.Views);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        public static void m()
        {
            //var list = new List<int>();
            //var k =list[2];
            while (true)
            {
                try
                {
                    Console.Write("Youtube Playlist ID: ");
                    var id = Console.ReadLine();
                    var playlist = new YoutubePlaylist(id, PlaylistRefreshFlags.All);

                    Console.WriteLine("Refreshing...");
                    Console.WriteLine();
                    //playlist.Refresh();

                    //Console.WriteLine(video.Url);
                    Console.WriteLine("Title: {0}", playlist.Title);
                    Console.WriteLine("Length: {0}", playlist.Count);
                    Console.WriteLine("Views: {0}", playlist.Views);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private static string CleanFileName(string fileName)
        {
            return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }

        private static void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Done!");
        }

        private static DateTime s_startTime;
        private static DateTime s_lastUpdate;
        private static long s_lastBytesReceived;
        private static object s_lock;

        private static void DownloadProgressChange(object sender, DownloadProgressChangedEventArgs e)
        {
            if (e.BytesReceived < s_lastBytesReceived)
                return;

            var now = DateTime.Now;
            if (now < s_lastUpdate)
                return;

            var avgSpeed = (e.BytesReceived / (now - s_startTime).TotalSeconds) / 1024;
            avgSpeed = Math.Round(avgSpeed, 2);

            var bytesDiff = e.BytesReceived - s_lastBytesReceived;
            var timeDiff = (now - s_lastUpdate).TotalSeconds;
            if (timeDiff < 1)
                return;

            var speed = (bytesDiff / timeDiff) / 1024;
            speed = Math.Round(speed, 2);

            if (speed > 1000)
            {

            }

            Console.WriteLine("{0}% {1}/{2} at {3} kb/s, avg {4} kb/s", e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive, speed, avgSpeed);
            s_lastUpdate = now;
            s_lastBytesReceived = e.BytesReceived;
        }
    }
}
