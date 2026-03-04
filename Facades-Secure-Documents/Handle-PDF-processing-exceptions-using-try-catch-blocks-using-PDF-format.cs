using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "concatenated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF to demonstrate a core‑API operation (e.g., page count)
            using (Document doc = new Document(inputPath))
            {
                Console.WriteLine($"Input PDF has {doc.Pages.Count} page(s).");
            }

            // Use a Facade (PdfFileEditor) to concatenate the PDF with itself.
            // PdfFileEditor does NOT implement IDisposable, so it is NOT wrapped in a using block.
            PdfFileEditor editor = new PdfFileEditor();

            // TryConcatenate returns false on failure; the reason can be obtained via LastException.
            bool concatenated = editor.TryConcatenate(inputPath, inputPath, outputPath);

            if (!concatenated)
            {
                // Retrieve the underlying exception for detailed diagnostics.
                Exception last = editor.LastException;
                Console.Error.WriteLine("Concatenation failed.");
                if (last != null)
                {
                    Console.Error.WriteLine($"Message: {last.Message}");
                    if (last.InnerException != null)
                        Console.Error.WriteLine($"Inner: {last.InnerException.Message}");
                }
                return;
            }

            Console.WriteLine($"Successfully concatenated to '{outputPath}'.");
        }
        // Specific Aspose.Pdf exceptions
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine("Invalid password provided for the PDF.");
            Console.Error.WriteLine($"Message: {ex.Message}");
        }
        catch (InvalidPdfFileFormatException ex)
        {
            Console.Error.WriteLine("The PDF file format is invalid or corrupted.");
            Console.Error.WriteLine($"Message: {ex.Message}");
        }
        catch (PdfException ex)
        {
            Console.Error.WriteLine("A generic PDF processing error occurred.");
            Console.Error.WriteLine($"Message: {ex.Message}");
        }
        // Fallback for any other unexpected errors
        catch (Exception ex)
        {
            Console.Error.WriteLine("An unexpected error occurred.");
            Console.Error.WriteLine($"Message: {ex.Message}");
        }
    }
}