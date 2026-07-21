using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfUtilities
{
    /// <summary>
    /// Provides helper methods for working with PDF form fields.
    /// </summary>
    public static class PdfFormUtility
    {
        /// <summary>
        /// Loads a PDF document and extracts all form field names with their current values.
        /// </summary>
        /// <param name="pdfPath">Full path to the PDF file.</param>
        /// <returns>Dictionary where the key is the field's full name and the value is its string representation.</returns>
        public static Dictionary<string, string> GetPdfFormFields(string pdfPath)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(pdfPath) || !File.Exists(pdfPath))
                return result; // Return empty if the file is missing or path is invalid.

            // Load the PDF using a using block to ensure proper disposal (document-disposal-with-using rule).
            using (Document doc = new Document(pdfPath))
            {
                // The Form property gives access to the collection of form fields.
                // Iterate over each field, retrieve its full name and current value.
                foreach (Field field in doc.Form.Fields)
                {
                    // FullName uniquely identifies the field within the PDF.
                    string name = field.FullName ?? string.Empty;

                    // Value may be null; convert safely to string.
                    string value = field.Value?.ToString() ?? string.Empty;

                    // Add to the dictionary; if duplicate names exist, the last one wins.
                    result[name] = value;
                }
            }

            return result;
        }
    }

    // Dummy entry point to satisfy the console‑application requirement.
    internal class Program
    {
        private static void Main(string[] args)
        {
            // No operation – this method exists solely to provide a valid entry point.
        }
    }
}