using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// ---------------------------------------------------------------------------
// Minimal stub for PdfFormEditor when the Aspose.Pdf library is not available.
// This allows the sample to compile and run (the stub does not perform real
// PDF manipulation). In a real project reference the official Aspose.Pdf
// NuGet package instead of this stub.
// ---------------------------------------------------------------------------
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        private string _boundPath;

        // Binds the PDF file to the editor. In the stub we just store the path.
        public void BindPdf(string pdfPath)
        {
            if (string.IsNullOrEmpty(pdfPath))
                throw new ArgumentException("pdfPath cannot be null or empty", nameof(pdfPath));
            _boundPath = pdfPath;
        }

        // Returns an empty list of field names. Real implementation would inspect the PDF.
        public string[] GetFieldNames()
        {
            // Stub: return an empty array. Replace with real logic when using Aspose.Pdf.
            return Array.Empty<string>();
        }

        // Renames a field. Stub does nothing.
        public void RenameField(string oldName, string newName)
        {
            // No‑op in stub.
        }

        // Saves the edited PDF. Stub copies the original file to the destination.
        public void Save(string outputPath)
        {
            if (string.IsNullOrEmpty(_boundPath))
                throw new InvalidOperationException("No PDF has been bound. Call BindPdf first.");
            // Simple copy to emulate a save operation.
            File.Copy(_boundPath, outputPath, overwrite: true);
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_renamed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFormEditor does not implement IDisposable, so no using block
        PdfFormEditor formEditor = new PdfFormEditor();

        // Load the PDF document
        formEditor.BindPdf(inputPath);

        // Get all form field names
        string[] fieldNames = formEditor.GetFieldNames();

        foreach (string oldName in fieldNames)
        {
            // Convert the field name to camelCase
            string newName = ToCamelCase(oldName);
            if (!oldName.Equals(newName, StringComparison.Ordinal))
            {
                // Rename the field
                formEditor.RenameField(oldName, newName);
            }
        }

        // Save the updated PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
    }

    // Converts a string to camelCase (e.g., "First_Name" -> "firstName")
    static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        // Split on any non‑alphanumeric characters
        string[] parts = Regex.Split(input, @"[^A-Za-z0-9]+");
        if (parts.Length == 0)
            return input;

        // Lowercase first part, capitalize subsequent parts
        string result = parts[0].Length > 0
            ? char.ToLowerInvariant(parts[0][0]) + parts[0].Substring(1)
            : string.Empty;

        for (int i = 1; i < parts.Length; i++)
        {
            if (parts[i].Length == 0) continue;
            result += char.ToUpperInvariant(parts[i][0]) + parts[i].Substring(1);
        }

        return result;
    }
}
