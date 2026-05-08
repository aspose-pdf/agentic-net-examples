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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only odd‑numbered pages
                if (i % 2 == 1)
                {
                    Page page = doc.Pages[i];

                    // Determine if the page is currently landscape by comparing width and height
                    double width  = page.MediaBox.URX - page.MediaBox.LLX;
                    double height = page.MediaBox.URY - page.MediaBox.LLY;

                    if (width > height) // landscape
                    {
                        double llx = page.MediaBox.LLX;
                        double lly = page.MediaBox.LLY;

                        // Create a new rectangle with swapped dimensions (portrait)
                        Aspose.Pdf.Rectangle portraitBox = new Aspose.Pdf.Rectangle(
                            llx,
                            lly,
                            llx + height, // new upper‑right X (previous height)
                            lly + width   // new upper‑right Y (previous width)
                        );

                        page.MediaBox = portraitBox;
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
