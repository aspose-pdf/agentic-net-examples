using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the PDF and FDF files (assumed to be in a Resources folder)
        string pdfPath = Path.Combine("Resources", "input.pdf");
        string fdfPath = Path.Combine("Resources", "annotations.fdf");
        string outputPath = Path.Combine("Resources", "output.pdf");

        // Verify that the source files exist
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

        // Load the PDF document, import annotations from the FDF, and save the result
        using (Document doc = new Document(pdfPath))               // document-disposal-with-using
        {
            using (FileStream fdfStream = File.OpenRead(fdfPath)) // load FDF stream
            {
                // Import annotations from the FDF file into the PDF document
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the PDF with the newly imported annotations
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}