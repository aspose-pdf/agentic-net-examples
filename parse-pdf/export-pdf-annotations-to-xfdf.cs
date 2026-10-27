using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a Form object to work with AcroForm data
            Form pdfForm = new Form(pdfDoc);

            // Open a FileStream for writing the XFDF data; using ensures the stream is closed
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export the form data to XFDF format and write it to the stream
                pdfForm.ExportXfdf(xfdfStream);
            }

            // No further actions needed; the using blocks close both Document and FileStream
        }

        Console.WriteLine($"XFDF data exported to '{outputXfdfPath}'.");
    }
}