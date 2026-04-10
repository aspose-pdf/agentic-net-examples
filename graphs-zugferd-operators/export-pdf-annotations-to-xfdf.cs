using System;
using System.IO;
using Aspose.Pdf;

namespace ExportAnnotationsApp
{
    class Program
    {
        static void Main()
        {
            // Input PDF file containing annotations
            const string inputPdfPath = "input.pdf";

            // Output XFDF file where annotations will be exported
            const string outputXfdfPath = "annotations.xfdf";

            // Verify that the source PDF exists
            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
                return;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (var pdfDoc = new Document(inputPdfPath))
            {
                // Export all annotations to the specified XFDF file
                pdfDoc.ExportAnnotationsToXfdf(outputXfdfPath);
            }

            Console.WriteLine($"Annotations exported successfully to '{outputXfdfPath}'.");
        }
    }
}