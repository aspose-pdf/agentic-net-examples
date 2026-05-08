using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfViewer, bind the PDF, and print silently to the default printer.
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(inputPath);
            viewer.PrintDocument(); // Sends the document to the default printer without showing a dialog.
        }
        finally
        {
            viewer.Close(); // Ensure resources are released.
        }

        Console.WriteLine("Print job sent to the default printer.");
    }
}