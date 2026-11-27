using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "brochure2022.pdf";
        const string outputXfdf = "page4Highlight.xfdf";
        const int pageNumber = 4;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
            {
                // Bind the PDF document
                editor.BindPdf(inputPdf);

                // Export only Highlight annotations from the specified page
                string[] annotationTypes = new[] { "Highlight" };
                using (FileStream xfdfStream = File.Create(outputXfdf))
                {
                    // startPage and endPage are both the page we are interested in
                    editor.ExportAnnotationsXfdf(xfdfStream, pageNumber, pageNumber, annotationTypes);
                }
            }

            Console.WriteLine($"Highlight annotations from page {pageNumber} exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
