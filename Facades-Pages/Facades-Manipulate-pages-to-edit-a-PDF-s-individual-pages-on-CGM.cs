using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file, temporary PDF generated from CGM, and final edited PDF.
        const string cgmInputPath   = "input.cgm";
        const string tempPdfPath    = "temp_from_cgm.pdf";
        const string editedPdfPath  = "edited_output.pdf";

        // Verify the CGM source exists.
        if (!File.Exists(cgmInputPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmInputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert the CGM file to a PDF using the PdfProducer facade.
        // -----------------------------------------------------------------
        try
        {
            // Produce a PDF from the CGM file. ImportFormat.Cgm is the enum value for CGM input.
            PdfProducer.Produce(cgmInputPath, ImportFormat.Cgm, tempPdfPath);
            Console.WriteLine($"CGM converted to PDF: {tempPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during CGM → PDF conversion: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Edit individual pages of the generated PDF using PdfPageEditor.
        // -----------------------------------------------------------------
        try
        {
            // Use a using block to ensure the facade is disposed correctly.
            using (PdfPageEditor pageEditor = new PdfPageEditor())
            {
                // Bind the temporary PDF file to the editor.
                pageEditor.BindPdf(tempPdfPath);

                // Example edit: rotate the first page 90 degrees clockwise.
                // ProcessPages specifies which pages the editor will affect (1‑based indexing).
                pageEditor.ProcessPages = new int[] { 1 };
                pageEditor.Rotation = 90; // Valid values: 0, 90, 180, 270.

                // Example edit: change the zoom of the same page to 150%.
                pageEditor.Zoom = 1.5f; // 1.0 = 100%

                // Apply all pending changes to the document.
                pageEditor.ApplyChanges();

                // Save the edited PDF to the final output path.
                pageEditor.Save(editedPdfPath);
                Console.WriteLine($"Edited PDF saved to: {editedPdfPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF page editing: {ex.Message}");
            return;
        }
        finally
        {
            // Clean up the intermediate PDF if desired.
            try
            {
                if (File.Exists(tempPdfPath))
                {
                    File.Delete(tempPdfPath);
                }
            }
            catch
            {
                // Ignored – not critical if the file cannot be deleted.
            }
        }
    }
}