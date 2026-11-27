using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
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

        // Load and bind the PDF using PdfContentEditor (facade API)
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath); // binding performed; no IDisposable

        // Load the same document for modification
        using (Document doc = new Document(inputPath))
        {
            // Access page 1 (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a text annotation with title "Note"
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Title    = "Note";
            annotation.Contents = "This is a note annotation.";

            // Add the annotation to the page
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}