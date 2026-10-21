using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // optional, kept for consistency

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "stamped_even_pages.pdf";
        const string stampImg = "stamp.png";

        // Verify required files exist
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

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);
        int totalPages = pdfDocument.Pages.Count;

        // Create an ImageStamp that will be applied to pages
        ImageStamp imgStamp = new ImageStamp(stampImg)
        {
            // Position the stamp (example: bottom‑right corner)
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment   = VerticalAlignment.Bottom,
            // Make the stamp semi‑transparent
            Opacity = 0.5,
            // Do not place the stamp behind page content
            Background = false
        };

        // Apply the stamp only to even‑numbered pages using Page.AddStamp
        for (int pageNum = 1; pageNum <= totalPages; pageNum++)
        {
            if (pageNum % 2 == 0)
            {
                pdfDocument.Pages[pageNum].AddStamp(imgStamp);
            }
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);
        Console.WriteLine($"Even‑page stamping completed. Output saved to '{outputPdf}'.");
    }
}
