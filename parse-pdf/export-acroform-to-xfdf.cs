using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputXfdfPath = "output.xfdf";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Check that the document contains an AcroForm with fields
            if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any AcroForm fields to export.");
                return;
            }

            // Create a FileStream for the XFDF output file and export the form fields
            using (FileStream xfdfStream = new FileStream(outputXfdfPath, FileMode.Create, FileAccess.Write))
            {
                // Export form fields (and related annotations) to XFDF using the Document API
                pdfDoc.ExportAnnotationsToXfdf(xfdfStream);
            }

            Console.WriteLine($"AcroForm fields successfully exported to '{outputXfdfPath}'.");
        }
    }
}
