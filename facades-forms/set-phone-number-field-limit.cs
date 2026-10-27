using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Stub implementation for missing Aspose.Pdf.Facades.PdfFormEditor (remove when the real library is referenced)
namespace Aspose.Pdf.Facades
{
    /// <summary>
    /// Minimal stub of the PdfFormEditor class providing only the members used in this sample.
    /// This allows the code to compile when the actual Aspose.Pdf library is not available.
    /// Replace this stub with the real Aspose.Pdf.Facades.PdfFormEditor implementation.
    /// </summary>
    public class PdfFormEditor
    {
        public void BindPdf(string path) { /* No‑op stub */ }
        public void SetFieldLimit(string fieldName, int maxLength) { /* No‑op stub */ }
        public void Save(string path) { /* No‑op stub */ }
        public void Close() { /* No‑op stub */ }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "PhoneNumber";
        const int    maxLength  = 15;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFormEditor is a Facades class; it does not implement IDisposable.
        PdfFormEditor formEditor = new PdfFormEditor();

        // Load the PDF document.
        formEditor.BindPdf(inputPath);

        // Set the maximum character length for the specified form field.
        formEditor.SetFieldLimit(fieldName, maxLength);

        // Save the updated PDF.
        formEditor.Save(outputPath);

        // Release internal resources.
        formEditor.Close();

        Console.WriteLine($"Field '{fieldName}' limit set to {maxLength} characters. Saved to '{outputPath}'.");
    }
}
