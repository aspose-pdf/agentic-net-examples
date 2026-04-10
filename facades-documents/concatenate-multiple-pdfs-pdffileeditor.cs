using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files to be concatenated
        const string firstPdf  = "file1.pdf";
        const string secondPdf = "file2.pdf";
        const string thirdPdf  = "file3.pdf";

        // Temporary file for the first concatenation step
        const string tempPdf   = "temp_concat.pdf";

        // Final output file
        const string outputPdf = "merged.pdf";

        // Verify that all source files exist
        if (!File.Exists(firstPdf)  || !File.Exists(secondPdf) || !File.Exists(thirdPdf))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        try
        {
            // Create the PdfFileEditor instance (does not implement IDisposable)
            PdfFileEditor editor = new PdfFileEditor();

            // First concatenation: file1 + file2 -> tempPdf
            bool firstResult = editor.Concatenate(firstPdf, secondPdf, tempPdf);
            if (!firstResult)
            {
                Console.Error.WriteLine("Failed to concatenate the first two PDFs.");
                return;
            }

            // Second concatenation: tempPdf + file3 -> outputPdf
            bool secondResult = editor.Concatenate(tempPdf, thirdPdf, outputPdf);
            if (!secondResult)
            {
                Console.Error.WriteLine("Failed to concatenate the temporary PDF with the third PDF.");
                return;
            }

            // Optional: clean up the temporary file
            if (File.Exists(tempPdf))
            {
                File.Delete(tempPdf);
            }

            Console.WriteLine($"Successfully concatenated PDFs into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}