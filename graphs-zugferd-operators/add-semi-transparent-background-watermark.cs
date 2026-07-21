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
            // Define the visual style of the watermark text
            TextState watermarkState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 72,
                ForegroundColor = Aspose.Pdf.Color.LightGray
            };

            // Apply the watermark to every page
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact
                WatermarkArtifact wm = new WatermarkArtifact();

                // Place it behind existing content
                wm.IsBackground = true;

                // Make it semi‑transparent
                wm.Opacity = 0.3; // 30 % opacity

                // Set the watermark text and its style
                wm.SetTextAndState("CONFIDENTIAL", watermarkState);

                // Center the watermark on the page using Aspose.Pdf.Point (correct type)
                wm.Position = new Aspose.Pdf.Point(page.PageInfo.Width / 2, page.PageInfo.Height / 2);

                // Add the artifact to the page
                page.Artifacts.Add(wm);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
