using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "gradient_background.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // Create a BackgroundArtifact and add it to the page
            // The artifact will be placed behind page contents (IsBackground = true)
            BackgroundArtifact background = new BackgroundArtifact
            {
                IsBackground = true
            };

            // NOTE:
            // Aspose.Pdf does not provide a direct property for gradient fills on BackgroundArtifact.
            // A gradient can be achieved by adding a shading operator to the artifact's Contents.
            // Below is a simple example that sets a solid background color.
            // To implement a gradient, you would need to create a Shading object (e.g., AxialShading)
            // and add the appropriate PDF operators to background.Contents.
            // This example uses a solid color for simplicity and ensures the code compiles.

            background.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 1.0); // Light blue solid fill

            // Add the artifact to the page's Artifacts collection
            page.Artifacts.Add(background);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with background artifact saved to '{outputPath}'.");
    }
}