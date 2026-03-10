using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string mhtInputPath   = "input.mht";   // source MHT file
        const string fdfAnnotations = "annotations.fdf"; // FDF file containing annotations
        const string pdfOutputPath  = "output.pdf";   // resulting PDF

        // Verify input files exist
        if (!File.Exists(mhtInputPath))
        {
            Console.Error.WriteLine($"MHT file not found: {mhtInputPath}");
            return;
        }
        if (!File.Exists(fdfAnnotations))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfAnnotations}");
            return;
        }

        // Load the MHT file into a PDF document using MhtLoadOptions
        using (Document pdfDoc = new Document(mhtInputPath, new MhtLoadOptions()))
        {
            // Open the FDF file as a stream and import its annotations into the document
            using (FileStream fdfStream = File.OpenRead(fdfAnnotations))
            {
                // FdfReader reads annotations from the FDF stream and adds them to the document
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // Save the modified document as a PDF file
            pdfDoc.Save(pdfOutputPath);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{pdfOutputPath}'.");
    }
}