using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input and output PDFs
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Read PDF metadata using the PdfFileInfo facade
        // -----------------------------------------------------------------
        var pdfInfo = new PdfFileInfo(inputPdfPath);
        Console.WriteLine("PDF Metadata:");
        Console.WriteLine($" Title          : {pdfInfo.Title}");
        Console.WriteLine($" Author         : {pdfInfo.Author}");
        Console.WriteLine($" Subject        : {pdfInfo.Subject}");
        Console.WriteLine($" Keywords       : {pdfInfo.Keywords}");
        Console.WriteLine($" Creator        : {pdfInfo.Creator}");
        Console.WriteLine($" Producer       : {pdfInfo.Producer}");
        Console.WriteLine($" CreationDate   : {pdfInfo.CreationDate}");
        Console.WriteLine($" ModDate        : {pdfInfo.ModDate}");
        Console.WriteLine($" Number of pages: {pdfInfo.NumberOfPages}");

        // -----------------------------------------------------------------
        // 2. Add a text annotation using PdfAnnotationEditor facade
        // -----------------------------------------------------------------
        using (var annotationEditor = new PdfAnnotationEditor())
        {
            // Bind the existing PDF file to the editor
            annotationEditor.BindPdf(inputPdfPath);

            // Access the underlying Document object
            Document doc = annotationEditor.Document;

            // Work with the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the annotation will appear
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the TextAnnotation, set title and contents
            var textAnnotation = new TextAnnotation(page, rect)
            {
                Title = "Sample Title",
                Contents = "This annotation was added via Aspose.Pdf.Facades."
            };

            // Optional appearance settings
            textAnnotation.Border = new Border(textAnnotation)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };
            textAnnotation.Color = Aspose.Pdf.Color.Yellow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnnotation);

            // Save the modified PDF using the facade's Save method
            annotationEditor.Save(outputPdfPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}