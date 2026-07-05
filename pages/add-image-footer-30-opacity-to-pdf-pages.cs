using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a FooterArtifact instance
                FooterArtifact footer = new FooterArtifact();

                // Set the image that will appear in the footer
                footer.SetImage(footerImg);

                // Position the footer at the bottom centre of the page
                footer.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                footer.ArtifactVerticalAlignment   = VerticalAlignment.Bottom;

                // Set the desired opacity (30 % = 0.3)
                footer.Opacity = 0.3;

                // Add the artifact to the page's artifact collection
                page.Artifacts.Add(footer);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPdf}'.");
    }
}