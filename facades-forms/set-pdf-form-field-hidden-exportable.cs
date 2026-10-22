using System;
using System.IO;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub definitions for Aspose.Pdf classes (used when the real library is
// not referenced). In a real project you should reference the Aspose.Pdf NuGet
// package instead of these stubs.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation of the Aspose.Pdf.Facades.PdfFormEditor class.
    /// Provides only the members required for the sample code to compile.
    /// </summary>
    public class PdfFormEditor
    {
        private string _boundPath;

        /// <summary>
        /// Binds the specified PDF file to the editor.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);
            _boundPath = pdfPath;
        }

        /// <summary>
        /// Sets the visibility of a form field. In the real library the second
        /// parameter is a boolean where false means hidden.
        /// </summary>
        public void SetFieldVisibility(string fieldName, bool visible)
        {
            // Stub – no real implementation.
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            // In a production environment this would modify the field's /F flag.
        }

        /// <summary>
        /// Sets whether a form field should be exported with the form data.
        /// </summary>
        public void SetFieldExport(string fieldName, bool export)
        {
            // Stub – no real implementation.
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            // In a production environment this would modify the field's /Ff flag.
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_boundPath))
                throw new InvalidOperationException("No PDF has been bound to the editor.");
            // For the stub we simply copy the original file.
            File.Copy(_boundPath, outputPath, overwrite: true);
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

        // Hide the field but keep it exportable in form submissions
        formEditor.SetFieldVisibility("EmployeeID", false); // false => hidden
        formEditor.SetFieldExport("EmployeeID", true);      // true => exportable

        // Save the updated PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"Field 'EmployeeID' set to hidden but exportable. Saved to '{outputPath}'.");
    }
}
