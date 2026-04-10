using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output.pdf";         // result PDF
        const string logoPath  = "corporate_logo.png"; // corporate logo image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate annotations in reverse order so that removal does not affect the loop
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Process only WatermarkAnnotation objects
                    if (ann is WatermarkAnnotation watermarkAnn)
                    {
                        // Preserve the original opacity
                        double originalOpacity = watermarkAnn.Opacity;

                        // Preserve the original rectangle (position and size)
                        Aspose.Pdf.Rectangle rect = watermarkAnn.Rect;

                        // Remove the existing WatermarkAnnotation
                        page.Annotations.Delete(annIndex);

                        // Create a new WatermarkArtifact (image based watermark)
                        WatermarkArtifact artifact = new WatermarkArtifact();

                        // Set the corporate logo image
                        artifact.SetImage(logoPath);

                        // Apply the preserved opacity
                        artifact.Opacity = originalOpacity;

                        // Position the artifact at the same lower‑left corner as the original annotation
                        // (Aspose.Pdf.Point uses X = left, Y = bottom)
                        artifact.Position = new Aspose.Pdf.Point(rect.LLX, rect.LLY);

                        // Add the artifact to the page
                        page.Artifacts.Add(artifact);
                    }
                }
            }

            // Save the modified document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermark annotations replaced and saved to '{outputPdf}'.");
    }
}