using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for FdfReader

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string texInputPath   = "input.tex";   // TeX source file
        const string fdfAnnotations = "annotations.fdf"; // FDF file with annotations
        const string outputPdfPath  = "output.pdf";

        // Verify files exist
        if (!File.Exists(texInputPath))
        {
            Console.Error.WriteLine($"TeX source not found: {texInputPath}");
            return;
        }
        if (!File.Exists(fdfAnnotations))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfAnnotations}");
            return;
        }

        // Load the TeX file and create a PDF document
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document pdfDoc = new Document(texInputPath, texLoadOptions))
        {
            // Import annotations from the FDF file into the PDF document
            using (FileStream fdfStream = File.OpenRead(fdfAnnotations))
            {
                // FdfReader reads annotations from a stream and adds them to the document
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdfPath}'.");
    }
}