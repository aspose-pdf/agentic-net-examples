using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ConvertErrorAction

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string validationReport = "validation.xml"; // will hold validation report
        const string conversionLog = "conversion_log.xml"; // log for PDF/A conversion
        const string outputPdf = "output_pdfa1b.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Validate the source PDF. The Validate method writes an XML report
            //    to the supplied path and returns a boolean indicating conformance.
            //    We use PdfFormat.PDF_A_1B as the validation format because the
            //    task is about converting to PDF/A‑1b.
            // -----------------------------------------------------------------
            bool conforms = doc.Validate(validationReport, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Document conforms to PDF/A‑1b: {conforms}");

            // -----------------------------------------------------------------
            // 2. Convert the document to PDF/A‑1b. The overload that accepts a log
            //    file path, the target format and an error‑handling action is the
            //    recommended way (see the "use-fontrepository‑substitutions‑and‑document‑convert‑for‑pdfa" fix).
            // -----------------------------------------------------------------
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // -----------------------------------------------------------------
            // 3. Save the converted PDF/A‑1b file.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPdf}'.");
    }
}
