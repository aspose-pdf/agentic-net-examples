using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the FDF file containing annotations
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output_with_annotations.pdf";

        // Verify that the input files exist
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF stream (read‑only) and import its annotations into the document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // The FdfReader reads page numbers from the FDF and places each annotation
                // on the corresponding page of the PDF automatically.
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the updated PDF with the imported annotations
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}