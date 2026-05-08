using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            using (var tempDoc = new Document())
            {
                // Add a blank page.
                tempDoc.Pages.Add();
                tempDoc.Save(inputPath);
            }
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define annotation rectangle (left, bottom, right, top)
            var rect = new Rectangle(100, 500, 300, 550);

            // Create a text annotation on the first page
            var annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",
                Contents = "Annotation with UTC creation date.",
                // Set the creation date to the current UTC time.
                CreationDate = DateTime.UtcNow
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(annotation);

            // Save the modified document using the Facades API
            var editor = new PdfAnnotationEditor();
            editor.BindPdf(doc);
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
