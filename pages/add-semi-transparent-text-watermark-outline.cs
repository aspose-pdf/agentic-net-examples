using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for FontRepository and TextState

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a textual stamp
                TextStamp stamp = new TextStamp(watermarkText);

                // Configure text appearance (fill color)
                stamp.TextState.Font = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize = 48;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red; // fill color

                // Semi‑transparent fill
                stamp.Opacity = 0.3f; // 30 % opacity for the text

                // Outline (stroke) settings
                stamp.OutlineOpacity = 0.6f; // semi‑transparent outline
                stamp.OutlineWidth   = 1.5f; // outline thickness

                // Center the stamp on the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Center;

                // Apply the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}