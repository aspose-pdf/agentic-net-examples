using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string validationXmlPath = "validation.xml"; // PDF/A validation XML report
        const string conversionLogPath = "conversion_log.xml"; // conversion log (optional but required by overload)
        const string outputPdfPath = "output_pdfa4.pdf";   // resulting PDF/A‑4 file

        // Verify that required files exist before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // 1. Validate the document against PDF/A‑4 and write the validation report.
            //    The Validate method returns true if the document conforms.
            bool conforms = pdfDoc.Validate(validationXmlPath, PdfFormat.PDF_A_4);
            Console.WriteLine($"Validation result: {(conforms ? "conforms" : "does NOT conform")}. Report saved to '{validationXmlPath}'.");

            // 2. Convert the document to PDF/A‑4. Use the overload that accepts a log file path,
            //    the target format, and an error‑handling action.
            pdfDoc.Convert(conversionLogPath, PdfFormat.PDF_A_4, ConvertErrorAction.Delete);

            // 3. Save the converted PDF/A‑4 file.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF successfully converted to PDF/A‑4 and saved as '{outputPdfPath}'.");
    }
}
