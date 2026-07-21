using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // FdfReader resides here

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        string pdfPath      = "input.pdf";                     // source PDF
        string fdfPath      = Path.Combine("Resources", "annotations.fdf"); // FDF file in resources folder
        string outputPath   = "output_with_annotations.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF not found: {fdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: create/load)
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF stream and import its annotations into the document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
                // fdfStream will be closed automatically by the using block
            }

            // Save the updated PDF (lifecycle: save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}