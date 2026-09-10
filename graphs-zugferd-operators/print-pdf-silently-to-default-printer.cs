using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfViewer resides here

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the PDF file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfViewer provides silent printing to the default printer
        PdfViewer viewer = new PdfViewer();
        try
        {
            viewer.BindPdf(inputPath);   // Load the PDF
            viewer.PrintDocument();      // Send to default printer without a dialog
        }
        finally
        {
            viewer.Close();              // Release resources
        }

        Console.WriteLine("Print job sent to the default printer.");
    }
}