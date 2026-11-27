using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string xfdfPath = "annotations.xfdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Create the annotation editor
            PdfAnnotationEditor annotEditor = new PdfAnnotationEditor();

            // Bind the PDF to the editor
            annotEditor.BindPdf(doc);

            // Import annotations from XFDF – the method expects a string[]
            annotEditor.ImportAnnotations(new[] { xfdfPath });

            // Save the PDF with the imported annotations
            annotEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with imported annotations to '{outputPath}'.");
    }
}
