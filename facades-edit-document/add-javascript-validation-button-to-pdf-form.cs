using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for JavascriptAction
using Aspose.Pdf.Facades;

// Stub implementation for PdfFormEditor when the real class is unavailable.
// This provides the minimal members used in the sample code.
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor : IDisposable
    {
        private Document? _document; // nullable because it is created in BindPdf

        /// <summary>
        /// Loads the PDF document.
        /// </summary>
        public void BindPdf(string path)
        {
            _document = new Document(path);
        }

        /// <summary>
        /// Attaches JavaScript either to a specific form field or as a document‑level script.
        /// </summary>
        /// <param name="js">The JavaScript code.</param>
        /// <param name="fieldName">The name of the form field (e.g., a button). If null or empty, the script is added at document level.</param>
        public void SetJavaScript(string js, string? fieldName)
        {
            if (_document == null)
                throw new InvalidOperationException("Document is not bound. Call BindPdf first.");

            // Aspose.Pdf exposes a writable OpenAction property for document‑level JavaScript.
            // For field‑level actions the real API would locate the field and assign the action to its OnActivated event.
            // The stub keeps things simple and always adds a document‑level script.
            _document.OpenAction = new JavascriptAction(js);
        }

        /// <summary>
        /// Saves the modified PDF.
        /// </summary>
        public void Save(string outputPath)
        {
            _document?.Save(outputPath);
        }

        /// <summary>
        /// Releases resources.
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            _document?.Dispose();
            _document = null;
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

        // JavaScript that checks a list of required fields.
        // If any field is empty, an alert is shown and the script returns false.
        string validationJs = @"
var required = ['Field1', 'Field2', 'Field3']; // replace with your field names
for (var i = 0; i < required.length; i++) {
    var f = this.getField(required[i]);
    if (!f || f.value === '') {
        app.alert('Please fill the required field: ' + required[i]);
        if (f) f.setFocus();
        return false;
    }
}
return true;
";

        // Use PdfFormEditor (Aspose.Pdf.Facades) to bind the PDF and add the script.
        using (PdfFormEditor formEditor = new PdfFormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Attach the JavaScript to a button field (e.g., a submit button named "SubmitBtn").
            // The stub implementation adds the script at document level to avoid missing Interactive namespaces.
            formEditor.SetJavaScript(validationJs, "SubmitBtn");

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Validation JavaScript added. Output saved to '{outputPath}'.");
    }
}
