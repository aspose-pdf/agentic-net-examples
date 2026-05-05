using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare a simple PDF file to act as the source document.
        string inputPath = "temp_input.pdf";
        string outputPath = "temp_output.pdf";

        // Create a one‑page PDF using the Document API (lifecycle rule).
        using (Document doc = new Document())
        {
            // Add a blank page.
            doc.Pages.Add();
            // Save the document to a file.
            doc.Save(inputPath);
        }

        // Instantiate the PdfFileEditor facade.
        PdfFileEditor editor = new PdfFileEditor();

        // Attempt to create a booklet with null page‑order arrays.
        // The overload MakeBooklet(string, string, int[], int[]) should throw
        // an exception when either array is null.
        try
        {
            // Both leftPages and rightPages are null.
            editor.MakeBooklet(inputPath, outputPath, null, null);
            Console.WriteLine("MakeBooklet completed without throwing – test failed.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception is thrown.
            Console.WriteLine($"Caught expected exception: {ex.GetType().Name}");
            Console.WriteLine($"Message: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files.
            if (File.Exists(inputPath)) File.Delete(inputPath);
            if (File.Exists(outputPath)) File.Delete(outputPath);
        }
    }
}