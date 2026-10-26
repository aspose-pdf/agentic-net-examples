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
        const string placeholderImg = "placeholder.png"; // confidential placeholder image

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
            int firstAppendixPage = -1;

            // Determine the first page that contains the word "Appendix"
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                TextAbsorber absorber = new TextAbsorber
                {
                    ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure)
                };
                doc.Pages[i].Accept(absorber);
                if (absorber.Text != null && absorber.Text.IndexOf("Appendix", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    firstAppendixPage = i;
                    break;
                }
            }

            if (firstAppendixPage == -1)
            {
                Console.WriteLine("No appendix section detected. No changes applied.");
                doc.Save(outputPath);
                return;
            }

            // Prepare a full‑page placeholder stamp
            ImageStamp placeholderStamp = new ImageStamp(placeholderImg)
            {
                // Place the stamp on top of existing content to hide images
                Background = false,
                // Stretch to cover the whole page
                Width      = doc.Pages[1].PageInfo.Width,
                Height     = doc.Pages[1].PageInfo.Height,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the placeholder to every page from the appendix start to the end
            for (int i = firstAppendixPage; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Optionally set alternative text for existing images (helps accessibility)
                foreach (XImage img in page.Resources.Images)
                {
                    img.TrySetAlternativeText("Image removed for confidentiality", page);
                }

                // Add the placeholder stamp (covers the page)
                page.AddStamp(placeholderStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Appendix images replaced with placeholder. Saved to '{outputPath}'.");
        }
    }
}