using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "locked_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text annotation on the first page
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "This annotation is locked.",
                // Set the Locked flag to prevent user modifications
                Flags    = AnnotationFlags.Locked
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(annotation);

            // Use PdfAnnotationEditor (Facades API) to bind the modified document and save it
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                editor.BindPdf(doc);               // Initialize the facade with the document
                editor.Save(outputPath);           // Save the result
                editor.Close();                    // Close the facade (optional, using will dispose)
            }
        }

        Console.WriteLine($"PDF saved with locked annotation to '{outputPath}'.");
    }
}