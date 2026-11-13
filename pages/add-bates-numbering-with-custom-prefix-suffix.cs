using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp that will be applied to each page.
            // {pageNumber} is a placeholder that Aspose.Pdf replaces with the actual page index (1‑based).
            TextStamp stamp = new TextStamp("DOC{pageNumber}-2026");
            // Position the stamp at the bottom center of the page
            stamp.HorizontalAlignment = HorizontalAlignment.Center;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;
            // Add a small bottom margin so the text is not clipped
            stamp.BottomMargin = 10; // use BottomMargin instead of non‑existent Margin property

            // Define visual appearance (TextState is read‑only, modify its members directly)
            stamp.TextState.Font = FontRepository.FindFont("Arial");
            stamp.TextState.FontSize = 12;
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates numbering added. Output saved to '{outputPath}'.");
    }
}
