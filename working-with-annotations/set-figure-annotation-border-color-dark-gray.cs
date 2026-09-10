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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages
            foreach (Page page in doc.Pages)
            {
                // Iterate through all annotations on the page
                foreach (Annotation ann in page.Annotations)
                {
                    // Check if the annotation is a figure annotation (Square, Circle, Polygon, Polyline, etc.)
                    if (ann is CommonFigureAnnotation)
                    {
                        // Set the border color to dark gray using the Characteristics.Border property
                        // The Border property expects a System.Drawing.Color, so we use the fully qualified type.
                        ann.Characteristics.Border = System.Drawing.Color.DarkGray;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"All figure annotation borders updated and saved to '{outputPath}'.");
    }
}
