using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // For TextState if needed (optional)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampText  = "Confidential";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextStamp with the desired text
            TextStamp textStamp = new TextStamp(stampText)
            {
                // Align to the bottom‑left corner
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // 10‑point margins from left and bottom edges
                LeftMargin   = 10,
                BottomMargin = 10
            };

            // Optional styling (font size, color) – uses Aspose.Pdf.Color (cross‑platform)
            textStamp.TextState.FontSize        = 12;
            textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Pages are 1‑based; apply the stamp to each page individually
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                doc.Pages[i].AddStamp(textStamp);
            }

            // Save the modified PDF (still inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}