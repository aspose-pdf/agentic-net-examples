using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string inputFdfPath  = "annotations.fdf";
        const string outputPdfPath = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputFdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {inputFdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open the FDF file as a stream
            using (FileStream fdfStream = File.OpenRead(inputFdfPath))
            {
                // Import annotations from the FDF stream into the PDF document
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the modified PDF (extension .pdf ensures PDF output)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdfPath}'.");
    }
}