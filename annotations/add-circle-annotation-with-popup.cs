using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "CircleWithPopup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the circle annotation
            // (llx, lly, urx, ury) – lower‑left and upper‑right corners
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the circle annotation
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                // Light gray interior fill
                InteriorColor = Aspose.Pdf.Color.LightGray,
                // Semi‑transparent fill
                Opacity = 0.4,
                // Popup title (appears in the note window)
                Title = "Circle note",
                // Popup contents (text shown in the note window)
                Contents = "Circle note"
            };

            // Add the annotation to the page
            page.Annotations.Add(circle);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation with popup saved to '{outputPath}'.");
    }
}