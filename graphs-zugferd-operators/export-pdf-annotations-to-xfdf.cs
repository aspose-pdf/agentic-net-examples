using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document; the using block ensures proper disposal.
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Export all annotations in the document to an XFDF file.
                pdfDoc.ExportAnnotationsToXfdf(outputXfdf);
            }

            Console.WriteLine($"Annotations successfully exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}