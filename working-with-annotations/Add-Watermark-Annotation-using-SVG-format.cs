using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string svgPath   = "watermark.svg";
        const string outputPdf = "output_watermarked.pdf";

        if (!File.Exists(inputPdf) || !File.Exists(svgPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a WatermarkArtifact and set the SVG image as its content
            WatermarkArtifact watermark = new WatermarkArtifact();
            watermark.SetImage(svgPath);                     // SVG file
            watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
            watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;
            watermark.Opacity = 0.5;                         // optional transparency

            // Add the watermark artifact to each page (or target specific pages)
            foreach (Page page in doc.Pages)
            {
                page.Artifacts.Add(watermark);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}