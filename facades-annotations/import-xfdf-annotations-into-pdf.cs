using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string xfdfFile = "annotations.xfdf";
        const string outputPdf = "output.pdf";

        // Verify that source files exist
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

        // Use PdfAnnotationEditor (Facade) to bind the PDF, import XFDF annotations, and save
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPdf);                     // Load the PDF document
            editor.ImportAnnotationsFromXfdf(xfdfFile);   // Import all annotations from XFDF
            editor.Save(outputPdf);                       // Save the updated PDF
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}