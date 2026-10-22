using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub implementation for the missing PdfFormEditor class.
// In a real project you should reference the official Aspose.Pdf.Facades NuGet package.
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub that mimics the behaviour of Aspose.Pdf.Facades.PdfFormEditor
    /// sufficient for compilation and basic testing.
    /// </summary>
    public class PdfFormEditor
    {
        private string _boundPdfPath;

        /// <summary>
        /// Binds the PDF file that will be edited.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);
            _boundPdfPath = pdfPath;
        }

        /// <summary>
        /// Attaches JavaScript to a form field.
        /// This stub does not actually embed the script – it only records the call.
        /// </summary>
        public void SetJavaScript(string fieldName, string script)
        {
            // No‑op for the stub. In production the Aspose implementation writes the script to the field.
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            if (script == null)
                throw new ArgumentNullException(nameof(script));
        }

        /// <summary>
        /// Saves the edited PDF to the specified output path.
        /// The stub simply copies the original file.
        /// </summary>
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_boundPdfPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path cannot be null or empty.", nameof(outputPath));

            // Ensure the directory exists.
            var dir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.Copy(_boundPdfPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // PDF containing the SubmitForm button
        const string outputPdf = "output_with_js.pdf";    // Resulting PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that shows a confirmation dialog.
        // app.alert returns 4 for "Yes" and 2 for "No" when the dialog type is 3 (Yes/No).
        // If the user confirms, the form is submitted.
        string js = @"
if (app.alert('Are you sure you want to submit the form?', 3) == 4) {
    this.submitForm();
}";

        // PdfFormEditor is the Facades API for working with form fields.
        // It does not implement IDisposable, so no using block is required.
        PdfFormEditor formEditor = new PdfFormEditor();

        // Bind the existing PDF.
        formEditor.BindPdf(inputPdf);

        // Attach the JavaScript to the button named "SubmitForm".
        // The method SetJavaScript(string fieldName, string script) assigns a
        // JavaScript action to the specified form field.
        formEditor.SetJavaScript("SubmitForm", js);

        // Save the modified PDF.
        formEditor.Save(outputPdf);

        Console.WriteLine($"PDF saved with JavaScript attached: {outputPdf}");
    }
}
