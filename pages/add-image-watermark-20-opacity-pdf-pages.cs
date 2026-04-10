using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string watermarkImgPath = "watermark.png";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact, set the image and opacity (20%)
                WatermarkArtifact artifact = new WatermarkArtifact();
                artifact.SetImage(watermarkImgPath);
                artifact.Opacity = 0.2; // 20% opacity for subtle branding

                // Add the artifact to the page
                page.Artifacts.Add(artifact);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}