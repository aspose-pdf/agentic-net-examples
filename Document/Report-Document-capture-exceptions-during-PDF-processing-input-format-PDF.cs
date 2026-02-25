using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string validationLog = "validation_log.txt";

        // Verify that the input PDF exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Wrap Document in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Report basic document information
                Console.WriteLine($"Pages: {doc.Pages.Count}");
                Console.WriteLine($"Title: {doc.Info.Title}");
                Console.WriteLine($"Author: {doc.Info.Author}");

                // Validate the PDF against PDF/A‑1B compliance and capture any validation errors
                // The Validate method returns true if the document complies, false otherwise.
                bool isValid = doc.Validate(validationLog, PdfFormat.PDF_A_1B);
                Console.WriteLine($"Validation result: {(isValid ? "OK" : "Issues found")} (log saved to {validationLog})");

                // Optional: extract all text to demonstrate further processing
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                Console.WriteLine($"Extracted text length: {absorber.Text?.Length ?? 0}");
            }
        }
        // Capture Aspose.Pdf specific exceptions
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        // Capture any other unexpected exceptions
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}