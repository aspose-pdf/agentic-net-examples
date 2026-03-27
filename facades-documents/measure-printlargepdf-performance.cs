using System;
using System.Diagnostics;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "large.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Measure performance when using a file path
        Stopwatch pathTimer = new Stopwatch();
        using (PdfViewer pathViewer = new PdfViewer())
        {
            pathViewer.AutoResize = true;
            pathViewer.AutoRotate = true;
            pathViewer.PrintPageDialog = false;
            pathTimer.Start();
            pathViewer.PrintLargePdf(inputPath);
            pathTimer.Stop();
        }

        // Measure performance when using a stream
        Stopwatch streamTimer = new Stopwatch();
        using (PdfViewer streamViewer = new PdfViewer())
        {
            streamViewer.AutoResize = true;
            streamViewer.AutoRotate = true;
            streamViewer.PrintPageDialog = false;
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                streamTimer.Start();
                streamViewer.PrintLargePdf(fileStream);
                streamTimer.Stop();
            }
        }

        Console.WriteLine($"PrintLargePdf(string) elapsed: {pathTimer.ElapsedMilliseconds} ms");
        Console.WriteLine($"PrintLargePdf(Stream) elapsed: {streamTimer.ElapsedMilliseconds} ms");
    }
}