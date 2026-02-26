using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API, includes TeXSaveOptions, TeXLoadOptions
using Aspose.Pdf.Annotations;        // For XfdfReader if needed (optional)

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // Source PDF with annotations
        const string xfdfPath       = "annotations.xfdf";   // Temporary XFDF file
        const string texPath        = "intermediate.tex";   // TeX representation of the PDF
        const string outputPdfPath  = "output.pdf";         // Final PDF after re‑importing annotations

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the original PDF and export its annotations to XFDF
            // ------------------------------------------------------------
            using (Document srcDoc = new Document(inputPdfPath))
            {
                // Export all annotations to an XFDF file
                srcDoc.ExportAnnotationsToXfdf(xfdfPath);
            }

            // ------------------------------------------------------------
            // 2. Convert the PDF to TeX format (preserves visual content)
            // ------------------------------------------------------------
            using (Document srcDoc = new Document(inputPdfPath))
            {
                TeXSaveOptions texSaveOpts = new TeXSaveOptions();
                srcDoc.Save(texPath, texSaveOpts);
            }

            // ------------------------------------------------------------
            // 3. Load the TeX file back into a PDF document
            // ------------------------------------------------------------
            TeXLoadOptions texLoadOpts = new TeXLoadOptions();   // default options
            using (Document texDoc = new Document(texPath, texLoadOpts))
            {
                // --------------------------------------------------------
                // 4. Import the previously exported annotations into the new PDF
                // --------------------------------------------------------
                texDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // --------------------------------------------------------
                // 5. Save the final PDF with annotations restored
                // --------------------------------------------------------
                texDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Annotations exported, PDF converted via TeX, and re‑imported successfully.");
            Console.WriteLine($"Result saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files (optional)
            try { if (File.Exists(xfdfPath)) File.Delete(xfdfPath); } catch { }
            try { if (File.Exists(texPath))  File.Delete(texPath);  } catch { }
        }
    }
}