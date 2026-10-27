using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            int pageCount = doc.Pages.Count; // 1‑based page count

            // Iterate using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= pageCount; i++)
            {
                Page page = doc.Pages[i];

                // Create a dynamic footer stamp showing the current page number
                TextStamp pageNumberStamp = new TextStamp($"Page {i} of {pageCount}");
                // Position the stamp at the bottom center of the page
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
                pageNumberStamp.BottomMargin        = 20; // distance from the bottom edge (points)

                // Styling – modify the existing TextState instance (read‑only property)
                pageNumberStamp.TextState.FontSize        = 12;
                pageNumberStamp.TextState.Font            = FontRepository.FindFont("Arial");
                pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the stamp to the current page
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
