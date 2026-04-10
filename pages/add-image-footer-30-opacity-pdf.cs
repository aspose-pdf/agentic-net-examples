using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for alignment enums

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string footerImg = "footer.png"; // image to use as footer

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
            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a footer artifact
                FooterArtifact footer = new FooterArtifact();

                // Set the image that will appear in the footer
                footer.SetImage(footerImg);

                // Set opacity to 30% (0.3)
                footer.Opacity = 0.3;

                // Align the footer to the bottom‑center of the page
                footer.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                footer.ArtifactVerticalAlignment   = VerticalAlignment.Bottom;

                // Add the artifact to the page
                page.Artifacts.Add(footer);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPdf}'.");
    }
}