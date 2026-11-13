using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only odd‑numbered pages
                if (i % 2 == 1)
                {
                    // Current MediaBox
                    Aspose.Pdf.Rectangle media = doc.Pages[i].MediaBox;

                    // Width and height of the page
                    double width = media.URX - media.LLX;
                    double height = media.URY - media.LLY;

                    // Swap dimensions to convert to portrait
                    // Keep lower‑left corner unchanged
                    Aspose.Pdf.Rectangle newMedia = new Aspose.Pdf.Rectangle(
                        media.LLX,
                        media.LLY,
                        media.LLX + height,
                        media.LLY + width);

                    doc.Pages[i].MediaBox = newMedia;
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd pages reoriented and saved to '{outputPath}'.");
    }
}