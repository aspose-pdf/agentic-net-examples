using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string logoPath = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Store indices of WatermarkAnnotation objects to remove after iteration
                List<int> indicesToRemove = new List<int>();

                // Annotations collection uses 1‑based indexing
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];

                    // Identify WatermarkAnnotation instances
                    if (ann is Aspose.Pdf.Annotations.WatermarkAnnotation wmAnn)
                    {
                        // Preserve the original opacity
                        double originalOpacity = wmAnn.Opacity;

                        // Record the index for later removal
                        indicesToRemove.Add(i);

                        // Create a new WatermarkArtifact with the corporate logo
                        Aspose.Pdf.WatermarkArtifact artifact = new Aspose.Pdf.WatermarkArtifact();
                        artifact.SetImage(logoPath);
                        artifact.Opacity = originalOpacity;

                        // Position the artifact using the same rectangle as the original annotation
                        Aspose.Pdf.Rectangle rect = wmAnn.Rect;
                        // Position expects a Point (lower‑left corner of the rectangle)
                        artifact.Position = new Aspose.Pdf.Point(rect.LLX, rect.LLY);

                        // Add the artifact to the page
                        page.Artifacts.Add(artifact);
                    }
                }

                // Remove the original WatermarkAnnotations in reverse order to keep indices valid
                for (int idx = indicesToRemove.Count - 1; idx >= 0; idx--)
                {
                    page.Annotations.Delete(indicesToRemove[idx]);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"All WatermarkAnnotations replaced with logo. Saved to '{outputPath}'.");
    }
}