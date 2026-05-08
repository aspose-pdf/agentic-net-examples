using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string pdfPath      = "input.pdf";      // PDF to which interactions will be applied
        const string xfdfPath     = "interaction_log.xfdf"; // XFDF (XML) file containing interaction logs
        const string outputPdf    = "reconstructed_output.pdf";

        // Verify files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Open the XFDF (XML) stream
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                // Import annotations (interaction logs) from the XFDF stream into the PDF
                XfdfReader.ReadAnnotations(xfdfStream, pdfDoc);
            }

            // Save the PDF with the imported interactions
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Reconstructed PDF saved to '{outputPdf}'.");
    }
}