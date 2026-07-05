using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // Added for TextFragment

class Program
{
    static void Main()
    {
        const string outputPath = "output_pdfa.pdf";    // PDF/A‑1b compliant result
        const string logPath    = "conversion_log.xml"; // optional conversion log

        // ---------------------------------------------------------------------
        // 1. Create a minimal source PDF in memory (no external file required).
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a simple page with a paragraph so the document is not empty.
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF for PDF/A conversion"));

            // ---------------------------------------------------------------
            // 2. Convert the document to PDF/A‑1b (archival format).
            //    The conversion is performed in‑place on the Document object.
            // ---------------------------------------------------------------
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // ---------------------------------------------------------------
            // 3. Verify conversion – the IsPdfaCompliant property is read‑only.
            // ---------------------------------------------------------------
            bool isPdfa = doc.IsPdfaCompliant;
            Console.WriteLine($"Document is PDF/A compliant: {isPdfa}");

            // ---------------------------------------------------------------
            // 4. Use PdfFileInfo facade to write the updated PDF/A document.
            //    PdfFileInfo works on a physical file, so we first save the
            //    converted document, then update its info and write the final file.
            // ---------------------------------------------------------------
            // Save the converted document to a temporary location.
            const string tempPath = "temp.pdf";
            doc.Save(tempPath);

            using (PdfFileInfo info = new PdfFileInfo(tempPath))
            {
                // Optional: enforce strict validation when saving.
                info.UseStrictValidation = true;

                // Save the PDF/A‑compliant file (overwrites the temporary file).
                info.SaveNewInfo(outputPath);
            }

            // Clean up the temporary file.
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }

        Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
    }
}
