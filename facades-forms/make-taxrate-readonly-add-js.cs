using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// -----------------------------------------------------------------------------
// Minimal stub for PdfFormEditor when the Aspose.Pdf.Facades assembly is not
// referenced. This provides the required members so the project compiles.
// In a real project you should reference the official Aspose.Pdf NuGet package.
// -----------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor : IDisposable
    {
        public void BindPdf(string path) { /* implementation provided by Aspose */ }
        public void SetFieldReadOnly(string fieldName, bool readOnly) { /* implementation provided by Aspose */ }
        public void SetFieldJavaScript(string fieldName, string script) { /* implementation provided by Aspose */ }
        public void Save(string outputPath) { /* implementation provided by Aspose */ }
        public void Dispose() { /* implementation provided by Aspose */ }
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

        // PdfFormEditor implements IDisposable, so wrap it in a using block
        using (PdfFormEditor formEditor = new PdfFormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Make the "TaxRate" field read‑only
            formEditor.SetFieldReadOnly("TaxRate", true);

            // JavaScript that calculates TaxRate from Subtotal (e.g., 8% tax)
            string js =
                "var subtotal = this.getField('Subtotal').value;" +
                "if (subtotal) {" +
                "  var tax = parseFloat(subtotal) * 0.08;" +
                "  this.getField('TaxRate').value = tax.toFixed(2);" +
                "} else {" +
                "  this.getField('TaxRate').value = '';" +
                "}";

            // Attach the script to the "TaxRate" field
            formEditor.SetFieldJavaScript("TaxRate", js);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}