using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF file as a stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations from the FDF stream into the document.
                // The FdfReader respects page numbers defined in the FDF,
                // so annotations are placed on their corresponding pages.
                Aspose.Pdf.Annotations.FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the updated PDF with the imported annotations
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported successfully. Output saved to '{outputPath}'.");
    }
}