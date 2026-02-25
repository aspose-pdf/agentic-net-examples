using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the XSL‑FO source file (PDF/A compliant XSL‑FO)
        const string xslFoPath = "input.xslfo";
        // Desired output PDF file path
        const string outputPdfPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(xslFoPath))
        {
            Console.Error.WriteLine($"Source file not found: {xslFoPath}");
            return;
        }

        // Load options for XSL‑FO; no XSL stylesheet is required here
        XslFoLoadOptions loadOptions = new XslFoLoadOptions();

        // Load the XSL‑FO document and convert it to a PDF document
        using (Document pdfDoc = new Document(xslFoPath, loadOptions))
        {
            // If the XSL‑FO was generated as PDF/A and you want a regular PDF,
            // you can remove PDF/A compliance (optional)
            // pdfDoc.RemovePdfaCompliance();

            // Save the resulting PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"XSL‑FO successfully converted to PDF: {outputPdfPath}");
    }
}