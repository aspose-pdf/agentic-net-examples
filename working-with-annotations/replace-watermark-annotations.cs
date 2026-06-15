using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

namespace ReplaceWatermarkAnnotationsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // Step 1: Create a sample PDF with a WatermarkAnnotation (self‑contained)
            // -----------------------------------------------------------------
            using (Document sampleDoc = new Document())
            {
                // Add a single page
                Page samplePage = sampleDoc.Pages.Add();

                // Define a rectangle for the watermark annotation (use fully qualified type)
                Aspose.Pdf.Rectangle watermarkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

                // Create a WatermarkAnnotation and set its opacity
                WatermarkAnnotation watermarkAnn = new WatermarkAnnotation(samplePage, watermarkRect);
                watermarkAnn.Opacity = 0.5f;

                // Add the annotation to the page
                samplePage.Annotations.Add(watermarkAnn);

                // Save the sample PDF
                sampleDoc.Save("input.pdf");
            }

            // ---------------------------------------------------------------
            // Step 2: Prepare a dummy corporate logo image (1x1 pixel PNG)
            // ---------------------------------------------------------------
            string logoPath = "logo.png";
            string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK2cAAAAASUVORK5CYII=";
            byte[] logoBytes = Convert.FromBase64String(base64Png);
            File.WriteAllBytes(logoPath, logoBytes);

            // ---------------------------------------------------------------
            // Step 3: Open the PDF, replace each WatermarkAnnotation with a
            //         WatermarkArtifact that uses the new logo and keeps opacity
            // ---------------------------------------------------------------
            using (Document pdfDoc = new Document("input.pdf"))
            {
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Collect existing WatermarkAnnotations (cannot modify collection while iterating)
                    List<WatermarkAnnotation> watermarks = new List<WatermarkAnnotation>();
                    for (int annIndex = 1; annIndex <= page.Annotations.Count; annIndex++)
                    {
                        Annotation annotation = page.Annotations[annIndex];
                        if (annotation is WatermarkAnnotation)
                        {
                            watermarks.Add((WatermarkAnnotation)annotation);
                        }
                    }

                    // Replace each collected annotation
                    foreach (WatermarkAnnotation oldAnnotation in watermarks)
                    {
                        // Preserve opacity and rectangle
                        double oldOpacity = oldAnnotation.Opacity; // Opacity is a double in Aspose.Pdf
                        Aspose.Pdf.Rectangle oldRect = oldAnnotation.Rect;

                        // Remove the old annotation from the page
                        page.Annotations.Delete(oldAnnotation);

                        // Create a new WatermarkArtifact with the corporate logo
                        WatermarkArtifact artifact = new WatermarkArtifact();
                        artifact.SetImage(logoPath);
                        artifact.Opacity = oldOpacity;

                        // Position the artifact at the same lower‑left corner as the original annotation
                        artifact.Position = new Aspose.Pdf.Point(oldRect.LLX, oldRect.LLY);

                        // Add the artifact to the page
                        page.Artifacts.Add(artifact);
                    }
                }

                // Save the modified PDF
                pdfDoc.Save("output.pdf");
            }
        }
    }
}
