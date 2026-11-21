using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoImagePath = "corporate_logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (Aspose.Pdf requirement)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Iterate annotations in reverse order so that removal does not affect the loop
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation ann = page.Annotations[annIndex];

                    // Process only WatermarkAnnotations
                    if (ann is WatermarkAnnotation oldWatermark)
                    {
                        // Preserve the original opacity and rectangle
                        double originalOpacity = oldWatermark.Opacity; // Opacity is double in Aspose.Pdf
                        Aspose.Pdf.Rectangle rect = oldWatermark.Rect;

                        // Remove the old watermark annotation
                        page.Annotations.Delete(annIndex);

                        // Create an ImageStamp that uses the corporate logo
                        ImageStamp logoStamp = new ImageStamp(logoImagePath)
                        {
                            Opacity = originalOpacity,
                            XIndent = rect.LLX,
                            YIndent = rect.LLY,
                            Width = rect.URX - rect.LLX,
                            Height = rect.URY - rect.LLY
                        };

                        // Add the stamp to the same page
                        page.AddStamp(logoStamp);
                    }
                }
            }

            // Save the modified PDF (PDF format is the default)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"All WatermarkAnnotations replaced. Output saved to '{outputPdfPath}'.");
    }
}
