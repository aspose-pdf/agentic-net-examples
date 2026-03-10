using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        // Load the CGM file and convert it to a PDF document
        using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Define the rectangle for the caret annotation (coordinates are in points)
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle caretRect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the caret annotation on the first page
            CaretAnnotation caret = new CaretAnnotation(doc.Pages[1], caretRect)
            {
                // Optional properties
                Color = Aspose.Pdf.Color.Red,          // annotation border color
                Contents = "Caret annotation example", // popup text
                Symbol = CaretSymbol.Paragraph        // associate a paragraph symbol
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(caret);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Caret annotation added and PDF saved to '{outputPdfPath}'.");
    }
}