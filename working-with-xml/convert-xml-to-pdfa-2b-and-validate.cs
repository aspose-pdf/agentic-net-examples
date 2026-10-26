using System;
using System.IO;
using Aspose.Pdf;

class PdfA2bValidation
{
    static void Main()
    {
        // Input PDF, intermediate XML, and final PDF/A‑2b paths
        const string inputPdfPath = "input.pdf";
        const string xmlPath = "intermediate.xml";
        const string pdfAPath = "output_pdfa2b.pdf";
        const string validationLogPath = "validation_log.xml";

        // Verify the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the original PDF and export it to XML (non‑PDF format)
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // XmlSaveOptions is required; otherwise the file would be saved as PDF
            XmlSaveOptions xmlOpts = new XmlSaveOptions();
            pdfDoc.Save(xmlPath, xmlOpts);
        }

        // -----------------------------------------------------------------
        // 2. Load the XML representation back into a Document object
        // -----------------------------------------------------------------
        using (Document xmlDoc = new Document(xmlPath, new XmlLoadOptions()))
        {
            // -----------------------------------------------------------------
            // 3. Convert the document to PDF/A‑2b using PdfFormatConversionOptions
            // -----------------------------------------------------------------
            PdfFormatConversionOptions convOpts = new PdfFormatConversionOptions(PdfFormat.PDF_A_2B);
            xmlDoc.Convert(convOpts);

            // Save the PDF/A‑2b file
            xmlDoc.Save(pdfAPath);
        }

        // -----------------------------------------------------------------
        // 4. Validate the resulting PDF/A‑2b file for compliance
        // -----------------------------------------------------------------
        try
        {
            // Load the generated PDF/A‑2b document
            using (Document pdfADoc = new Document(pdfAPath))
            {
                // The Validate method returns a boolean indicating compliance.
                // It also creates a log file (XML) with detailed validation information.
                bool isCompliant = pdfADoc.Validate(validationLogPath, PdfFormat.PDF_A_2B);

                if (isCompliant)
                {
                    Console.WriteLine($"The file '{pdfAPath}' is compliant with PDF/A‑2b.");
                }
                else
                {
                    Console.WriteLine($"The file '{pdfAPath}' is NOT compliant with PDF/A‑2b.");
                    Console.WriteLine($"See validation details in '{validationLogPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation failed: {ex.Message}");
        }
    }
}
