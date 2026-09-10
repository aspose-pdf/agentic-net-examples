using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF to be converted
        const string inputPdfPath = "input.pdf";

        // Intermediate PPTX file
        const string intermediatePptxPath = "intermediate.pptx";

        // Final PDF/A file for archival
        const string outputPdfAPath = "archival_output.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // Step 1: Load the source PDF and save it as PPTX
            // -------------------------------------------------
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Initialize PPTX save options (default constructor)
                PptxSaveOptions pptxOptions = new PptxSaveOptions();

                // Save the PDF as PPTX
                pdfDocument.Save(intermediatePptxPath, pptxOptions);
            }

            // -------------------------------------------------
            // Step 2: Load the generated PPTX and convert to PDF/A
            // -------------------------------------------------
            // Aspose.PDF can auto‑detect the PPTX format when loading from a file path,
            // so we do not need a dedicated PptxLoadOptions class (which is not present
            // in the referenced version). This also resolves the CS0246 and CS1503 errors.
            using (Document pptxDocument = new Document(intermediatePptxPath))
            {
                // Convert the document to PDF/A (PDF_A_1B) and log any conversion errors.
                // The Convert method modifies the document in‑place.
                pptxDocument.Convert("conversion_log.txt", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the PDF/A compliant document
                pptxDocument.Save(outputPdfAPath);
            }

            Console.WriteLine($"Conversion completed. PDF/A saved to '{outputPdfAPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
