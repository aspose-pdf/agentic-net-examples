using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";      // source PDF
        const string fdfPath = "annotations.fdf"; // FDF containing annotations
        const string outputPath = "output.pdf";   // PDF with imported annotations

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

        // Load PDF, import annotations, and save
        using (Document doc = new Document(pdfPath))
        {
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations; page numbers inside the FDF are respected automatically
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the PDF with the newly added annotations
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}