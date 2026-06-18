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

        // Temporary file used for the intermediate concatenation step
        const string tempPdf   = "temp_merge.pdf";

        // Verify that all source files exist before proceeding
        if (!File.Exists(firstPdf)  ||
            !File.Exists(secondPdf) ||
            !File.Exists(thirdPdf))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        try
        {
            // Create the PdfFileEditor facade instance
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
                Console.Error.WriteLine("Failed to concatenate the third PDF.");
                return;
            }

            // Cleanup the temporary file
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