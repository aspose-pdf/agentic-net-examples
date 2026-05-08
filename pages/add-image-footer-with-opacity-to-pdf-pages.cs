using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Facades;            // Not required here but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string footerPath = "footer.png";   // Image to use as footer
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerPath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a FooterArtifact instance
                FooterArtifact footer = new FooterArtifact();

                // Set the image for the footer (can use a file path or a stream)
                footer.SetImage(footerPath);

                // 30 % opacity (range 0.0 … 1.0)
                footer.Opacity = 0.30;

                // Place the footer behind the page content (false = foreground)
                footer.IsBackground = false;

                // Center the footer horizontally; vertical position is controlled by BottomMargin
                footer.ArtifactHorizontalAlignment = HorizontalAlignment.Center;

                // Optional: adjust bottom margin if needed (0 = directly at the bottom)
                footer.BottomMargin = 0;

                // Add the artifact to the current page
                page.Artifacts.Add(footer);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with image footer: {outputPath}");
    }
}