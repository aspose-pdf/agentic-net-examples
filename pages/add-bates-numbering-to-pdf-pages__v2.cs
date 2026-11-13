using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextState, FontStyles, Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "bates_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create the Bates number for the current page: "2026-####"
                string batesNumber = $"2026-{i:D4}";

                // Configure a TextStamp with the desired appearance
                TextStamp stamp = new TextStamp(batesNumber)
                {
                    // Position the stamp at the bottom‑right corner
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    BottomMargin        = 20,   // distance from bottom edge
                    RightMargin         = 20    // distance from right edge
                };

                // TextState is read‑only; modify its properties after the stamp is created
                stamp.TextState.FontSize = 12;
                stamp.TextState.FontStyle = FontStyles.Bold;
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bates‑numbered PDF saved to '{outputPath}'.");
    }
}
