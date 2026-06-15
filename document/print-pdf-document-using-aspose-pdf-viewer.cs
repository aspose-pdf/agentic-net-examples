using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(pdfPath))
            {
                // PdfViewer provides the built‑in Print functionality
                PdfViewer viewer = new PdfViewer();
                try
                {
                    // Bind the document to the viewer
                    viewer.BindPdf(doc);
                    // Print to the default printer
                    viewer.PrintDocument();
                }
                finally
                {
                    // Ensure resources are released
                    viewer.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}