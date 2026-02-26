using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";   // source PDF
        const string fdfPath   = "annotations.fdf"; // FDF file with annotations
        const string outputPdf = "output.pdf";   // PDF with imported annotations

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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Open the FDF stream and import its annotations into the PDF
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // Save the modified PDF (annotations are now embedded)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Annotations imported and PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}