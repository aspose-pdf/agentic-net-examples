using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf  = "input.pdf";
        const string xfdfFile  = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(xfdfFile))
        {
            Console.Error.WriteLine($"XFDF file not found: {xfdfFile}");
            return;
        }

        // Use PdfAnnotationEditor (facade) to bind the PDF, import XFDF annotations, and save.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the target PDF document.
            editor.BindPdf(inputPdf);

            // Import all annotations from the XFDF file.
            editor.ImportAnnotationsFromXfdf(xfdfFile);

            // Save the resulting PDF with imported annotations.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}