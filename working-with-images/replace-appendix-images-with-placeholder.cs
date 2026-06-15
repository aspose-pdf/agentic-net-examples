using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "output_protected.pdf";
        const string placeholderImg = "placeholder.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(placeholderImg))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Find the first page that contains the word "Appendix"
            int appendixStartPage = -1;
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber();
                absorber.TextSearchOptions = new TextSearchOptions(true); // case‑insensitive
                doc.Pages[i].Accept(absorber);
                if (absorber.Text != null && absorber.Text.IndexOf("Appendix", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    appendixStartPage = i;
                    break;
                }
            }

            // If no appendix marker is found, assume no replacement is needed
            if (appendixStartPage == -1)
            {
                Console.WriteLine("No appendix section detected. Saving original document.");
                doc.Save(outputPath);
                return;
            }

            // Replace every image on each appendix page with the placeholder image
            using (FileStream placeholderStream = File.OpenRead(placeholderImg))
            {
                // Keep the placeholder stream open for the duration of replacements
                for (int pageNum = appendixStartPage; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];
                    // XImageCollection is 1‑based; get the count first
                    int imageCount = page.Resources.Images.Count;
                    for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                    {
                        // Replace the image at the current index with the placeholder
                        // The Replace method expects a fresh stream for each call, so we create a new MemoryStream copy
                        placeholderStream.Position = 0;
                        using (MemoryStream msCopy = new MemoryStream())
                        {
                            placeholderStream.CopyTo(msCopy);
                            msCopy.Position = 0;
                            page.Resources.Images.Replace(imgIdx, msCopy);
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Protected PDF saved to '{outputPath}'.");
        }
    }
}