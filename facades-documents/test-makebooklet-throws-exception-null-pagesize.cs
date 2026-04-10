using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Create a minimal PDF with a single blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();               // add one page
            doc.Save(inputPath);           // save the source PDF
        }

        // Test that MakeBooklet throws when the PageSize argument is null
        try
        {
            PdfFileEditor editor = new PdfFileEditor();
            // This call is expected to raise an exception because pageSize is null
            editor.MakeBooklet(inputPath, outputPath, null);
            Console.WriteLine("Test failed: no exception was thrown.");
        }
        catch (Exception ex)
        {
            // Expected path: an exception (e.g., ArgumentNullException) is caught
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