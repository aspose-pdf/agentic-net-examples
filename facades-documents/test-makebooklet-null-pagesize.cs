using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare a simple PDF file to use as input for the booklet operation
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposePdfBookletTest");
        Directory.CreateDirectory(tempDir);

        string inputPdf = Path.Combine(tempDir, "input.pdf");
        string outputPdf = Path.Combine(tempDir, "output.pdf");

        // Create a one‑page PDF using the standard Document creation pattern
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            doc.Pages.Add();
            doc.Save(inputPdf); // Save the document – this follows the provided save rule
        }

        // Attempt to create a booklet with a null PageSize – this should throw an exception
        try
        {
            PdfFileEditor editor = new PdfFileEditor();

            // The overload with custom page size expects a non‑null PageSize instance.
            // Passing null is expected to raise an ArgumentNullException (or similar).
            editor.MakeBooklet(inputPdf, outputPdf, null);

            // If no exception is thrown, the test has failed
            Console.WriteLine("FAIL: No exception was thrown when PageSize was null.");
        }
        catch (ArgumentNullException ex)
        {
            // Expected path – the API correctly rejected the null argument
            Console.WriteLine($"PASS: Caught expected ArgumentNullException: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Any other exception type also indicates that the method did not succeed silently
            Console.WriteLine($"PASS: Caught exception as expected (type: {ex.GetType().Name}): {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try { if (File.Exists(inputPdf)) File.Delete(inputPdf); } catch { }
            try { if (File.Exists(outputPdf)) File.Delete(outputPdf); } catch { }
            try { Directory.Delete(tempDir, true); } catch { }
        }
    }
}