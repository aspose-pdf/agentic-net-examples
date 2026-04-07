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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages
                foreach (Page page in doc.Pages)
                {
                    // Annotations collection uses 1‑based indexing
                    for (int i = 1; i <= page.Annotations.Count; i++)
                    {
                        Annotation ann = page.Annotations[i];

                        // Identify figure annotations (square, circle, polygon, polyline)
                        if (ann is SquareAnnotation ||
                            ann is CircleAnnotation ||
                            ann is PolygonAnnotation ||
                            ann is PolylineAnnotation)
                        {
                            // Change the border color to dark gray
                            ann.Color = Aspose.Pdf.Color.DarkGray;
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All figure annotation borders set to dark gray. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}