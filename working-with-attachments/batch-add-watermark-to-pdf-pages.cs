using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_watermarked.pdf";
        const string watermarkText = "CONFIDENTIAL";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block to ensure proper disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing).
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // OPTIONAL: Detect pages that contain file‑attachment annotations.
                // If you only want to watermark pages that actually have attachments,
                // uncomment the block below.
                /*
                bool hasAttachment = false;
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is FileAttachmentAnnotation)
                    {
                        hasAttachment = true;
                        break;
                    }
                }
                if (!hasAttachment) continue; // skip pages without attachments
                */

                // Create a text watermark artifact.
                // WatermarkArtifact is part of the core Aspose.Pdf API.
                WatermarkArtifact watermark = new WatermarkArtifact();

                // Set the watermark text and its visual style.
                // The TextState defines font, size and color.
                watermark.SetTextAndState(
                    watermarkText,
                    new TextState
                    {
                        FontSize = 72,
                        Font = FontRepository.FindFont("Helvetica"),
                        ForegroundColor = Color.Red,
                        FontStyle = FontStyles.Bold
                    });

                // Position the watermark in the centre of the page.
                watermark.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                watermark.ArtifactVerticalAlignment   = VerticalAlignment.Center;

                // Make the watermark appear behind the page content.
                watermark.IsBackground = true;

                // Add the artifact to the current page.
                page.Artifacts.Add(watermark);
            }

            // Save the modified document.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdf}'.");
    }
}