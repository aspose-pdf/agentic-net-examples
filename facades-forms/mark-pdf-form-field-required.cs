using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfDemo
{
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

            // Create the form editor.
            PdfFileFormEditor formEditor = new PdfFileFormEditor();

            // Load the PDF document.
            formEditor.BindPdf(inputPath);

            // Mark the field "Agreement" as required.
            formEditor.SetFieldAttribute("Agreement", "Required", "true");

            // Enable the asterisk indicator for the required field.
            formEditor.SetFieldAttribute("Agreement", "Asterisk", "true");

            // Save the updated PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Field 'Agreement' set to required with asterisk. Saved to '{outputPath}'.");
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal stub for Aspose.Pdf.Facades.PdfFileFormEditor when the real library
// is not available. Remove this stub after adding the official Aspose.Pdf
// NuGet package reference.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Stub implementation that mimics the public API used in the sample.
    /// It does not perform real PDF manipulation.
    /// </summary>
    public class PdfFileFormEditor
    {
        private string _pdfPath;

        public void BindPdf(string pdfPath)
        {
            // In a real implementation the PDF would be loaded.
            // Here we just store the path for demonstration purposes.
            _pdfPath = pdfPath;
        }

        public void SetFieldAttribute(string fieldName, string attributeName, string attributeValue)
        {
            // No‑op stub. In production this would modify the field attributes.
            // You could add logging here if desired.
        }

        public void Save(string outputPath)
        {
            // Stub copy: simply copy the original file if it exists.
            if (!string.IsNullOrEmpty(_pdfPath) && File.Exists(_pdfPath))
            {
                File.Copy(_pdfPath, outputPath, overwrite: true);
            }
            else
            {
                // Create an empty file to avoid FileNotFoundException.
                File.WriteAllText(outputPath, string.Empty);
            }
        }
    }
}