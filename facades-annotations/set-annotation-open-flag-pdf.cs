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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDoc = new Document(inputPath);

        // Bind the document to PdfAnnotationEditor (required by the task)
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(pdfDoc);

        // Page numbers are 1‑based in Aspose.Pdf
        int pageNumber = 4;          // target page (1‑based)
        int annotationIndex = 2;     // zero‑based index within the page's annotation collection

        // Validate page number
        if (pageNumber < 1 || pageNumber > editor.Document.Pages.Count)
        {
            Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
            return;
        }

        // Retrieve the target page
        Page page = editor.Document.Pages[pageNumber];
        AnnotationCollection annotations = page.Annotations;

        // Validate annotation index
        if (annotationIndex < 0 || annotationIndex >= annotations.Count)
        {
            Console.Error.WriteLine($"Annotation index {annotationIndex} not found on page {pageNumber}.");
            return;
        }

        // Retrieve the target annotation
        Annotation annotation = annotations[annotationIndex];

        // The Open flag exists only on specific markup annotation types (e.g., TextAnnotation, PopupAnnotation)
        bool openFlagSet = false;
        if (annotation is TextAnnotation textAnn)
        {
            textAnn.Open = true;
            openFlagSet = true;
        }
        else if (annotation is PopupAnnotation popupAnn)
        {
            popupAnn.Open = true;
            openFlagSet = true;
        }
        else
        {
            Console.Error.WriteLine("The selected annotation type does not support the Open property.");
            return;
        }

        // No explicit UpdateAnnotation call is required – the modification is made directly on the page's annotation collection.
        // Save the updated PDF via the editor
        editor.Save(outputPath);

        Console.WriteLine($"Annotation updated and saved to '{outputPath}'.");
    }
}
