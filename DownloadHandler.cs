using System.Diagnostics;
using System.Net;

public class DownloadHandler
{
    public static async Task Execute(string downloadURL, string downloadPath)
    {

        var fileName = Path.GetFileName(new Uri(downloadURL).AbsolutePath);
        var finalDownloadPath = Path.Combine(downloadPath, fileName);

        var httpHandler = new HttpClientHandler { AllowAutoRedirect = true, MaxConnectionsPerServer = 10 };
        using var httpClient = new HttpClient(httpHandler);
        using var response = await httpClient.GetAsync(downloadURL, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        var totalBytes = response.Content.Headers.ContentLength ?? -1L;

        var canReportProgress = totalBytes != -1;

        using var contentStream = await response.Content.ReadAsStreamAsync();
        using var fileStream = new FileStream(finalDownloadPath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 8192, true);

        var buffer = new byte[1024 * 128];
        long totalRead = 0L;
        int bytesRead;
        int lastDraw = -1;
        int barWidth = 30;

        var stopwatch = Stopwatch.StartNew();

        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
            totalRead += bytesRead;

            if (canReportProgress)
            {
                var percent = (int)(totalRead * 100 / totalBytes);

                if (percent != lastDraw)
                {
                    lastDraw = percent;

                    // calculate download speed
                    double seconds = stopwatch.Elapsed.TotalSeconds;
                    double kbps = totalRead / 1024.0 / seconds;
                    string speed = kbps >= 1024
                        ? $"({kbps / 1024.0:0.00} MB/s)"
                        : $"({kbps:0.00} KB/s)";


                    // progress bar
                    int filledBars = percent * barWidth / 100;
                    string bar = new string('=', filledBars) + ">" + new string(' ', barWidth - filledBars);

                    Console.CursorLeft = 0;
                    Console.Write($"[{bar}] {percent,3}% @ {speed}");
                }


            }
        }

        Console.Write("\nDownload Complete");



    }
}