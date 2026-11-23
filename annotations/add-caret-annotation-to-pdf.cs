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
            // Verify that the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The document contains no pages.");
                return;
            }

            // Define the rectangle (in points) where the caret annotation will appear
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);

            // Create a CaretAnnotation on page 1
            CaretAnnotation caret = new CaretAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",                     // Title displayed in the annotation popup
                Color = Aspose.Pdf.Color.Red        // Red border color
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(caret);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Caret annotation added and saved to '{outputPath}'.");
    }
}