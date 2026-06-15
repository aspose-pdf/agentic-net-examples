using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: create → load → save)
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with custom format "current/total"
            // The character '#' is replaced by the page number; using two placeholders
            // yields "1/10", "2/10", etc.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("#/#");

            // Optional visual settings (centered at bottom of each page)
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin = 20; // distance from bottom edge

            // Font and color must be set via the TextState object (FontSize is read‑only on the stamp itself)
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
