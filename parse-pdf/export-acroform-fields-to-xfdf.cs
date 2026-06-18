using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document containing AcroForm fields
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream to write the XFDF data
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including form fields) to the XFDF stream
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
            }
        }

        Console.WriteLine($"AcroForm fields exported to XFDF file: {outputXfdfPath}");
    }
}