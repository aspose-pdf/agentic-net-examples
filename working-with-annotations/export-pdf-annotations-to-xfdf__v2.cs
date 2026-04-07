using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing annotations
        const string inputPdfPath = "input.pdf";

        // Output XFDF file that will hold all annotation resources
        const string outputXfdfPath = "annotations.xfdf";

        // Verify the source file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: using ensures proper disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all annotations, including their appearance streams, to XFDF
            pdfDoc.ExportAnnotationsToXfdf(outputXfdfPath);
        }

        Console.WriteLine($"Annotations and appearance streams saved to '{outputXfdfPath}'.");
    }
}