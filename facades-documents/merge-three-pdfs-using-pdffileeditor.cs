using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files – adjust paths as needed
        const string firstPdf  = "file1.pdf";
        const string secondPdf = "file2.pdf";
        const string thirdPdf  = "file3.pdf";

        // Output PDF file
        const string outputPdf = "merged.pdf";

        // Validate input files
        if (!File.Exists(firstPdf)  || !File.Exists(secondPdf) || !File.Exists(thirdPdf))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // Temporary file to hold intermediate concatenation result
        string tempPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

        try
        {
            // First concatenation: file1 + file2 -> tempPdf
            PdfFileEditor editor = new PdfFileEditor();
            bool firstResult = editor.Concatenate(firstPdf, secondPdf, tempPdf);
            if (!firstResult)
            {
                Console.Error.WriteLine("Failed to concatenate first two PDFs.");
                return;
            }

            // Second concatenation: tempPdf + file3 -> outputPdf
            bool secondResult = editor.Concatenate(tempPdf, thirdPdf, outputPdf);
            if (!secondResult)
            {
                Console.Error.WriteLine("Failed to concatenate the third PDF.");
                return;
            }

            Console.WriteLine($"Successfully merged PDFs into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary file
            if (File.Exists(tempPdf))
            {
                try { File.Delete(tempPdf); } catch { /* ignore */ }
            }
        }
    }
}