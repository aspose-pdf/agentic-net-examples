using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare text state for the watermark
            TextState ts = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Aspose.Pdf.Color.Gray
            };

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a watermark artifact
                WatermarkArtifact wm = new WatermarkArtifact
                {
                    IsBackground = true,          // place behind existing content
                    Opacity = 0.5,                // semi‑transparent
                };

                // Set the watermark text and its visual style
                wm.SetTextAndState("CONFIDENTIAL", ts);

                // Add the artifact to the page
                page.Artifacts.Add(wm);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}