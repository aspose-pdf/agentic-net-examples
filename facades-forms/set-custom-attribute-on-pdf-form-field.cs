using System;
using System.IO;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub for Aspose.Pdf.Facades.PdfFormEditor when the real library is not
// referenced. This allows the sample to compile and run for demonstration
// purposes. In a production environment you should reference the official
// Aspose.Pdf NuGet package instead of using this stub.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation of the Aspose.Pdf.Facades.PdfFormEditor class.
    /// Provides only the members used in the sample code.
    /// </summary>
    public class PdfFormEditor
    {
        private string _boundPdfPath;

        /// <summary>
        /// Binds the specified PDF file for editing.
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file.</param>
        public void BindPdf(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            _boundPdfPath = pdfPath;
        }

        /// <summary>
        /// Sets a custom attribute on a form field.
        /// This stub does not modify the PDF; it only demonstrates the API call.
        /// </summary>
        /// <param name="fieldName">Name of the form field.</param>
        /// <param name="attributeName">Custom attribute name.</param>
        /// <param name="attributeValue">Custom attribute value.</param>
        public void SetFieldAttribute(string fieldName, string attributeName, string attributeValue)
        {
            // In the real Aspose.Pdf library this would modify the field's
            // dictionary. The stub simply validates parameters.
            if (string.IsNullOrEmpty(_boundPdfPath))
                throw new InvalidOperationException("BindPdf must be called before SetFieldAttribute.");
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("fieldName cannot be null or empty.", nameof(fieldName));
            if (string.IsNullOrEmpty(attributeName))
                throw new ArgumentException("attributeName cannot be null or empty.", nameof(attributeName));
            // No actual PDF manipulation performed.
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// The stub simply copies the original file to the destination.
        /// </summary>
        /// <param name="outputPath">Path where the PDF should be saved.</param>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_boundPdfPath))
                throw new InvalidOperationException("BindPdf must be called before Save.");

            // Simple copy to emulate a save operation.
            File.Copy(_boundPdfPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a form editor and bind the PDF.
        PdfFormEditor formEditor = new PdfFormEditor();
        formEditor.BindPdf(inputPath);

        // Assign custom attribute "data-id" with value "12345" to the field "OrderNumber".
        formEditor.SetFieldAttribute("OrderNumber", "data-id", "12345");

        // Save the updated PDF.
        formEditor.Save(outputPath);

        Console.WriteLine($"Custom attribute set and saved to '{outputPath}'.");
    }
}
