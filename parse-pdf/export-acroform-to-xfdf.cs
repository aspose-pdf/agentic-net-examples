using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "output.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a FileStream for the XFDF output
            using (FileStream fs = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                // Export all annotations (including AcroForm fields) to XFDF via the stream
                doc.ExportAnnotationsToXfdf(fs);
            }
        }

        Console.WriteLine($"XFDF exported to '{outputXfdf}'.");
    }
}