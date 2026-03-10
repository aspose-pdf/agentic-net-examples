using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source TeX file, the FDF annotation file, and the output PDF.
        const string texPath = "input.tex";
        const string fdfPath = "annotations.fdf";
        const string outputPdf = "output.pdf";

        // Verify that the required files exist.
        if (!File.Exists(texPath))
        {
            Console.Error.WriteLine($"TeX file not found: {texPath}");
            return;
        }
        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the TeX file into a PDF document using TeXLoadOptions.
        TeXLoadOptions texLoadOptions = new TeXLoadOptions();
        using (Document doc = new Document(texPath, texLoadOptions))
        {
            // Import annotations from the FDF file into the document.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the resulting PDF with the imported annotations.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with imported annotations saved to '{outputPdf}'.");
    }
}