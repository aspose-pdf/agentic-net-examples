using System;
using System.IO;
using System.Linq;
using Aspose.Pdf.Facades;

// Stub implementation for PdfFormEditor when the Aspose.Pdf library is not referenced.
// This allows the code to compile and run for demonstration purposes.
// In a real project, reference the official Aspose.Pdf NuGet package instead of this stub.
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private string _pdfPath;

        /// <summary>
        /// Binds the specified PDF file to the editor.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("PDF path cannot be null or empty.", nameof(pdfPath));
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);
            _pdfPath = pdfPath;
        }

        /// <summary>
        /// Retrieves the list of options for a radio‑button field.
        /// This stub returns an empty array; replace with real implementation when using Aspose.Pdf.
        /// </summary>
        public string[] GetRadioButtonOptions(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("Field name cannot be null or empty.", nameof(fieldName));
            // Real implementation would inspect the PDF form fields and return the actual options.
            // For the stub we simply return an empty array to keep the example compilable.
            return Array.Empty<string>();
        }
    }
}

class Program
{
    static void Main()
    {
        const string pdfPath = "form.pdf";               // input PDF containing the form
        const string radioFieldName = "Gender";          // name of the radio button field to check

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Expected set of options for the radio button field
        string[] expectedOptions = new[] { "Male", "Female", "Other" };

        // Bind the PDF to the form editor (no using needed – PdfFormEditor is not a Document)
        PdfFormEditor formEditor = new PdfFormEditor();
        formEditor.BindPdf(pdfPath);

        // Retrieve the actual options defined for the radio button field
        string[] actualOptions = formEditor.GetRadioButtonOptions(radioFieldName);

        // Compare the two sets (order‑independent)
        bool optionsMatch = expectedOptions.Length == actualOptions.Length &&
                            expectedOptions.OrderBy(o => o)
                                           .SequenceEqual(actualOptions.OrderBy(o => o));

        if (optionsMatch)
        {
            Console.WriteLine("Radio button options match the expected set.");
        }
        else
        {
            Console.WriteLine("Radio button options do NOT match the expected set.");
            Console.WriteLine($"Expected: [{string.Join(", ", expectedOptions)}]");
            Console.WriteLine($"Actual  : [{string.Join(", ", actualOptions)}]");
        }

        // No explicit disposal required for PdfFormEditor; it will be reclaimed by GC.
    }
}
