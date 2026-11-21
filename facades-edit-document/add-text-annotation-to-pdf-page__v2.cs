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
        const string title = "Note";
        const string contents = "Custom annotation content";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least one page
        if (doc.Pages.Count < 1)
        {
            Console.Error.WriteLine("PDF does not contain any pages.");
            return;
        }

        // Define the annotation rectangle (llx, lly, urx, ury)
        var rect = new Rectangle(100, 200, 200, 250);

        // Create a text annotation and set its properties
        var textAnnotation = new TextAnnotation(doc.Pages[1], rect)
        {
            Title = title,
            Contents = contents,
            Open = true,               // open the annotation when the PDF is opened
            Icon = TextIcon.Note      // use the standard note icon
        };

        // Add the annotation to the first page
        doc.Pages[1].Annotations.Add(textAnnotation);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Text annotation added and saved to '{outputPath}'.");
    }
}
