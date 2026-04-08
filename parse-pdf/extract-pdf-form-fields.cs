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
    /// <param name="pdfPath">Path to the PDF document.</param>
    /// <returns>Dictionary of field names and their string values.</returns>
    public static Dictionary<string, string> GetFormFields(string pdfPath)
    {
        // Validate input
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException($"PDF file not found: {pdfPath}", pdfPath);

        // Load the PDF document using Aspose.Pdf's Document class.
        // The using block ensures deterministic disposal of the Document object.
        using (Document doc = new Document(pdfPath))
        {
            // Prepare the result dictionary.
            var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            // Iterate over all fields in the form.
            // doc.Form.Fields implements IEnumerable<Field>.
            foreach (Field field in doc.Form.Fields)
            {
                // FullName uniquely identifies the field within the PDF.
                string name = field.FullName ?? string.Empty;

                // Value may be null; convert to string safely.
                string value = field.Value?.ToString() ?? string.Empty;

                // Add to dictionary (if duplicate names exist, the last one wins).
                fields[name] = value;
            }

            return fields;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: <exe> <pdfPath>");
            return;
        }

        string pdfPath = args[0];
        try
        {
            var formFields = PdfFormUtility.GetFormFields(pdfPath);
            Console.WriteLine("Form fields found:");
            foreach (var kvp in formFields)
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
