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

        // Load the PDF document; using ensures proper disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Open the FDF file as a stream.
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                // Import annotations; page numbers embedded in the FDF are applied automatically.
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the PDF with the imported annotations.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations imported and saved to '{outputPath}'.");
    }
}