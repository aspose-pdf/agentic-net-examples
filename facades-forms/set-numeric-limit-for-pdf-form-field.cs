using System;
using System.IO;
using Aspose.Pdf.Facades;

// Stub definitions for missing Aspose.Pdf classes (only needed if the actual library is not referenced).
// In a real project, reference the official Aspose.Pdf NuGet package instead of using these stubs.
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub for Aspose.Pdf.Facades.PdfFormEditor to allow compilation when the real library is unavailable.
    /// </summary>
    public class PdfFormEditor : IDisposable
    {
        /// <summary>
        /// Binds the specified PDF file for editing.
        /// </summary>
        public void BindPdf(string pdfPath)
        {
            // In the real implementation this loads the PDF document.
            // Stub does nothing.
        }

        /// <summary>
        /// Sets a numeric limit (minimum and maximum) for a form field.
        /// </summary>
        public void SetFieldLimit(string fieldName, int min, int max)
        {
            // Real implementation would enforce the range on the field.
            // Stub does nothing.
        }

        /// <summary>
        /// Saves the edited PDF to the specified path.
        /// </summary>
        public void Save(string outputPath)
        {
            // Real implementation would write the modified PDF.
            // Stub does nothing.
        }

        public void Dispose()
        {
            // Clean‑up resources if needed.
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

        // Use PdfFormEditor to set a numeric limit on the "Quantity" field
        using (PdfFormEditor formEditor = new PdfFormEditor())
        {
            // Load the PDF document
            formEditor.BindPdf(inputPath);

            // Define the allowed range: 1 to 100
            formEditor.SetFieldLimit("Quantity", 1, 100);

            // Save the updated PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Quantity field limited to 1‑100 and saved as '{outputPath}'.");
    }
}
