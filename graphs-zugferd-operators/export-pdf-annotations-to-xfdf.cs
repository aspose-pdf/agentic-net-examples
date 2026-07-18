using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXfdf = "annotations.xfdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPdf))
            {
                // Export all annotations to an XFDF file (operation)
                doc.ExportAnnotationsToXfdf(outputXfdf);
            } // Document is disposed here (lifecycle: disposal)

            Console.WriteLine($"Annotations successfully exported to '{outputXfdf}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}