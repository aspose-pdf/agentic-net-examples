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

        // Load the PDF document inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Process only odd‑numbered pages
                if (i % 2 == 1)
                {
                    Page page = doc.Pages[i];

                    // Determine current orientation via PageInfo.IsLandscape
                    if (page.PageInfo.IsLandscape)
                    {
                        // Retrieve current MediaBox dimensions
                        Aspose.Pdf.Rectangle mb = page.MediaBox;
                        double llx = mb.LLX;
                        double lly = mb.LLY;
                        double urx = mb.URX;
                        double ury = mb.URY;
                        double width  = urx - llx;
                        double height = ury - lly;

                        // Swap width and height to convert to portrait
                        double newUrx = llx + height;
                        double newUry = lly + width;

                        // Apply the new MediaBox
                        page.MediaBox = new Aspose.Pdf.Rectangle(llx, lly, newUrx, newUry);

                        // Update the PageInfo flag
                        page.PageInfo.IsLandscape = false;
                    }
                }
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Odd‑page orientation adjusted and saved to '{outputPath}'.");
    }
}