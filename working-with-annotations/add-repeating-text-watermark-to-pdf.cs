using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "watermarked_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Prepare TextState – use property initializers to avoid constructor overload issues
            var textState = new TextState
            {
                Font = FontRepository.FindFont("Helvetica"),
                FontSize = 48,
                ForegroundColor = Color.LightGray
            };

            // Create a watermark artifact that will be placed behind page contents
            WatermarkArtifact watermark = new WatermarkArtifact
            {
                IsBackground = true,                                   // place behind content
                Opacity = 0.3,                                         // semi‑transparent
                ArtifactHorizontalAlignment = HorizontalAlignment.Center,
                ArtifactVerticalAlignment   = VerticalAlignment.Center,
                Text = "CONFIDENTIAL",                               // repeating text
                TextState = textState
            };

            // Add the same artifact to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPath}'.");
    }
}
