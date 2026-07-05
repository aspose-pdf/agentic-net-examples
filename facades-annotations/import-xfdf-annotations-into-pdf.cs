using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string xfdfPath  = "annotations.xfdf";
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

        // Create the annotation editor facade
        PdfAnnotationEditor editor = new PdfAnnotationEditor();

        // Load the target PDF document
        editor.BindPdf(pdfPath);

        // Import all annotations from the XFDF file
        editor.ImportAnnotationsFromXfdf(xfdfPath);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"Annotations imported successfully. Saved to '{outputPath}'.");
    }
}