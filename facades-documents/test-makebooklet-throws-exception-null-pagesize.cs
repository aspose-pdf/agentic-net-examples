using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for temporary input and output PDF files
        string inputPath = "temp_input.pdf";
        string outputPath = "temp_output.pdf";

        // -----------------------------------------------------------------
        // Create a simple PDF document with a single blank page.
        // Use the recommended 'using' pattern for deterministic disposal.
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add an empty page (Aspose.Pdf uses 1‑based indexing)
            doc.Pages.Add();

            // Save the document as PDF (no SaveOptions needed for PDF format)
            doc.Save(inputPath);
        }

        // -----------------------------------------------------------------
        // Attempt to create a booklet with a null PageSize.
        // The MakeBooklet method (not the Try* variant) should throw an exception.
        // -----------------------------------------------------------------
        try
        {
            // PdfFileEditor does not implement IDisposable, so no using block is required.
            PdfFileEditor editor = new PdfFileEditor();

            // This call is expected to fail because the PageSize argument is null.
            // It should throw an ArgumentNullException (or a similar exception).
            editor.MakeBooklet(inputPath, outputPath, null);

            // If no exception is thrown, indicate unexpected behavior.
            Console.WriteLine("ERROR: No exception was thrown when PageSize was null.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception is thrown.
            Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Clean up temporary files.
            try { if (File.Exists(inputPath)) File.Delete(inputPath); } catch { }
            try { if (File.Exists(outputPath)) File.Delete(outputPath); } catch { }
        }
    }
}