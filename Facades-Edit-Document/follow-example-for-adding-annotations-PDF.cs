using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output.pdf";

        // Verify source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Add annotations using the PdfAnnotationEditor facade
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document
            editor.BindPdf(pdfPath);

            // Import annotations from the FDF file
            editor.ImportAnnotationsFromFdf(fdfPath);

            // Save the updated PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}