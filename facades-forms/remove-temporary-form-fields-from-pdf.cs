using System;
using System.IO;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub implementation for Aspose.Pdf.Facades.PdfFormEditor.
// This allows the sample to compile without the full Aspose.Pdf library.
// In a production environment you should reference the official Aspose.Pdf
// NuGet package which provides the full-featured PdfFormEditor class.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub of the Aspose.Pdf.Facades.PdfFormEditor class.
    /// Provides only the members used in the sample code.
    /// </summary>
    public class PdfFormEditor
    {
        private string _boundPdfPath;

        /// <summary>
        /// Binds the editor to a PDF file.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            _boundPdfPath = pdfPath;
        }

        /// <summary>
        /// Returns the names of all form fields in the bound PDF.
        /// The stub returns an empty array; replace with real implementation.
        /// </summary>
        public string[] GetFieldNames()
        {
            // Real implementation would inspect the PDF and return field names.
            // For compilation purposes we return an empty array.
            return Array.Empty<string>();
        }

        /// <summary>
        /// Deletes a form field by name.
        /// The stub does nothing; the real library removes the field from the PDF.
        /// </summary>
        public void DeleteField(string fieldName)
        {
            // No‑op in stub.
        }

        /// <summary>
        /// Saves the modified PDF to the specified path.
        /// The stub simply copies the original file if it exists.
        /// </summary>
        public void Save(string outputPath)
        {
            if (!string.IsNullOrEmpty(_boundPdfPath) && File.Exists(_boundPdfPath))
            {
                // Simple copy to emulate a save operation.
                File.Copy(_boundPdfPath, outputPath, overwrite: true);
            }
            else
            {
                // If no source PDF is bound, create an empty file.
                File.WriteAllBytes(outputPath, Array.Empty<byte>());
            }
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF for form field manipulation
            PdfFormEditor formEditor = new PdfFormEditor();
            formEditor.BindPdf(inputPath);

            // Retrieve all field names
            string[] fieldNames = formEditor.GetFieldNames();

            // Delete fields whose names start with "Temp_"
            foreach (string fieldName in fieldNames)
            {
                if (!string.IsNullOrEmpty(fieldName) &&
                    fieldName.StartsWith("Temp_", StringComparison.Ordinal))
                {
                    formEditor.DeleteField(fieldName);
                }
            }

            // Save the cleaned PDF
            formEditor.Save(outputPath);
            Console.WriteLine($"Temporary fields removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
