using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (using rule for document disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over odd‑numbered pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i += 2)
            {
                Page page = doc.Pages[i];

                // Get current MediaBox
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // If the page is landscape (width > height), swap dimensions
                if (width > height)
                {
                    // Create a new rectangle with swapped width/height
                    Aspose.Pdf.Rectangle newBox = new Aspose.Pdf.Rectangle(
                        mediaBox.LLX,               // same lower‑left X
                        mediaBox.LLY,               // same lower‑left Y
                        mediaBox.LLX + height,      // new upper‑right X (old height)
                        mediaBox.LLY + width);      // new upper‑right Y (old width)

                    // Apply the new MediaBox
                    page.MediaBox = newBox;
                }
            }

            // Save the modified document (using rule for saving)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page orientation adjusted and saved to '{outputPath}'.");
    }
}