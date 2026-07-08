using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public static class PdfIntegrityHelper
{
    /// <summary>
    /// Validates a PDF file after annotation modifications.
    /// Returns true if the document is compliant; false otherwise.
    /// A validation log is written to <paramref name="logFilePath"/>.
    /// </summary>
    /// <param name="pdfFilePath">Path to the PDF to be validated.</param>
    /// <param name="logFilePath">Path where the validation log will be saved.</param>
    /// <returns>True if the PDF passes validation; otherwise false.</returns>
    public static bool ValidatePdfIntegrity(string pdfFilePath, string logFilePath)
    {
        if (string.IsNullOrEmpty(pdfFilePath))
            throw new ArgumentException("PDF file path must be provided.", nameof(pdfFilePath));

        if (string.IsNullOrEmpty(logFilePath))
            throw new ArgumentException("Log file path must be provided.", nameof(logFilePath));

        // Use PdfAnnotationEditor (Facade) to bind the PDF.
        // The facade implements IDisposable, so we wrap it in a using block.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Initialize the facade with the target PDF.
            editor.BindPdf(pdfFilePath);

            try
            {
                // Validate the document. The method writes a detailed log to the specified file.
                // Use the correct PdfFormat constant – PdfFormat.PDF_UA_1 – for standard validation.
                bool isValid = editor.Document.Validate(logFilePath, PdfFormat.PDF_UA_1);
                return isValid;
            }
            catch (ObjectReferenceCorruptedException ex)
            {
                // This exception is thrown when a corrupted object reference is detected.
                // Log the error information to the console (or handle as needed) and report failure.
                Console.Error.WriteLine($"Corrupted object detected: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Any other unexpected errors are also treated as validation failures.
                Console.Error.WriteLine($"Validation error: {ex.Message}");
                return false;
            }
        }
    }
}

// Adding a minimal entry point so the project compiles as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // The Main method is intentionally left minimal. It can be used for quick manual testing.
        // Example usage (uncomment and adjust paths as needed):
        //
        // string pdfPath = "sample.pdf";
        // string logPath = "validation.log";
        // bool result = PdfIntegrityHelper.ValidatePdfIntegrity(pdfPath, logPath);
        // Console.WriteLine($"PDF validation result: {result}");
    }
}
