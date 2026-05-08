using System;
using System.IO;
using Aspose.Pdf;   // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF
        const string xfdfOutputPath = "output.xfdf"; // destination XFDF file

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a FileStream for writing the XFDF data
            // (lifecycle rule: wrap the stream in using to ensure it is closed)
            using (FileStream xfdfStream = new FileStream(xfdfOutputPath,
                                                          FileMode.Create,
                                                          FileAccess.Write))
            {
                // Export all annotations (including form fields) to XFDF via the stream
                // Correct API: Document.ExportAnnotationsToXfdf(Stream)
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
                // The using block will automatically close the stream
            }

            // No need to save the PDF here; we only exported XFDF data
        }

        Console.WriteLine($"XFDF data successfully written to '{xfdfOutputPath}'.");
    }
}