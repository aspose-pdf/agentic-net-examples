using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    // Entry point
    static void Main(string[] args)
    {
        // Expected arguments:
        // args[0] - source PDF file path
        // args[1] - FDF file to import data from
        // args[2] - PDF file path after import (output)
        // args[3] - FDF file path to export data to
        if (args.Length < 4)
        {
            Console.WriteLine("Usage: <sourcePdf> <importFdf> <outputPdf> <exportFdf>");
            return;
        }

        string sourcePdfPath = args[0];
        string importFdfPath = args[1];
        string outputPdfPath = args[2];
        string exportFdfPath = args[3];

        // Validate input files
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        if (!File.Exists(importFdfPath))
        {
            Console.Error.WriteLine($"Import FDF not found: {importFdfPath}");
            return;
        }

        try
        {
            // -------------------------------------------------
            // Load the PDF document
            // -------------------------------------------------
            Document pdfDoc = new Document(sourcePdfPath);

            // -------------------------------------------------
            // Import data from FDF into the PDF's AcroForm
            // -------------------------------------------------
            using (FileStream fdfStream = File.OpenRead(importFdfPath))
            {
                // The static method reads annotations (including form field values) from the FDF stream
                FdfReader.ReadAnnotations(fdfStream, pdfDoc);
            }

            // -------------------------------------------------
            // Save the PDF after import
            // -------------------------------------------------
            pdfDoc.Save(outputPdfPath); // document-save rule

            // -------------------------------------------------
            // Export current form field values to a simple FDF file
            // -------------------------------------------------
            ExportFormDataToFdf(pdfDoc, exportFdfPath);

            Console.WriteLine("Import and export completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to write form field values to an FDF file.
    // This creates a minimal FDF structure: header, field entries, and trailer.
    static void ExportFormDataToFdf(Document pdfDoc, string fdfPath)
    {
        // Ensure the document actually contains a form
        if (pdfDoc.Form == null || pdfDoc.Form.Count == 0)
        {
            Console.WriteLine("No AcroForm fields found; empty FDF will be created.");
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(fdfPath, false))
            {
                // FDF header
                writer.WriteLine("%FDF-1.2");
                writer.WriteLine("%âãÏÓ"); // binary comment as per spec
                writer.WriteLine("1 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/FDF");
                writer.WriteLine("<<");
                writer.WriteLine("/Fields [");

                // Iterate over each field and write its value
                foreach (Field field in pdfDoc.Form)
                {
                    // Field name (FullName is preferred, fallback to Name)
                    string fieldName = field?.FullName ?? field?.Name ?? string.Empty;
                    // Field value – may be null for empty fields
                    string fieldValue = field?.Value?.ToString() ?? string.Empty;

                    writer.WriteLine("<<");
                    writer.WriteLine($"/T ({EscapeString(fieldName)})");
                    writer.WriteLine($"/V ({EscapeString(fieldValue)})");
                    writer.WriteLine(">>");
                }

                // Close the Fields array and the dictionaries
                writer.WriteLine("]");
                writer.WriteLine(">>");
                writer.WriteLine(">>");
                writer.WriteLine("endobj");
                writer.WriteLine("trailer");
                writer.WriteLine("<< /Root 1 0 R >>");
                writer.WriteLine("%%EOF");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write FDF file '{fdfPath}': {ex.Message}");
            throw; // re‑throw to let the caller handle if needed
        }
    }

    // Escape parentheses and backslashes in strings for FDF syntax
    static string EscapeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return input.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
    }
}
