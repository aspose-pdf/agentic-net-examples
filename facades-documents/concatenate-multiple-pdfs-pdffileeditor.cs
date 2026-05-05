using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files – ensure they exist before proceeding
        const string firstPdf  = "file1.pdf";
        const string secondPdf = "file2.pdf";
        const string thirdPdf  = "file3.pdf";

        // Intermediate file used for the first concatenation step
        const string tempPdf   = "temp_concat.pdf";

        // Final merged output
        const string outputPdf = "merged.pdf";

        // Verify input files are present
        if (!File.Exists(firstPdf)  ||
            !File.Exists(secondPdf) ||
            !File.Exists(thirdPdf))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        try
        {
            // Create the PdfFileEditor facade (no IDisposable implementation)
            PdfFileEditor editor = new PdfFileEditor();

            // Step 1: concatenate the first two PDFs into a temporary file
            bool firstStep = editor.Concatenate(firstPdf, secondPdf, tempPdf);
            if (!firstStep)
            {
                Console.Error.WriteLine("Failed to concatenate the first two PDFs.");
                return;
            }

            // Step 2: concatenate the temporary file with the third PDF into the final output
            bool secondStep = editor.Concatenate(tempPdf, thirdPdf, outputPdf);
            if (!secondStep)
            {
                Console.Error.WriteLine("Failed to concatenate the third PDF.");
                return;
            }

            // Optional cleanup of the intermediate file
            if (File.Exists(tempPdf))
            {
                File.Delete(tempPdf);
            }

            Console.WriteLine($"Successfully merged PDFs into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}