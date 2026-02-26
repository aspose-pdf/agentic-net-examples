using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string htmlInput   = "input.html";          // source HTML file
        const string pdfOutput   = "converted.pdf";       // PDF after HTML conversion
        const string xfdfFile    = "annotations.xfdf";    // intermediate XFDF file
        const string finalPdf    = "final.pdf";           // PDF after re‑importing annotations

        if (!File.Exists(htmlInput))
        {
            Console.Error.WriteLine($"HTML file not found: {htmlInput}");
            return;
        }

        // 1. Load HTML and convert to PDF (HTML conversion requires GDI+ – Windows only)
        Document doc;
        try
        {
            // HtmlLoadOptions is in Aspose.Pdf namespace; no extra using needed
            doc = new Document(htmlInput, new HtmlLoadOptions());
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load HTML: {ex.Message}");
            return;
        }

        using (doc) // ensure deterministic disposal
        {
            // Save the intermediate PDF
            try
            {
                // SaveOptions must be supplied for non‑PDF formats; here we are saving PDF, so default Save() is fine
                doc.Save(pdfOutput);
                Console.WriteLine($"HTML converted to PDF: {pdfOutput}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
                return;
            }

            // 2. Export all annotations to XFDF
            try
            {
                doc.ExportAnnotationsToXfdf(xfdfFile);
                Console.WriteLine($"Annotations exported to XFDF: {xfdfFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ExportAnnotationsToXfdf failed: {ex.Message}");
                // Continue – the document may have no annotations
            }

            // 3. (Optional) Remove existing annotations to demonstrate import
            // For simplicity we skip removal; we just re‑import the same XFDF

            // 4. Import annotations from XFDF back into the document
            try
            {
                doc.ImportAnnotationsFromXfdf(xfdfFile);
                Console.WriteLine($"Annotations imported from XFDF: {xfdfFile}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ImportAnnotationsFromXfdf failed: {ex.Message}");
                // Continue – document will be saved without imported annotations
            }

            // 5. Save the final PDF (with imported annotations)
            try
            {
                doc.Save(finalPdf);
                Console.WriteLine($"Final PDF saved: {finalPdf}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save final PDF: {ex.Message}");
            }
        }
    }
}