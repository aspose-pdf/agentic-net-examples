using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXfdf = "fields.xfdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify that the document contains an AcroForm with fields
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
                return;
            }

            // Create a FileStream for the XFDF output; using ensures the stream is closed
            using (FileStream xfdfStream = new FileStream(outputXfdf, FileMode.Create, FileAccess.Write))
            {
                // Export form fields (widget annotations) to XFDF format
                doc.ExportAnnotationsToXfdf(xfdfStream);
            }

            Console.WriteLine($"AcroForm fields exported to '{outputXfdf}'.");
        }
    }
}
