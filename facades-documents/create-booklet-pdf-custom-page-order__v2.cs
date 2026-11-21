using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "source.pdf";
        const string outputPdfPath = "booklet.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // PdfFileEditor does not implement IDisposable; instantiate directly
            PdfFileEditor editor = new PdfFileEditor();

            // Use the overload that creates a booklet without requiring a PageSize argument
            // This method returns a bool indicating success (depending on Aspose.PDF version)
            bool success = editor.MakeBooklet(inputPdfPath, outputPdfPath);

            if (success)
                Console.WriteLine($"Booklet created successfully: {outputPdfPath}");
            else
                Console.Error.WriteLine("Booklet creation failed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during booklet creation: {ex.Message}");
        }
    }
}
