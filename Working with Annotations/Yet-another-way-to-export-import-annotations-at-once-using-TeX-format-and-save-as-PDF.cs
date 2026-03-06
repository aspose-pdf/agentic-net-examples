using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, intermediate files, and final output.
        const string inputPdfPath   = "input.pdf";
        const string xfdfPath       = "annotations.xfdf";
        const string texPath        = "intermediate.tex";
        const string outputPdfPath  = "output.pdf";

        // Ensure the input PDF exists.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // STEP 1: Load the original PDF and export its annotations to XFDF.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Export all annotations to an XFDF file.
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
        }

        // STEP 2: Convert the PDF to TeX format using TeXSaveOptions.
        // NOTE: TeX conversion (and the underlying System.Drawing usage) works only on Windows.
        // On non‑Windows platforms we skip the TeX round‑trip and directly copy the original PDF.
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Initialize TeX save options (default constructor is sufficient here).
                TeXSaveOptions texSaveOptions = new TeXSaveOptions();

                // Save the PDF as a TeX file.
                pdfDoc.Save(texPath, texSaveOptions);
            }

            // STEP 3: Load the TeX file back into a PDF document.
            using (Document texDoc = new Document(texPath, new TeXLoadOptions()))
            {
                // Import the previously exported annotations from the XFDF file.
                texDoc.ImportAnnotationsFromXfdf(xfdfPath);

                // Save the final PDF with annotations restored.
                texDoc.Save(outputPdfPath);
            }
        }
        else
        {
            // Fallback for Linux/macOS: simply copy the original PDF and import annotations.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Process completed. Output PDF saved to '{outputPdfPath}'.");
    }
}
