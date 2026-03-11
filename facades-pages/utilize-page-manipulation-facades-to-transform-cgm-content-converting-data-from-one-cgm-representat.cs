using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source CGM file and the intermediate/final PDFs
        const string cgmPath = "input.cgm";
        const string intermediatePdf = "intermediate.pdf";
        const string finalPdf = "output.pdf";

        // Verify that the CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert the CGM file to a PDF using the PdfProducer facade.
        // PdfProducer.Produce(string inputFile, ImportFormat format, string outputFile)
        // -----------------------------------------------------------------
        PdfProducer.Produce(cgmPath, ImportFormat.Cgm, intermediatePdf);
        Console.WriteLine($"CGM converted to PDF: {intermediatePdf}");

        // -----------------------------------------------------------------
        // Step 2: Manipulate the resulting PDF pages using PdfFileEditor.
        // In this example we add uniform margins of 20 points to all pages.
        // PdfFileEditor.AddMargins(string inputFile, string outputFile,
        //                          int[] pages, double left, double bottom,
        //                          double right, double top)
        // Passing null for the pages array applies the operation to all pages.
        // -----------------------------------------------------------------
        PdfFileEditor editor = new PdfFileEditor();
        editor.AddMargins(intermediatePdf, finalPdf, null, 20, 20, 20, 20);
        Console.WriteLine($"Margins added, final PDF saved: {finalPdf}");
    }
}