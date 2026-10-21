using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

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

        // Create an ImageStamp with 30% opacity and set it as a background element.
        ImageStamp imgStamp = new ImageStamp(stampImg)
        {
            Opacity = 0.3,                 // 30% opacity
            Background = true,             // place behind existing page content
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment   = VerticalAlignment.Center
        };

        // Load the source PDF using Document (avoids PdfStampFacade which may be unavailable).
        Document pdfDocument = new Document(inputPdf);

        // Apply the stamp to every page (pages are 1‑based in Aspose.Pdf).
        for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
        {
            pdfDocument.Pages[pageNum].AddStamp(imgStamp);
        }

        // Save the stamped PDF.
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Background stamp applied with 30% opacity. Saved to '{outputPdf}'.");
    }
}
