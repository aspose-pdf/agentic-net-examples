using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file paths
        const string pdf1 = "first.pdf";
        const string pdf2 = "second.pdf";
        const string pdf3 = "third.pdf";

        // Output concatenated PDF path
        const string outputPdf = "merged.pdf";

        // Verify that all source files exist
        if (!File.Exists(pdf1) || !File.Exists(pdf2) || !File.Exists(pdf3))
        {
            Console.Error.WriteLine("One or more input PDF files were not found.");
            return;
        }

        try
        {
            // PdfFileEditor does NOT implement IDisposable; do NOT wrap in using
            PdfFileEditor editor = new PdfFileEditor();

            // First concatenate pdf1 and pdf2 into a temporary file
            string tempIntermediate = Path.GetTempFileName();
            editor.Concatenate(pdf1, pdf2, tempIntermediate);

            // Then concatenate the intermediate file with pdf3 to produce the final output
            editor.Concatenate(tempIntermediate, pdf3, outputPdf);

            // Clean up the temporary intermediate file
            File.Delete(tempIntermediate);

            Console.WriteLine($"Successfully concatenated PDFs into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during concatenation: {ex.Message}");
        }
    }
}