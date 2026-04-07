using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // existing PDF
        const string fdfPath      = "data.fdf";       // FDF file with annotations
        const string outputPdfPath = "merged_output.pdf";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open the FDF stream and import its annotations into the PDF
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the merged document
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdfPath}'.");
    }
}