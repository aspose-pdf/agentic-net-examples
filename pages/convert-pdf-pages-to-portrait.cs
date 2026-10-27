using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Text;                // Required for text-related types (if needed)

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

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based (rule: page-indexing-one-based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get current MediaBox dimensions
                Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                double width  = mediaBox.URX - mediaBox.LLX;
                double height = mediaBox.URY - mediaBox.LLY;

                // If the page is landscape (width > height), swap dimensions to make it portrait
                if (width > height)
                {
                    // Create a new rectangle with swapped width/height while keeping the lower‑left corner unchanged
                    double newURX = mediaBox.LLX + height; // new width = old height
                    double newURY = mediaBox.LLY + width;  // new height = old width
                    page.MediaBox = new Aspose.Pdf.Rectangle(mediaBox.LLX, mediaBox.LLY, newURX, newURY);
                }
                // If already portrait, no change needed
            }

            // Save the modified document (rule: document-disposal-with-using ensures target stays alive until Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All pages converted to portrait orientation and saved to '{outputPath}'.");
    }
}