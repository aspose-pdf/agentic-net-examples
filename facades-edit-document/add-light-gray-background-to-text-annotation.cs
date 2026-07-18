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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the annotation editor (facade)
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document
            Document doc = editor.Document;

            // Select the page where the annotation will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation (sticky note) with a light gray background
            TextAnnotation textAnn = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "This is a note with a light gray background.",
                Open     = true,
                // The Color property sets the annotation's background color
                Color    = Aspose.Pdf.Color.LightGray
            };

            // Add the annotation to the page
            page.Annotations.Add(textAnn);

            // Save the modified PDF via the facade
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with light gray text annotation: {outputPath}");
    }
}