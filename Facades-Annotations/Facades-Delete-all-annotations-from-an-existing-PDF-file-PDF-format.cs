using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdfPath}");
            return;
        }

        // Use PdfAnnotationEditor to work with annotations
        using (PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            annotationEditor.BindPdf(inputPdfPath);

            // Delete all annotations in the document
            annotationEditor.DeleteAnnotations();

            // Save the modified document using the Document.Save rule
            annotationEditor.Document.Save(outputPdfPath);
        }

        Console.WriteLine($"All annotations have been removed. Saved to '{outputPdfPath}'.");
    }
}