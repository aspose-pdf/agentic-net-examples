using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for temporary input and output PDFs
        string inputPath = "temp_input.pdf";
        string outputPath = "temp_output.pdf";

        // Create a simple PDF with one blank page
        using (Document doc = new Document())
        {
            // Ensure at least one page exists
            doc.Pages.Add();
            // Save the document to disk
            doc.Save(inputPath);
        }

        // Attempt to create a booklet with a null PageSize
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // This call should throw an ArgumentNullException because pageSize is null
            editor.MakeBooklet(inputPath, outputPath, null);
            Console.WriteLine("MakeBooklet completed without throwing (unexpected).");
        }
        catch (Exception ex)
        {
            // Expected path: an exception is thrown
            Console.WriteLine($"Expected exception caught: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            if (File.Exists(inputPath)) File.Delete(inputPath);
            if (File.Exists(outputPath)) File.Delete(outputPath);
        }
    }
}