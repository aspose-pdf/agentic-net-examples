using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the temporary input and output PDF files
        const string inputPath  = "temp_input.pdf";
        const string outputPath = "temp_output.pdf";

        // Ensure any previous files are removed
        if (File.Exists(inputPath))  File.Delete(inputPath);
        if (File.Exists(outputPath)) File.Delete(outputPath);

        // Create a simple one‑page PDF to use as the source document
        using (Document doc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            doc.Pages.Add();
            doc.Save(inputPath);
        }

        // PdfFileEditor does NOT implement IDisposable; instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();
        try
        {
            // Attempt to create a booklet with a null PageSize.
            // This should throw an ArgumentNullException (or a similar exception)
            // because the method expects a non‑null PageSize instance.
            editor.MakeBooklet(inputPath, outputPath, null);
            Console.WriteLine("ERROR: No exception was thrown when PageSize was null.");
        }
        catch (ArgumentNullException ex)
        {
            // Expected path – the method correctly rejected the null argument
            Console.WriteLine($"Caught expected ArgumentNullException: {ex.ParamName}");
        }
        catch (Exception ex)
        {
            // Any other exception type indicates unexpected behavior
            Console.WriteLine($"Caught unexpected exception type: {ex.GetType().Name} - {ex.Message}");
        }

        // Clean up temporary files
        if (File.Exists(inputPath))  File.Delete(inputPath);
        if (File.Exists(outputPath)) File.Delete(outputPath);
    }
}
