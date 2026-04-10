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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Check if the page is currently landscape
                if (page.PageInfo.IsLandscape)
                {
                    // Process only odd‑numbered pages
                    if (i % 2 == 1)
                    {
                        // Get current MediaBox
                        Aspose.Pdf.Rectangle mb = page.MediaBox;

                        // Calculate width and height
                        double width  = mb.URX - mb.LLX;
                        double height = mb.URY - mb.LLY;

                        // Swap width and height to make it portrait
                        Aspose.Pdf.Rectangle newMb = new Aspose.Pdf.Rectangle(
                            mb.LLX,               // lower‑left X stays the same
                            mb.LLY,               // lower‑left Y stays the same
                            mb.LLX + height,      // new upper‑right X = original height
                            mb.LLY + width);      // new upper‑right Y = original width

                        // Apply the new MediaBox
                        page.MediaBox = newMb;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page orientation adjusted and saved to '{outputPath}'.");
    }
}