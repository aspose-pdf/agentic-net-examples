using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // PdfViewer handles printing without showing any UI
            PdfViewer viewer = new PdfViewer();
            try
            {
                viewer.BindPdf(doc);          // Attach the document to the viewer
                viewer.PrintDocument();       // Print silently to the default printer
            }
            finally
            {
                viewer.Close();               // Release native resources
            }
        }

        Console.WriteLine("Print job sent to the default printer.");
    }
}