using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file, intermediate PDF, and final PDF paths
        const string cgmPath      = "input.cgm";
        const string intermediatePdfPath = "intermediate.pdf";
        const string finalPdfPath = "output_resized.pdf";

        // Verify the CGM file exists
        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Convert CGM to PDF using PdfProducer (CGM is input‑only)
            // ------------------------------------------------------------
            // PdfProducer.Produce creates a PDF file from the CGM source.
            // No additional load options are required for a simple conversion.
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, intermediatePdfPath);
            Console.WriteLine($"CGM converted to PDF: {intermediatePdfPath}");

            // ------------------------------------------------------------
            // 2. Change page size of the generated PDF using PdfPageEditor
            // ------------------------------------------------------------
            // Create the editor facade and bind the intermediate PDF.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(intermediatePdfPath);

                // Set the desired output page size.
                // Aspose.Pdf.PageSize provides common sizes (A0‑A6, Letter, etc.).
                // Here we change all pages to A3 size.
                editor.PageSize = PageSize.A3; // 420 x 297 mm

                // Optionally, specify which pages to process.
                // By default all pages are processed, so this line is optional.
                // editor.ProcessPages = new int[] { 1, 2, 3 }; // example for first three pages

                // Apply the changes to the document.
                editor.ApplyChanges();

                // Save the resized PDF to the final output file.
                editor.Save(finalPdfPath);
            }

            Console.WriteLine($"Page sizes changed and saved to: {finalPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}