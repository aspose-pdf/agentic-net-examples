using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // Required for PdfException

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Use the PdfFileInfo facade to bind the PDF file.
            // The facade implements IDisposable, so wrap it in a using block.
            using (PdfFileInfo pdfInfo = new PdfFileInfo())
            {
                pdfInfo.BindPdf(inputPath); // Load the PDF

                // Example operation: update some metadata fields
                pdfInfo.Title  = "Processed PDF";
                pdfInfo.Author = "Aspose Example";

                // Save the modified PDF to a new file.
                pdfInfo.Save(outputPath);
            }

            Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
        }
        // Catch I/O related errors (e.g., file not accessible, disk full)
        catch (IOException ioEx)
        {
            Console.Error.WriteLine($"I/O error: {ioEx.Message}");
        }
        // Catch PDF-specific errors thrown by Aspose.Pdf
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PDF error: {pdfEx.Message}");
        }
        // Catch any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}