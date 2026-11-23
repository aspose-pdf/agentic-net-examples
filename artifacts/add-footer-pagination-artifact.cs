using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least six pages (1‑based indexing)
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The PDF contains fewer than 6 pages.");
                return;
            }

            // Retrieve page six
            Page page = doc.Pages[6];

            // Create a pagination footer artifact with descriptive text
            // FooterArtifact is a concrete pagination artifact whose subtype is Footer.
            FooterArtifact footer = new FooterArtifact
            {
                Text = $"Page {page.Number}"
            };

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(footer);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer artifact to '{outputPath}'.");
    }
}
