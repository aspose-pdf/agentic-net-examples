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
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfPath}");
            return;
        }

        try
        {
            // PdfAnnotationEditor does not implement IDisposable, so no using block is needed.
            var editor = new PdfAnnotationEditor();

            // Bind the existing PDF document.
            editor.BindPdf(pdfPath);

            // Import annotations from the XFDF file. The method expects a string array.
            editor.ImportAnnotations(new[] { xfdfPath });

            // Save the PDF with imported annotations.
            editor.Save(outputPath);

            Console.WriteLine($"Annotations imported successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
