using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF file containing AcroForm fields
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Access the AcroForm object
            Form acroForm = doc.Form;

            // If the document has no form or no fields, exit early
            if (acroForm == null || acroForm.Count == 0)
            {
                Console.WriteLine("No AcroForm fields found in the document.");
                return;
            }

            // Optional: store field name/value pairs for later processing
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();

            // Iterate over all fields in the form
            foreach (Field field in acroForm.Fields)
            {
                // Prefer FullName (qualified name); fallback to Name
                string fieldName = field.FullName ?? field.Name ?? "(unnamed)";

                // The Value property holds the field's current value; convert to string safely
                string fieldValue = field.Value?.ToString() ?? string.Empty;

                // Output the field information
                Console.WriteLine($"Field: {fieldName}, Value: {fieldValue}");

                // Store in the dictionary
                fieldValues[fieldName] = fieldValue;
            }

            // At this point 'fieldValues' contains all AcroForm data and can be used
            // for further processing (e.g., exporting to JSON, database insertion, etc.).
        }
    }
}