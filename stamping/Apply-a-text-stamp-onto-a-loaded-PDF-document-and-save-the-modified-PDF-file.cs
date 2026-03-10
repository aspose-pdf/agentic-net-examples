using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Required for TextState and FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampText  = "CONFIDENTIAL";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp(stampText);

            // Configure visual appearance of the stamp
            textStamp.HorizontalAlignment = HorizontalAlignment.Center;
            textStamp.VerticalAlignment   = VerticalAlignment.Center;
            textStamp.Opacity             = 0.5; // 50% transparent
            textStamp.TextState.FontSize  = 48;
            textStamp.TextState.Font      = FontRepository.FindFont("Helvetica");
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

            // Optionally set explicit position (XIndent/YIndent are measured from the bottom‑left corner)
            // textStamp.XIndent = 100;
            // textStamp.YIndent = 500;

            // Apply the stamp to every page (Aspose.Pdf uses 1‑based page indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(textStamp);
            }

            // Save the modified PDF (saving without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}