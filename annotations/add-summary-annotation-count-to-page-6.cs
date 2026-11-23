using System;
using System.IO;
using System.Drawing;
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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that the document has at least six pages (1‑based indexing)
                if (doc.Pages.Count < 6)
                {
                    Console.Error.WriteLine("Document has fewer than 6 pages.");
                    return;
                }

                // Retrieve page six
                Page pageSix = doc.Pages[6];

                // Count existing annotations on page six
                int annotationCount = pageSix.Annotations.Count;

                // Define the rectangle that will contain the summary FreeTextAnnotation
                // (left, bottom, right, top)
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

                // Create a DefaultAppearance – this defines the font and the text colour
                DefaultAppearance appearance = new DefaultAppearance("Arial", 12f, System.Drawing.Color.Black);

                // Construct the FreeTextAnnotation with the required parameters
                FreeTextAnnotation summary = new FreeTextAnnotation(pageSix, rect, appearance)
                {
                    Contents = $"Page 6 contains {annotationCount} annotation(s).",
                    // Background colour of the annotation
                    Color = Aspose.Pdf.Color.LightYellow
                };

                // Add a simple border (Width expects an int)
                summary.Border = new Border(summary) { Width = 1 };

                // Add the annotation to the page
                pageSix.Annotations.Add(summary);

                // Save the modified document
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
