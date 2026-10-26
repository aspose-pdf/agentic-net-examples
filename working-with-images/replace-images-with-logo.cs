using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logoPath = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (Aspose.Pdf pages are 1‑based)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Replace every image on the page with the new logo.
                // The page's Paragraphs collection contains Image objects that represent
                // raster images placed on the page.
                for (int i = 0; i < page.Paragraphs.Count; i++)
                {
                    if (page.Paragraphs[i] is Image img)
                    {
                        // Preserve the original dimensions (FixWidth / FixHeight) if they are set.
                        // If they are not set, the image will be rendered using its intrinsic size.
                        double? originalWidth = img.FixWidth;
                        double? originalHeight = img.FixHeight;

                        // Load the logo image stream.
                        using (FileStream logoStream = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                        {
                            img.ImageStream = logoStream;
                        }

                        // Re‑apply the original dimensions (if any) so the logo occupies the same space.
                        if (originalWidth.HasValue) img.FixWidth = originalWidth.Value;
                        if (originalHeight.HasValue) img.FixHeight = originalHeight.Value;
                    }
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Images replaced and saved to '{outputPdf}'.");
    }
}
