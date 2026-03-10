using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file, temporary PDF (produced from CGM), and final edited PDF paths
        const string inputCgmPath   = "input.cgm";
        const string tempPdfPath    = "temp_from_cgm.pdf";
        const string outputPdfPath  = "edited_output.pdf";

        // Verify the CGM source exists
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Convert the CGM file to a PDF using the PdfProducer facade.
            //    This creates an intermediate PDF that we will edit.
            // -----------------------------------------------------------------
            PdfProducer.Produce(inputCgmPath, ImportFormat.Cgm, tempPdfPath);
            Console.WriteLine($"CGM converted to PDF: {tempPdfPath}");

            // -----------------------------------------------------------------
            // 2. Edit individual pages using PdfPageEditor.
            //    We bind the editor to the intermediate PDF, apply transformations,
            //    and then save the result to the final output file.
            // -----------------------------------------------------------------
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the editor to the PDF produced from the CGM file
                pageEditor.BindPdf(tempPdfPath);

                // Example transformations (applied to all pages):
                // - Rotate each page 90 degrees clockwise
                // - Scale (zoom) each page to 90% of its original size
                // - Shift the origin of each page by (10, 20) points
                pageEditor.Rotation = 90;          // Valid values: 0, 90, 180, 270
                pageEditor.Zoom = 0.9f;            // 1.0 = 100% (float literal)
                pageEditor.MovePosition(10f, 20f); // X and Y offsets in points (float literals)

                // Preserve original page size by not altering the PageSize property.
                // If you need to edit specific pages only, set ProcessPages, e.g.:
                // pageEditor.ProcessPages = new int[] { 1, 3, 5 };

                // Apply the changes to the document
                pageEditor.ApplyChanges();

                // Save the edited PDF to the desired output location
                pageEditor.Save(outputPdfPath);
                Console.WriteLine($"Edited PDF saved: {outputPdfPath}");
            }

            // Optional: clean up the intermediate PDF if no longer needed
            if (File.Exists(tempPdfPath))
            {
                File.Delete(tempPdfPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
