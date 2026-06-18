using System;
using System.IO;
using Aspose.Pdf;          // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output_with_footer.pdf";
        const string footerImg = "footer.png";     // image to use as footer

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(footerImg))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImg}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a FooterArtifact, set the image and opacity (30%)
                FooterArtifact footer = new FooterArtifact();
                footer.SetImage(footerImg);   // load image from file
                footer.Opacity = 0.3f;        // 30% opacity

                // Optional: adjust margins if needed (example: 10 points from bottom)
                footer.BottomMargin = 10;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(footer);
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with image footer: {outputPdf}");
    }
}