using System;
using System.IO;
using Aspose.Pdf;

class ExportAnnotationsToFdf
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFdfPath = "annotations.xfdf"; // XFDF extension; format inferred automatically

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export only the annotations to an XFDF file.
            // The overload without SaveFormat infers the required format from the file extension.
            pdfDoc.Save(outputFdfPath);
        }

        Console.WriteLine($"Annotations exported to XFDF: {outputFdfPath}");
    }
}