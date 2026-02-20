using System;
using System.IO;
using Aspose.Pdf;                 // Document, Color, Rectangle
using Aspose.Pdf.Annotations;    // TextAnnotation, Border, BorderStyle
using Aspose.Pdf.Facades;        // PdfFileInfo

class Program
{
    static void Main(string[] args)
    {
        // Paths for input and output PDF files
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input PDF not found at '{inputPdfPath}'.");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Retrieve PDF metadata using the Facades PdfFileInfo class
            // -----------------------------------------------------------------
            var pdfInfo = new PdfFileInfo(inputPdfPath);
            Console.WriteLine("PDF Metadata:");
            Console.WriteLine($"  Title          : {pdfInfo.Title}");
            Console.WriteLine($"  Author         : {pdfInfo.Author}");
            Console.WriteLine($"  Subject        : {pdfInfo.Subject}");
            Console.WriteLine($"  Keywords       : {pdfInfo.Keywords}");
            Console.WriteLine($"  Creator        : {pdfInfo.Creator}");
            Console.WriteLine($"  Producer       : {pdfInfo.Producer}");
            Console.WriteLine($"  CreationDate   : {pdfInfo.CreationDate}");
            Console.WriteLine($"  ModDate        : {pdfInfo.ModDate}");
            Console.WriteLine($"  Number of pages: {pdfInfo.NumberOfPages}");
            Console.WriteLine($"  IsEncrypted    : {pdfInfo.IsEncrypted}");
            Console.WriteLine();

            // -----------------------------------------------------------------
            // Load the PDF document, add a text annotation, and save it
            // -----------------------------------------------------------------
            var document = new Document(inputPdfPath);

            // Define the rectangle (in points) where the annotation will appear
            // llx, lly, urx, ury
            var annotationRect = new Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation on the first page
            var textAnnotation = new TextAnnotation(document.Pages[1], annotationRect)
            {
                Title = "Sample Title",                     // Annotation title
                Contents = "This is a text annotation.",    // Popup text
                Color = Color.Blue                          // Annotation border color
            };

            // Initialize the border after the annotation object is created
            textAnnotation.Border = new Border(textAnnotation)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page's annotation collection
            document.Pages[1].Annotations.Add(textAnnotation);

            // Save the modified PDF using the standard Save method
            document.Save(outputPdfPath);

            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
