using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string ofdPath = "input.ofd";
        const string fdfPath = "annotations.fdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(ofdPath))
        {
            Console.Error.WriteLine($"OFD file not found: {ofdPath}");
            return;
        }

        if (!File.Exists(fdfPath))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfPath}");
            return;
        }

        // Load the OFD file as a PDF document using OfdLoadOptions
        using (Document doc = new Document(ofdPath, new OfdLoadOptions()))
        {
            // Open the FDF file stream and import its annotations into the document
            using (FileStream fdfStream = File.OpenRead(fdfPath))
            {
                FdfReader.ReadAnnotations(fdfStream, doc);
            }

            // Save the resulting document as a PDF file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{outputPdf}'.");
    }
}