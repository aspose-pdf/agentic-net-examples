using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Apply stamp only to odd‑numbered pages
                if (i % 2 == 1)
                {
                    // Create a textual stamp
                    Aspose.Pdf.TextStamp stamp = new Aspose.Pdf.TextStamp("CONFIDENTIAL");

                    // Set arbitrary rotation angle (30 degrees)
                    stamp.RotateAngle = 30;

                    // Position the stamp near the left margin
                    stamp.XIndent = 10; // distance from the left edge
                    // Vertically centre the stamp on the page
                    stamp.YIndent = doc.Pages[i].PageInfo.Height / 2;

                    // Configure text appearance using Aspose.Pdf.Color (cross‑platform)
                    stamp.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Helvetica");
                    stamp.TextState.FontSize = 24;
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;

                    // Add the stamp to the current page
                    doc.Pages[i].AddStamp(stamp);
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text stamp applied to odd pages. Saved as '{outputPath}'.");
    }
}