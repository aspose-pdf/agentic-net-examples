using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfFormUtilityDemo
{
    public static class PdfFormUtility
    {
        /// <summary>
        /// Reads all form fields from a PDF and returns a dictionary where the key is the field's full name
        /// and the value is the field's current value (as a string).
        /// </summary>
        /// <param name="pdfPath">Path to the PDF file.</param>
        /// <returns>Dictionary of field names and their values.</returns>
        public static Dictionary<string, string> GetFormFields(string pdfPath)
        {
            // Ensure the file exists before attempting to open it.
            if (!File.Exists(pdfPath))
                throw new FileNotFoundException($"PDF file not found: {pdfPath}");

            // Load the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(pdfPath))
            {
                var fields = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                // Iterate over all fields in the form.
                foreach (Field field in doc.Form.Fields)
                {
                    // FullName uniquely identifies the field; Value may be null.
                    string name = field.FullName;
                    string value = field.Value?.ToString() ?? string.Empty;

                    fields[name] = value;
                }

                return fields;
            }
        }
    }

    // Minimal entry point required for a console‑type project.
    // The method does not perform any work; it simply satisfies the compiler.
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage (can be removed in production):
            // if (args.Length > 0)
            // {
            //     var result = PdfFormUtility.GetFormFields(args[0]);
            //     foreach (var kvp in result)
            //         Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            // }
        }
    }
}
