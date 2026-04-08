using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Output XFDF file path
        const string outputXfdfPath = "fields.xfdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream for the XFDF output (write mode)
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including AcroForm fields) to XFDF via the stream
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"AcroForm fields exported to XFDF: {outputXfdfPath}");
    }
}