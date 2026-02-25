using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xslFoPath   = "input.xslfo";   // XSL‑FO source file
        const string outputPdf   = "output.pdf";    // Desired PDF output

        // Verify source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Source XSL‑FO file not found: {xslFoPath}");
            return;
        }

        try
        {
            // Load the XSL‑FO file into a Document using XslFoLoadOptions
            XslFoLoadOptions loadOptions = new XslFoLoadOptions();   // no external XSL stylesheet
            using (Document pdfDoc = new Document(xslFoPath, loadOptions))
            {
                // If the generated PDF is PDF/A compliant, remove that compliance
                // to obtain a regular PDF file.
                pdfDoc.RemovePdfaCompliance();

                // Save the result as a standard PDF.
                pdfDoc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}