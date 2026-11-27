using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "contract.pdf";
        const string xfdfPath = "contract.xfdf";
        const string outputPath = "MergedContract.pdf";

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

        // PdfAnnotationEditor does not implement IDisposable, so no using block is needed.
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Bind the source PDF document.
        editor.BindPdf(pdfPath);

        // Import annotations from the XFDF file.
        editor.ImportAnnotationFromXfdf(xfdfPath);

        // Save the PDF with merged annotations.
        editor.Save(outputPath);

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}