using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Stub definitions for missing Aspose.Pdf.Facades types (remove when the real library is referenced)
namespace Aspose.Pdf.Facades
{
    // Minimal stub for PdfFormEditor – provides only the members used in this sample.
    // In a real project, reference the Aspose.Pdf.Facades NuGet package instead of this stub.
    public class PdfFormEditor : IDisposable
    {
        private string _boundPdfPath;

        public void BindPdf(string pdfPath)
        {
            // In the real implementation this would load the PDF for editing.
            // Here we just store the path for demonstration purposes.
            _boundPdfPath = pdfPath;
        }

        public void AddJavaScript(string jsCode)
        {
            // Real implementation would embed the JavaScript at the document level.
            // This stub does nothing.
        }

        public void Save(string outputPath)
        {
            // Real implementation would write the modified PDF to disk.
            // For the stub we simply copy the original file if it exists.
            if (!string.IsNullOrEmpty(_boundPdfPath) && File.Exists(_boundPdfPath))
            {
                File.Copy(_boundPdfPath, outputPath, overwrite: true);
            }
        }

        public void Dispose()
        {
            // No resources to release in the stub.
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript that shows a confirmation dialog before submitting the form.
        // app.alert returns 4 for "Yes" when using a 3‑button dialog.
        string js = @"
if (app.alert('Are you sure you want to submit the form?', 3) == 4) {
    this.submitForm();
}
";

        try
        {
            // Bind the existing PDF to the FormEditor.
            using PdfFormEditor formEditor = new PdfFormEditor();
            formEditor.BindPdf(inputPath);

            // Add the JavaScript at the document level.
            formEditor.AddJavaScript(js);

            // Save the modified PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"PDF saved with JavaScript action: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
