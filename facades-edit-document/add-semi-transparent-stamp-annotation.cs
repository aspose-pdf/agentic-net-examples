using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade API (PdfFileStamp, Stamp)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // PDF to which the annotation will be added
        const string stampPdf  = "stamp.pdf";   // PDF page used as the visual stamp
        const string outputPdf = "output.pdf";

        // Verify source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampPdf))
        {
            Console.Error.WriteLine($"Stamp PDF not found: {stampPdf}");
            return;
        }

        // Initialize the facade that works with PDF files
        PdfFileStamp fileStamp = new PdfFileStamp();

        // Bind the target PDF document
        fileStamp.BindPdf(inputPdf);

        // Create a stamp object (annotation) and set its opacity to 0.5 (50% transparent)
        Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp
        {
            Opacity = 0.5f   // value range: 0.0 (fully transparent) to 1.0 (fully opaque)
        };

        // Bind a PDF page (or any PDF file) as the visual appearance of the stamp.
        // Here we bind the first page of 'stampPdf' as the stamp content.
        stamp.BindPdf(stampPdf, 1);

        // Add the stamp (annotation) to the document
        fileStamp.AddStamp(stamp);

        // Save the modified PDF
        fileStamp.Save(outputPdf);

        // Release resources
        fileStamp.Close();

        Console.WriteLine($"Annotation with 0.5 opacity added. Output saved to '{outputPdf}'.");
    }
}