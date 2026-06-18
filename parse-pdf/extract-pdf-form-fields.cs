using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormUtility
{
    /// <summary>
    /// Loads a PDF document and extracts all form field names with their current values.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF file.</param>
    /// <returns>Dictionary where the key is the field's full name and the value is its string representation.</returns>
    public static Dictionary<string, string> GetFormFields(string pdfPath)
    {
        // Ensure the file exists before attempting to open it.
        if (string.IsNullOrEmpty(pdfPath) || !File.Exists(pdfPath))
            throw new ArgumentException($"PDF file not found: {pdfPath}");

        // Use a using block for deterministic disposal of the Document (lifecycle rule).
        using (Document doc = new Document(pdfPath))
        {
            var fieldsDictionary = new Dictionary<string, string>();

            // Iterate over all fields in the form.
            foreach (Field field in doc.Form.Fields)
            {
                // FullName uniquely identifies the field.
                string fieldName = field.FullName;

                // Value may be null; convert to string safely.
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                // Add to the dictionary (duplicate names are unlikely but handled).
                fieldsDictionary[fieldName] = fieldValue;
            }

            return fieldsDictionary;
        }
    }
}

public class Program
{
    /// <summary>
    /// Simple console entry point to demonstrate the utility.
    /// </summary>
    /// <param name="args">First argument should be the PDF file path.</param>
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <executable> <pdfPath>");
            return;
        }

        try
        {
            var fields = PdfFormUtility.GetFormFields(args[0]);
            Console.WriteLine($"Found {fields.Count} form field(s):");
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
