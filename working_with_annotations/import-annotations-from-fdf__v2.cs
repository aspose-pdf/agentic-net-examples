using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "annotations.fdf";
        const string outputPath = "output.pdf";

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

        // Load the PDF document.
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(pdfPath))
        {
            // Open the FDF file as a stream.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations from the FDF into the document.
                // Annotations are placed on the pages indicated in the FDF data.
                Aspose.Pdf.Annotations.FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the PDF with the imported annotations.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}