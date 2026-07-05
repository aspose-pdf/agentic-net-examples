using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file
        const string pdfPath = "input.pdf";

        // Ensure the PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Build the XFDF file path in the same folder as the PDF
        string directory = Path.GetDirectoryName(pdfPath);
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
        string xfdfPath = Path.Combine(directory, $"{fileNameWithoutExt}.xfdf");

        // Load the PDF document and export its annotations to XFDF
        using (Document doc = new Document(pdfPath))
        {
            // Exports all annotations in the document to the XFDF file
            doc.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"Annotations exported to: {xfdfPath}");
    }
}