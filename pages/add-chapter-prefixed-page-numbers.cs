using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with custom format "Chapter #"
            // The character '#' will be replaced by the page number.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Chapter #");

            // Position the stamp at the bottom center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin       = 20; // distance from bottom edge

            // Optional styling
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.Font      = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;

            // Apply the stamp to every page in the document
            for (int i = 1; i <= doc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = doc.Pages[i];
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with prefix \"Chapter\" added. Saved to '{outputPath}'.");
    }
}
