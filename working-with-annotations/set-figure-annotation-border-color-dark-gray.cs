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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Define dark gray color (RGB values are 0‑255 range)
                Aspose.Pdf.Color darkGray = Aspose.Pdf.Color.FromRgb(51, 51, 51); // approx 0.2 * 255

                // Iterate through all pages and their annotations
                foreach (Page page in doc.Pages)
                {
                    foreach (Annotation ann in page.Annotations)
                    {
                        // StampAnnotation is the Aspose.Pdf core type that represents a figure/image annotation.
                        if (ann is StampAnnotation stampAnn)
                        {
                            // Set the border colour – the Color property controls the border colour
                            stampAnn.Color = darkGray;

                            // Ensure a visible border width (default may be 0)
                            if (stampAnn.Border == null)
                                stampAnn.Border = new Border(stampAnn) { Width = 1 };
                            else
                                stampAnn.Border.Width = 1;
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All figure annotation borders updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
