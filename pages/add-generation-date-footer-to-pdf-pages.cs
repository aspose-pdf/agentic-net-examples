using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for HorizontalAlignment enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a footer artifact for the current page
                FooterArtifact footer = new FooterArtifact();

                // Set the footer text to the current generation date
                footer.Text = DateTime.Now.ToString("yyyy-MM-dd");

                // Center the footer horizontally and place it near the bottom
                footer.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                footer.BottomMargin = 20; // optional margin from the bottom edge

                // Add the footer artifact to the page
                page.Artifacts.Add(footer);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with date footer: {outputPath}");
    }
}