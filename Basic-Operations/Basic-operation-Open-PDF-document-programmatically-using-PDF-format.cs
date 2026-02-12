using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        Document pdfDocument = new Document(inputPath);

        // Define the rectangle (llx, lly, urx, ury) for the annotation on page 1
        // Fully qualify the Rectangle type to avoid ambiguity with Aspose.Pdf.Drawing.Rectangle
        Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

        // Create a TextAnnotation on the first page
        TextAnnotation textAnnot = new TextAnnotation(pdfDocument.Pages[1], rect);
        textAnnot.Title = "Sample Title";               // Title shown in the annotation popup
        textAnnot.Contents = "This is a text annotation."; // Main content of the annotation
        textAnnot.Icon = TextIcon.Comment;              // Use the correct TextIcon enum

        // Initialize the border for the annotation (border-initialization rule)
        textAnnot.Border = new Border(textAnnot)
        {
            Style = BorderStyle.Solid,
            Width = 1
        };

        // Add the annotation to the page's annotation collection
        pdfDocument.Pages[1].Annotations.Add(textAnnot);

        // Save the modified PDF (document-save rule)
        pdfDocument.Save(outputPath);
    }
}