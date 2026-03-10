using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Included as per requirement (not directly used)

class Program
{
    static void Main()
    {
        // Directory that contains the source XSL‑FO file.
        string dataDir = @"YOUR_DATA_DIRECTORY";

        // Path to the XSL‑FO input file.
        string xslFoPath = Path.Combine(dataDir, "input.xslfo");

        // Desired path for the resulting regular PDF.
        string outputPdfPath = Path.Combine(dataDir, "output.pdf");

        // Verify that the source file exists.
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"XSL‑FO file not found: {xslFoPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Load the XSL‑FO document using XslFoLoadOptions.
        // The constructor of Aspose.Pdf.Document that accepts a file name
        // and a LoadOptions instance performs the conversion from XSL‑FO
        // to a PDF document in memory.
        // -----------------------------------------------------------------
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Use a using block to ensure the Document is disposed properly.
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(xslFoPath, loadOptions))
        {
            // If the generated PDF is PDF/A compliant, remove that compliance
            // to obtain a regular PDF. This call is safe even when the document
            // is not PDF/A; it simply does nothing.
            pdfDoc.RemovePdfaCompliance();

            // Save the in‑memory PDF as a standard PDF file.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Conversion completed. PDF saved to '{outputPdfPath}'.");
    }
}