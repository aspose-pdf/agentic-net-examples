using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "metadata.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input PDF not found: " + inputPath);
            return;
        }

        // Load PDF metadata using PdfFileInfo facade
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);
        string title = pdfInfo.Title;

        // Write the Title value to a simple log file
        string logContent = "Title: " + (title ?? string.Empty);
        File.WriteAllText(logPath, logContent);

        Console.WriteLine("Title metadata written to " + logPath);
    }
}