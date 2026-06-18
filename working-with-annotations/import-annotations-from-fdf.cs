using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";                     // source PDF
        const string fdfPath = "Resources/annotations.fdf";    // FDF file in resources folder
        const string outputPath = "output.pdf";                // result PDF

        // Verify that both files exist before proceeding
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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF file as a stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations from the FDF stream into the PDF document
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the PDF with the imported annotations
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}