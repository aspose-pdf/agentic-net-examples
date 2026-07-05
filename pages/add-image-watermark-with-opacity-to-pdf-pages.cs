using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string watermarkImgPath = "watermark.png";

        // Verify input files exist
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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Apply the watermark to each page
            foreach (Page page in doc.Pages)
            {
                // Create a watermark artifact
                WatermarkArtifact wm = new WatermarkArtifact();

                // Set the image for the watermark
                wm.SetImage(watermarkImgPath);

                // Set opacity to 20% (0.2)
                wm.Opacity = 0.2;

                // Place the watermark on top of page content
                wm.IsBackground = false;

                // Add the artifact to the page
                page.Artifacts.Add(wm);
            }

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}