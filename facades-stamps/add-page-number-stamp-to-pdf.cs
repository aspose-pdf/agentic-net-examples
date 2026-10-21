using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // required for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the {page_number} placeholder.
            // Aspose.Pdf.Facades will replace this token with the actual page number at save time.
            TextStamp pageNumberStamp = new TextStamp("Page {page_number}");

            // Optional visual formatting
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            // Use XIndent/YIndent instead of the non‑existent Margin property
            pageNumberStamp.XIndent = 0;          // centered, so no horizontal offset
            pageNumberStamp.YIndent = 20;         // distance from bottom edge (points)
            pageNumberStamp.TextState.Font      = FontRepository.FindFont("Arial");
            pageNumberStamp.TextState.FontSize  = 12;
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}