using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";          // source PDF
        const string fdfPath = "annotations.fdf";    // FDF file with annotations
        const string outputPdf = "output.pdf";       // PDF after importing annotations
        const string outputXps = "output.xps";       // optional XPS representation

        // Verify input files exist
        if (!File.Exists(pdfPath) || !File.Exists(fdfPath))
        {
            Console.Error.WriteLine("Required input files not found.");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Import annotations from the FDF stream into the document
                using (FileStream fdfStream = File.OpenRead(fdfPath))
                {
                    FdfReader.ReadAnnotations(fdfStream, doc);
                }

                // Save the PDF with the newly imported annotations
                doc.Save(outputPdf);

                // OPTIONAL: also save the document as XPS using XpsSaveOptions
                XpsSaveOptions xpsOpts = new XpsSaveOptions();
                doc.Save(outputXps, xpsOpts);
            }

            Console.WriteLine($"PDF with annotations saved to '{outputPdf}'.");
            Console.WriteLine($"XPS version saved to '{outputXps}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}