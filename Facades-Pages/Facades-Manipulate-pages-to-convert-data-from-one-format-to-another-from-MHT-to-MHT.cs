using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input MHT file and intermediate/output PDF files.
        const string inputMht   = "input.mht";
        const string tempPdf1   = "temp1.pdf";   // PDF created from MHT.
        const string tempPdf2   = "temp2.pdf";   // After page deletion.
        const string outputPdf  = "output.pdf"; // Final PDF after all manipulations.

        // Verify the source file exists.
        if (!File.Exists(inputMht))
        {
            Console.Error.WriteLine($"Source file not found: {inputMht}");
            return;
        }

        try
        {
            // ------------------------------------------------------------
            // 1. Load the MHT file using the dedicated MhtLoadOptions.
            // ------------------------------------------------------------
            using (Document doc = new Document(inputMht, new MhtLoadOptions()))
            {
                // Save the loaded document as PDF (MHT cannot be saved directly).
                doc.Save(tempPdf1);
            }

            // ------------------------------------------------------------
            // 2. Use PdfFileEditor (a Facade) to delete the first page.
            //    PdfFileEditor does NOT implement IDisposable, so do NOT wrap it in a using block.
            // ------------------------------------------------------------
            PdfFileEditor fileEditor = new PdfFileEditor();
            // Delete page 1 from tempPdf1 and write the result to tempPdf2.
            fileEditor.Delete(tempPdf1, new int[] { 1 }, tempPdf2);

            // ------------------------------------------------------------
            // 3. Use PdfPageEditor (another Facade) to rotate page 2 by 90 degrees.
            // ------------------------------------------------------------
            PdfPageEditor pageEditor = new PdfPageEditor();
            // Bind the PDF that resulted from the previous step.
            pageEditor.BindPdf(tempPdf2);
            // Specify the rotation angle (must be 0, 90, 180 or 270).
            pageEditor.Rotation = 90;
            // Apply the rotation only to page number 2 (1‑based indexing).
            pageEditor.ProcessPages = new int[] { 2 };
            // Commit the changes.
            pageEditor.ApplyChanges();
            // Save the final PDF.
            pageEditor.Save(outputPdf);

            // ------------------------------------------------------------
            // NOTE:
            // Aspose.Pdf does NOT support saving a document back to MHT format.
            // The result is therefore saved as PDF. If MHT output is required,
            // an additional conversion step (outside of Aspose.Pdf) would be needed.
            // ------------------------------------------------------------
            Console.WriteLine($"Processing completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up intermediate files (optional).
            try { if (File.Exists(tempPdf1)) File.Delete(tempPdf1); } catch { }
            try { if (File.Exists(tempPdf2)) File.Delete(tempPdf2); } catch { }
        }
    }
}