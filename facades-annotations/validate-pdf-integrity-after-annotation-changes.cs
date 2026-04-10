using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

namespace PdfIntegrity
{
    /// <summary>
    /// Helper class that validates a PDF file for corrupted objects after annotation changes.
    /// </summary>
    public static class PdfIntegrityHelper
    {
        /// <summary>
        /// Validates a PDF file for corrupted objects after annotation changes.
        /// The method loads the PDF, binds it to a PdfAnnotationEditor (facade),
        /// then runs Document.Validate which writes a log file and returns the validation result.
        /// </summary>
        /// <param name="inputPdfPath">Path to the PDF to be validated.</param>
        /// <param name="logFilePath">Path where the validation log will be saved.</param>
        /// <returns>True if the document passes validation; otherwise false.</returns>
        public static bool ValidatePdfIntegrity(string inputPdfPath, string logFilePath)
        {
            if (!File.Exists(inputPdfPath))
                throw new FileNotFoundException($"Input PDF not found: {inputPdfPath}");

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdfPath))
            {
                // Bind the document to the PdfAnnotationEditor facade.
                // This allows annotation modifications to be performed elsewhere;
                // here we just ensure the facade is initialized.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(doc))
                {
                    // No additional annotation work is required for validation.
                    // The editor will be disposed automatically at the end of this block.
                }

                // Validate the document. The log file will contain details about any corrupted objects.
                // PdfFormat.PDF_A_1B is used as a generic format for validation; any PdfFormat value is acceptable.
                bool isValid = doc.Validate(logFilePath, PdfFormat.PDF_A_1B);
                return isValid;
            }
        }
    }

    /// <summary>
    /// Simple console entry point required for compilation as an executable.
    /// Demonstrates how to call <see cref="PdfIntegrityHelper.ValidatePdfIntegrity"/>.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: the PDF to validate and the path for the validation log.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfIntegrity <inputPdfPath> <logFilePath>");
                return;
            }

            string inputPdfPath = args[0];
            string logFilePath = args[1];

            try
            {
                bool isValid = PdfIntegrityHelper.ValidatePdfIntegrity(inputPdfPath, logFilePath);
                Console.WriteLine($"PDF validation result: {(isValid ? "Valid" : "Invalid")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during validation: {ex.Message}");
            }
        }
    }
}
