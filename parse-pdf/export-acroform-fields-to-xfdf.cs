using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream for the XFDF output
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including AcroForm fields) to the XFDF stream
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"AcroForm fields exported to XFDF file: {outputXfdfPath}");
    }
}