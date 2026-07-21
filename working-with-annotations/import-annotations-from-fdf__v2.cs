using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // PDF to receive annotations
        const string fdfPath   = "annotations.fdf"; // FDF containing annotations
        const string outputPdf = "output.pdf";   // Resulting PDF

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

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF stream and import annotations
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // This method reads the annotation definitions (including page numbers)
                // and adds them to the appropriate pages in the document.
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the updated PDF with imported annotations
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPdf}'.");
    }
}