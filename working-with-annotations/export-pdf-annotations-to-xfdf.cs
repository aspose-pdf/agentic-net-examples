using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF path
        const string pdfPath = "input.pdf";

        // Verify the file exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Determine XFDF output path (same folder, same name with .xfdf extension)
        string xfdfPath = Path.ChangeExtension(pdfPath, ".xfdf");

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Export all annotations (including those on the current page) to XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"Annotations exported to: {xfdfPath}");
    }
}