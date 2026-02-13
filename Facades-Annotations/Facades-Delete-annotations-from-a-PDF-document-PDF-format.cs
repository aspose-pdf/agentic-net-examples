using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Bind the document to the annotation editor
        PdfAnnotationEditor annotationEditor = new PdfAnnotationEditor();
        annotationEditor.BindPdf(pdfDocument);

        // Delete all annotations from the document
        annotationEditor.DeleteAnnotations();

        // Save the updated PDF (uses the provided document-save rule)
        pdfDocument.Save(outputPath);

        Console.WriteLine($"All annotations removed. Saved to '{outputPath}'.");
    }
}