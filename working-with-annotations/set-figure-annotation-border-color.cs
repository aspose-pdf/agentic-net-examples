using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the standard Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate through all annotations on the page
                for (int j = 1; j <= page.Annotations.Count; j++)
                {
                    Annotation ann = page.Annotations[j];

                    // Figure annotations are concrete subclasses of CommonFigureAnnotation:
                    // SquareAnnotation, CircleAnnotation, PolygonAnnotation, PolylineAnnotation
                    if (ann is SquareAnnotation ||
                        ann is CircleAnnotation ||
                        ann is PolygonAnnotation ||
                        ann is PolylineAnnotation)
                    {
                        // Set the border color via the Characteristics property
                        // Use System.Drawing.Color because Border expects a System.Drawing.Color value
                        ann.Characteristics.Border = System.Drawing.Color.DarkGray;
                    }
                }
            }

            // Save the modified PDF (using the standard Document.Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"All figure annotation borders set to dark gray. Saved to '{outputPath}'.");
    }
}
