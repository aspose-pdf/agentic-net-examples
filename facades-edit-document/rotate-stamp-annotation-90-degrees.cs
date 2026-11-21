using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";
        const int    pageNum   = 1; // 1‑based page index

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        try
        {
            // Load the source PDF
            Document pdfDocument = new Document(inputPdf);

            // Get the target page (Aspose.Pdf pages are 1‑based)
            Page page = pdfDocument.Pages[pageNum];

            // Create an image stamp using the modern Aspose.Pdf.Drawing namespace
            ImageStamp imgStamp = new ImageStamp(stampImg)
            {
                // Rotate 90 degrees clockwise around the stamp centre
                RotateAngle = 90f,
                // Position the stamp – centre of the page in this example
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Ensure the stamp is drawn over the existing content
                Background = false
            };

            // Add the stamp to the selected page
            page.AddStamp(imgStamp);

            // Save the modified document
            pdfDocument.Save(outputPdf);

            Console.WriteLine($"Rotated stamp added. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
