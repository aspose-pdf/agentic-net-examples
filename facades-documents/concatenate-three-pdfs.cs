using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        string firstPdf = "first.pdf";
        string secondPdf = "second.pdf";
        string thirdPdf = "third.pdf";
        string tempPdf = "temp_concat.pdf";
        string outputPdf = "merged.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdf))
        {
            Console.Error.WriteLine($"File not found: {firstPdf}");
            return;
        }
        if (!File.Exists(secondPdf))
        {
            Console.Error.WriteLine($"File not found: {secondPdf}");
            return;
        }
        if (!File.Exists(thirdPdf))
        {
            Console.Error.WriteLine($"File not found: {thirdPdf}");
            return;
        }

        PdfFileEditor editor = new PdfFileEditor();

        // First concatenate the first two PDFs into a temporary file
        bool firstResult = editor.Concatenate(firstPdf, secondPdf, tempPdf);
        if (!firstResult)
        {
            Console.Error.WriteLine("Failed to concatenate the first two PDFs.");
            return;
        }

        // Then concatenate the temporary file with the third PDF into the final output
        bool finalResult = editor.Concatenate(tempPdf, thirdPdf, outputPdf);
        if (!finalResult)
        {
            Console.Error.WriteLine("Failed to concatenate the final PDF.");
            return;
        }

        // Clean up the temporary file
        try
        {
            File.Delete(tempPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Successfully merged PDFs into '{outputPdf}'.");
    }
}
