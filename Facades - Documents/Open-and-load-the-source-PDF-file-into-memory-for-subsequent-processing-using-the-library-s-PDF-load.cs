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

        // Load the PDF file into a memory stream so it can be used by multiple Aspose APIs
        byte[] pdfBytes = File.ReadAllBytes(inputPath);
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        {
            // ------------------------------------------------------------
            // 1️⃣ Get basic information (e.g., page count) using Document
            // ------------------------------------------------------------
            Document pdfDoc = new Document(pdfStream);
            Console.WriteLine($"Pages loaded: {pdfDoc.Pages.Count}");

            // ------------------------------------------------------------
            // 2️⃣ Bind the same stream to PdfViewer for further processing
            // ------------------------------------------------------------
            // Reset the stream position because Document has read it to the end
            pdfStream.Position = 0;
            using (PdfViewer viewer = new PdfViewer())
            {
                viewer.BindPdf(pdfStream);
                // The PDF is now loaded in the viewer – you can add rendering, extraction, etc.
                // Example placeholder for further viewer operations:
                // int renderedPage = viewer.GetPageCount(); // (use viewer methods that actually exist)
            }
        }
    }
}
