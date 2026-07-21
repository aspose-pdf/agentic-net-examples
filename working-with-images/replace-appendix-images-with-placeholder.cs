using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output.pdf";
        const string placeholderPath = "placeholder.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Detect if this page belongs to the appendix by searching for the word "Appendix"
                TextAbsorber pageAbsorber = new TextAbsorber();
                pageAbsorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
                page.Accept(pageAbsorber);

                if (pageAbsorber.Text != null &&
                    pageAbsorber.Text.IndexOf("Appendix", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Replace every image on this page with the placeholder image
                    var images = page.Resources.Images; // XImageCollection
                    for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                    {
                        // Open the placeholder stream for each replacement (required by Replace overload)
                        using (FileStream placeholderStream = File.OpenRead(placeholderPath))
                        {
                            // XImageCollection.Replace replaces the image at the given 1‑based index
                            images.Replace(imgIdx, placeholderStream);
                        }
                    }
                }
            }

            // Save the modified document (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}