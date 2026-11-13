using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormUtility
{
    /// <summary>
    /// Extracts all AcroForm field names and their values from a PDF.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>Dictionary where key = field name (or full hierarchical name) and value = field value as string.</returns>
    public static Dictionary<string, string> ExtractPdfFormFields(string pdfPath)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be a non‑empty string.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}");

        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Document must be disposed deterministically
        using (Document doc = new Document(pdfPath))
        {
            var form = doc.Form;
            // If there is no form or no fields, return the empty dictionary
            if (form?.Fields == null || form.Fields.Length == 0)
                return fields;

            // Iterate over all fields in the form
            foreach (Field field in form.Fields)
            {
                // Prefer the hierarchical full name; fall back to simple name
                string fieldName = !string.IsNullOrEmpty(field.FullName) ? field.FullName : field.Name;

                // Field.Value can be null; convert to string safely
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                // Add or update the entry in the dictionary
                fields[fieldName] = fieldValue;
            }
        }

        return fields;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Simple demonstration: print all form fields of the supplied PDF.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: AsposePdfApi <pdfPath>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            var result = PdfFormUtility.ExtractPdfFormFields(pdfPath);
            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
