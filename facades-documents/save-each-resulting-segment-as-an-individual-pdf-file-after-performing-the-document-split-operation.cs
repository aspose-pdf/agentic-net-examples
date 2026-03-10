using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file to be split
        const string inputPath = "input.pdf";

        // Output file name template.
        // %NUM% will be replaced with the page number (starting from 1)
        const string outputTemplate = "output_page%NUM%.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // PdfFileEditor provides the SplitToPages method that saves each page
            // directly to a separate PDF file according to the template.
            PdfFileEditor editor = new PdfFileEditor();

            // Split the document; each page is saved as a distinct PDF file.
            editor.SplitToPages(inputPath, outputTemplate);

            Console.WriteLine("Document split into individual pages successfully.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during the split operation
            Console.Error.WriteLine($"Error during split: {ex.Message}");
        }
    }
}