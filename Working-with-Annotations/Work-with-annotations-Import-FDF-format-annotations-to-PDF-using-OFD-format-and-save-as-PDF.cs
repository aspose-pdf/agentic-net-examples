using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // FdfReader resides here

class Program
{
    static void Main()
    {
        const string ofdPath   = "input.ofd";      // Source document in OFD format
        const string fdfPath   = "annotations.fdf"; // FDF file containing annotations
        const string outputPdf = "output.pdf";      // Destination PDF file

        // Verify input files exist
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

        try
        {
            // Load the OFD document; Aspose.Pdf automatically converts it to an in‑memory PDF
            using (Document doc = new Document(ofdPath))
            {
                // Open the FDF stream and import its annotations into the document
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // Save the enriched document as a PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with imported annotations saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}