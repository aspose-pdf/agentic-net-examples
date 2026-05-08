using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";      // source PDF
        const string fdfPath   = "annotations.fdf"; // FDF containing annotations
        const string outputPath = "output.pdf";    // result PDF

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

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF stream
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations; page numbers embedded in the FDF are honored
                Aspose.Pdf.Annotations.FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the PDF with the newly imported annotations
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}