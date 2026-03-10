using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // PdfViewer does not implement IDisposable, so ensure Close() is called.
        PdfViewer viewer = new PdfViewer();
        try
        {
            // Load the PDF file into the viewer.
            viewer.BindPdf(pdfPath);

            // Optional: adjust printing behavior.
            viewer.AutoResize = true;      // fit to printable area
            viewer.AutoRotate = true;      // rotate pages if needed
            viewer.PrintPageDialog = false; // suppress page range dialog

            // Print using the default printer and default settings.
            viewer.PrintDocument();
        }
        finally
        {
            // Release resources held by the viewer.
            viewer.Close();
        }

        Console.WriteLine("Print job sent successfully.");
    }
}