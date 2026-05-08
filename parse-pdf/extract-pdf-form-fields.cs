using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormUtility
{
    /// <summary>
    /// Reads all form fields from a PDF file and returns a dictionary where the key is the field name
    /// (full qualified name) and the value is the field's current value as a string.
    /// </summary>
    /// <param name="pdfPath">Full path to the PDF document.</param>
    /// <returns>Dictionary of field names and their string values.</returns>
    public static Dictionary<string, string> GetPdfFormFields(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);

        var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Ensure the Document is disposed properly.
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over each field in the form.
            foreach (Field field in doc.Form.Fields)
            {
                // Use FullName if available; otherwise fall back to PartialName.
                string name = field.FullName ?? field.PartialName ?? string.Empty;

                // Convert the field value to string safely (null becomes empty string).
                string value = field.Value?.ToString() ?? string.Empty;

                // Store the name/value pair.
                fields[name] = value;
            }
        }

        return fields;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration – can be removed or left empty.
        // if (args.Length > 0)
        // {
        //     var result = PdfFormUtility.GetPdfFormFields(args[0]);
        //     foreach (var kvp in result)
        //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        // }
    }
}