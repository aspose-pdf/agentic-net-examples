using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public static class PdfFormUtility
{
    // Returns a dictionary mapping each form field's full name to its string value.
    public static Dictionary<string, string> GetFormFields(string pdfPath)
    {
        if (string.IsNullOrEmpty(pdfPath))
            throw new ArgumentException("PDF path must be provided.", nameof(pdfPath));

        if (!File.Exists(pdfPath))
            throw new FileNotFoundException("PDF file not found.", pdfPath);

        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Ensure deterministic disposal of the Document.
        using (Document doc = new Document(pdfPath))
        {
            // Iterate over all fields in the PDF form.
            foreach (Field field in doc.Form.Fields)
            {
                // Convert the field value to string; treat null as empty.
                string value = field.Value?.ToString() ?? string.Empty;
                result[field.FullName] = value;
            }
        }

        return result;
    }
}

// Minimal entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // Optional demonstration (does not affect library functionality).
        // Example: if a PDF path is supplied, print its form fields.
        if (args.Length > 0)
        {
            try
            {
                var fields = PdfFormUtility.GetFormFields(args[0]);
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
        else
        {
            Console.WriteLine("No PDF path supplied. Provide a file path as the first argument to list form fields.");
        }
    }
}