using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "output.fdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document using the core Aspose.Pdf API
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // The core API does not provide ExportFdf (which is a Facades feature).
            // Instead, export annotations (including form fields) to XFDF using ExportAnnotationsToXfdf.
            // This produces an XFDF file, which is the closest equivalent available without Facades.
            using (FileStream fdfStream = new FileStream(outputFdfPath, FileMode.Create, FileAccess.Write))
            {
                pdfDoc.ExportAnnotationsToXfdf(fdfStream);
            }
        }

        Console.WriteLine($"Annotations exported to '{outputFdfPath}'.");
    }
}