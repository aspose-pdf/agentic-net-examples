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

        // Open the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least five pages
            if (doc.Pages.Count < 5)
            {
                Console.Error.WriteLine("The document does not contain page 5.");
                return;
            }

            // Define the annotation rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle annotRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create a square annotation on page 5
            SquareAnnotation square = new SquareAnnotation(doc.Pages[5], annotRect)
            {
                // Red border
                Color = Aspose.Pdf.Color.Red,
                // Fill color (e.g., light gray)
                InteriorColor = Aspose.Pdf.Color.LightGray,
                // Semi‑transparent fill (0.0 = fully transparent, 1.0 = opaque)
                Opacity = 0.5
            };

            // Set border width (optional)
            square.Border = new Border(square) { Width = 2 };

            // Add the annotation to the page's annotation collection
            doc.Pages[5].Annotations.Add(square);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Square annotation added and saved to '{outputPath}'.");
    }
}