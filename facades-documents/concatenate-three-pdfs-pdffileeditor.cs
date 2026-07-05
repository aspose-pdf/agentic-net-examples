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

        // Output PDF file
        const string outputPdf = "merged.pdf";

        // Verify that all input files exist
        if (!File.Exists(firstPdf) ||
            !File.Exists(secondPdf) ||
            !File.Exists(thirdPdf))
        {
            Console.Error.WriteLine("One or more input files were not found.");
            return;
        }

        // Temporary file to hold the intermediate result of the first concatenation
        string tempPdf = Path.GetTempFileName();

        try
        {
            // Create the PdfFileEditor instance (no IDisposable implementation)
            PdfFileEditor editor = new PdfFileEditor();

            // First concatenation: file1.pdf + file2.pdf -> tempPdf
            bool firstSuccess = editor.Concatenate(firstPdf, secondPdf, tempPdf);
            if (!firstSuccess)
            {
                Console.Error.WriteLine("Failed to concatenate the first two PDFs.");
                return;
            }

            // Second concatenation: tempPdf + file3.pdf -> outputPdf
            bool secondSuccess = editor.Concatenate(tempPdf, thirdPdf, outputPdf);
            if (!secondSuccess)
            {
                Console.Error.WriteLine("Failed to concatenate the third PDF.");
                return;
            }

            Console.WriteLine($"Successfully concatenated PDFs into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary file if it exists
            try
            {
                if (File.Exists(tempPdf))
                {
                    File.Delete(tempPdf);
                }
            }
            catch
            {
                // Suppress any errors while deleting the temp file
            }
        }
    }
}