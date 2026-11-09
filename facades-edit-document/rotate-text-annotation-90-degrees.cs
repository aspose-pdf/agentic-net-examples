using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_annotation.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Define the rectangle for the annotation (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

        // Get the first page (pages are 1‑based)
        Page page = pdfDocument.Pages[1];

        // Create a TextAnnotation, set its properties and rotate it 90 degrees
        TextAnnotation txtAnnot = new TextAnnotation(page, rect)
        {
            Title    = "Diagonal Note",
            Contents = "This annotation is rotated to match diagonal content."
        };
        // Rotation is set via the Characteristics property using the Rotation enum
        txtAnnot.Characteristics.Rotate = Aspose.Pdf.Rotation.on90;

        // Add the annotation to the page
        page.Annotations.Add(txtAnnot);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"PDF saved with rotated annotation to '{outputPath}'.");
    }
}
