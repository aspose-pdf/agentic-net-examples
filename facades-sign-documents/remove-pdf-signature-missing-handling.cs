using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub for PdfSignatureFacade (Aspose.Pdf.Facades) when the real class
// is not available in the referenced Aspose.Pdf version. The stub provides the
// subset of functionality required by the sample: loading a PDF, removing a
// signature field, and saving the document.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// A lightweight replacement for the missing Aspose.Pdf.Facades.PdfSignatureFacade.
    /// It uses Aspose.Pdf.Document internally to manipulate signature fields.
    /// </summary>
    public class PdfSignatureFacade
    {
        // The underlying Aspose.Pdf Document. It is nullable because it is only
        // instantiated after BindPdf is called.
        private Document? _document;

        /// <summary>
        /// Loads the PDF file that will be processed.
        /// </summary>
        /// <param name="pdfPath">Path to the input PDF.</param>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));

            _document = new Document(pdfPath);
        }

        /// <summary>
        /// Removes a signature field with the specified name.
        /// Throws <see cref="PdfException"/> if the field does not exist or is not a signature field.
        /// </summary>
        /// <param name="fieldName">The name of the signature field to remove.</param>
        public void RemoveSignature(string fieldName)
        {
            if (_document == null)
                throw new InvalidOperationException("PDF not bound. Call BindPdf first.");

            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));

            // Ensure the document contains a form.
            if (_document.Form == null || _document.Form.Fields == null || !_document.Form.Fields.Any())
                throw new PdfException($"No form fields present in the document; cannot find field '{fieldName}'.");

            // Find the field by name and verify it is a SignatureField.
            var field = _document.Form.Fields.FirstOrDefault(f => f.Name == fieldName) as SignatureField;
            if (field == null)
                throw new PdfException($"Signature field '{fieldName}' not found.");

            // Remove the field using the Form.Delete method (the correct API).
            _document.Form.Delete(fieldName);
        }

        /// <summary>
        /// Saves the modified PDF to the specified path.
        /// </summary>
        /// <param name="outputPath">Path where the PDF will be saved.</param>
        public void Save(string outputPath)
        {
            if (_document == null)
                throw new InvalidOperationException("PDF not bound. Call BindPdf first.");

            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            _document.Save(outputPath);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "unsigned.pdf";
        const string signatureFieldName = "Signature1";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // PdfSignatureFacade does not implement IDisposable, so no using block is required.
        var signatureFacade = new PdfSignatureFacade();

        // Load the PDF document into the facade.
        signatureFacade.BindPdf(inputPath);

        try
        {
            // Attempt to remove the specified signature field.
            // If the field does not exist, the stub throws a PdfException.
            signatureFacade.RemoveSignature(signatureFieldName);
            Console.WriteLine($"Signature field '{signatureFieldName}' removed successfully.");
        }
        catch (PdfException ex)
        {
            // Handle the case where the signature field is missing.
            Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.Error.WriteLine($"Error while removing signature: {ex.Message}");
        }

        // Save the modified PDF.
        signatureFacade.Save(outputPath);
        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}
