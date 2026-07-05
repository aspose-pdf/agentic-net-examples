using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the annotation editor and bind the PDF document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // The prototype annotation must be instantiated with a Page and a Rectangle.
            // We use the first page and a zero‑size rectangle because only the Modified
            // property is relevant for the ModifyAnnotations call.
            Page firstPage = editor.Document.Pages[1];
            Aspose.Pdf.Rectangle dummyRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextAnnotation prototype = new TextAnnotation(firstPage, dummyRect)
            {
                Modified = DateTime.Now // set to current system time
            };

            // Determine the page range (Aspose.Pdf uses 1‑based indexing)
            int startPage = 1;
            int endPage   = editor.Document.Pages.Count; // all pages

            // Apply the modification to all annotations of the specified type on the range
            editor.ModifyAnnotations(startPage, endPage, prototype);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations' Modified date updated and saved to '{outputPath}'.");
    }
}
