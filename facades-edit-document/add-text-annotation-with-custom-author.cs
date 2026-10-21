using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";
        const string author = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Get the first page (or any page you need)
            Page page = pdfDoc.Pages[1];

            // Define the annotation rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation and set custom author metadata
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title = author,               // author metadata
                Contents = "This is a note added via Aspose.Pdf.",
                Open = true,
                Icon = TextIcon.Note
            };

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the updated PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added with author '{author}' and saved to '{outputPath}'.");
    }
}
