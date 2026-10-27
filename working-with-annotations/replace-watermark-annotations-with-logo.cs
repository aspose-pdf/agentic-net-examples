using System;
using System.IO;
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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
                // Iterate annotations backwards to safely remove items
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = page.Annotations[i];
                    if (ann is WatermarkAnnotation watermark)
                    {
                        // Preserve the original opacity
                        double opacity = watermark.Opacity;

                        // Remove the existing watermark annotation
                        page.Annotations.Delete(i);

                        // Create a new image stamp with the corporate logo
                        ImageStamp logoStamp = new ImageStamp(logoPath)
                        {
                            Opacity = opacity,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };

                        // Add the logo stamp to the page
                        page.AddStamp(logoStamp);
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All WatermarkAnnotations replaced with logo. Saved to '{outputPath}'.");
    }
}