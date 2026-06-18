using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;   // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages (pages are 1‑based)
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document contains fewer than 5 pages.");
                return;
            }

            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a square annotation on page 5
            SquareAnnotation square = new SquareAnnotation(doc.Pages[5], rect);

            // Set a red border
            square.Color = Aspose.Pdf.Color.Red;

            // Set a semi‑transparent fill color (light blue in this example)
            square.InteriorColor = Aspose.Pdf.Color.FromRgb(0.2, 0.6, 0.9);

            // Apply 50 % opacity to the whole annotation (border + fill)
            square.Opacity = 0.5;

            // Add the annotation to the page's annotation collection
            doc.Pages[5].Annotations.Add(square);

            // Save the modified PDF (save rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}