using System;
using System.IO;
using Aspose.Pdf;

class ExportAnnotations
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Verify the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Determine the XFDF file name (same folder, same base name, .xfdf extension)
        string xfdfPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + ".xfdf");

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Export all annotations in the document to the XFDF file
            // The API ExportAnnotationsToXfdf writes the XFDF file directly
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"Annotations exported to: {xfdfPath}");
    }
}