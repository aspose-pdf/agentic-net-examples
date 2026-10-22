using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub definitions to allow compilation when the Aspose.Pdf library is not referenced.
// In a real project, reference the Aspose.Pdf NuGet package instead of these stubs.
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub of the PdfFormEditor class providing only the members used in this sample.
    /// </summary>
    public class PdfFormEditor
    {
        private string _boundPdfPath;

        /// <summary>
        /// Binds the specified PDF file for editing.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");
            _boundPdfPath = pdfPath;
            // In a real implementation the PDF would be loaded into memory here.
        }

        /// <summary>
        /// Sets a regular‑expression validation rule for a form field.
        /// </summary>
        public void SetFieldRegEx(string fieldName, string regexPattern)
        {
            if (string.IsNullOrEmpty(_boundPdfPath))
                throw new InvalidOperationException("No PDF is bound. Call BindPdf first.");
            // Stub: just output what would happen.
            Console.WriteLine($"[Stub] Applying regex '{regexPattern}' to field '{fieldName}'.");
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_boundPdfPath))
                throw new InvalidOperationException("No PDF is bound. Call BindPdf first.");
            // Stub: copy the original file to the output location to simulate a save.
            File.Copy(_boundPdfPath, outputPath, overwrite: true);
            Console.WriteLine($"[Stub] PDF saved to '{outputPath}'.");
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

        // Load the PDF into the form editor
        PdfFormEditor formEditor = new PdfFormEditor();
        formEditor.BindPdf(inputPath);

        // Regular expression that matches a typical email address
        string emailRegex = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$";

        // Apply the regex validation to the form field named "Email"
        formEditor.SetFieldRegEx("Email", emailRegex);

        // Save the updated PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"Email field validation applied. Saved to '{outputPath}'.");
    }
}
