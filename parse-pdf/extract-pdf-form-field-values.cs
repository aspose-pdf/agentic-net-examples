using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormUtility
{
    /// <summary>
    /// Reads all form fields from a PDF file and returns a dictionary where the key is the field's full name
    /// and the value is the field's current value as a string.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF document.</param>
    /// <returns>Dictionary of field names and their string values.</returns>
    public static Dictionary<string, string> GetPdfFormFields(string pdfPath)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        // Result container
        var fieldValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // The Form property gives access to the collection of form fields
            foreach (Field field in doc.Form.Fields)
            {
                // FullName uniquely identifies the field; Value may be null, so handle gracefully
                string name = field.FullName;
                string value = field.Value?.ToString() ?? string.Empty;

                // Add or update the entry in the dictionary
                fieldValues[name] = value;
            }
        }

        return fieldValues;
    }
}

public class Program
{
    // Required entry point for a console‑application build
    public static void Main(string[] args)
    {
        // Simple demo: first argument is the PDF file path
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <executable> <pdfPath>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            var fields = PdfFormUtility.GetPdfFormFields(pdfPath);
            foreach (var kvp in fields)
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