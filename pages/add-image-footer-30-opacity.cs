using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "footer.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a footer artifact and set the image
                FooterArtifact footer = new FooterArtifact();
                footer.SetImage(imagePath);

                // Set opacity to 30%
                footer.Opacity = 0.3;

                // Align the footer centered at the bottom of the page
                footer.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                footer.ArtifactVerticalAlignment   = VerticalAlignment.Bottom;

                // Add the artifact to the page
                page.Artifacts.Add(footer);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPath}'.");
    }
}