using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Create a BackgroundArtifact.
            // The BackgroundArtifact class allows setting a background for a page.
            // ------------------------------------------------------------
            BackgroundArtifact background = new BackgroundArtifact();

            // Place the artifact behind page contents
            background.IsBackground = true;

            // Set a solid background color as a placeholder.
            // Aspose.Pdf does not expose a direct gradient fill on BackgroundArtifact.
            // To achieve a gradient, you could render a PDF page that contains a gradient
            // (e.g., using a Graph with a rectangle and a shading pattern) and then
            // assign that page via background.SetPdfPage(pageWithGradient).
            // Here we simply set a solid color for demonstration.
            background.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add the artifact to the page's artifact collection
            page.Artifacts.Add(background);

            // Save the PDF document
            doc.Save("gradient_background.pdf");
        }

        Console.WriteLine("PDF with BackgroundArtifact created successfully.");
    }
}