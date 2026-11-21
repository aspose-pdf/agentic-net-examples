using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "portrait_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // MediaBox defines the page size: lower‑left (llx,lly) and upper‑right (urx,ury)
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;

                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // If the page is landscape (width > height), swap dimensions to make it portrait
                if (width > height)
                {
                    // New MediaBox with height as the new width and width as the new height
                    double newWidth  = height;
                    double newHeight = width;

                    // Preserve the lower‑left corner position
                    double llx = mediaBox.LLX;
                    double lly = mediaBox.LLY;

                    // Set the adjusted MediaBox
                    page.MediaBox = new Aspose.Pdf.Rectangle(
                        llx,
                        lly,
                        llx + newWidth,
                        lly + newHeight);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation and saved to '{outputPath}'.");
    }
}