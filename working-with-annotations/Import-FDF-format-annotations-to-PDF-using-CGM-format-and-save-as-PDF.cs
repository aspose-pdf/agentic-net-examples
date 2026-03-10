using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";          // CGM source file
        const string fdfPath = "annotations.fdf";    // FDF file containing annotations
        const string outputPdf = "output.pdf";       // Resulting PDF file

        // Verify input files exist
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the CGM file into a PDF document using CgmLoadOptions
        CgmLoadOptions loadOptions = new CgmLoadOptions();
        using (Document doc = new Document(cgmPath, loadOptions))
        {
            // Import annotations from the FDF file into the loaded document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the document as a PDF (Document.Save without SaveOptions always writes PDF)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdf}'.");
    }
}