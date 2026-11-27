using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "guide.pdf";
        const string xfdfPath = "links.xfdf";
        const int pageNumber = 2;

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Bind the PDF and export link annotations from the specified page to XFDF
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(pdfPath);

            // Export only Link annotations from pageNumber
            string[] annotationTypes = new[] { "Link" };
            using (FileStream xfdfStream = File.Create(xfdfPath))
            {
                editor.ExportAnnotationsXfdf(xfdfStream, pageNumber, pageNumber, annotationTypes);
            }
        }

        Console.WriteLine($"Link annotations from page {pageNumber} exported to '{xfdfPath}'.");
    }
}
