using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with default format "#"
            PageNumberStamp pageNumberStamp = new PageNumberStamp();

            // Configure superscript‑like appearance (smaller font size)
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.FontSize = 8; // smaller than typical body text
            // Use ForegroundColor instead of the non‑existent FontColor property
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Position the stamp at the bottom‑right corner of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Right;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20; // distance from bottom edge
            pageNumberStamp.RightMargin  = 20; // distance from right edge

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with superscript formatting added to '{outputPath}'.");
    }
}
