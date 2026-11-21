using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for FontRepository, Font, TextState

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp that will automatically update when pages are added or removed
            PageNumberStamp pageNumberStamp = new PageNumberStamp();
            // The default start number is 1, so we omit the non‑existent StartNumber property.
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 10;

            // Configure the text appearance via the existing TextState instance
            pageNumberStamp.TextState.Font          = FontRepository.FindFont("Arial");
            pageNumberStamp.TextState.FontSize      = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every existing page (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
