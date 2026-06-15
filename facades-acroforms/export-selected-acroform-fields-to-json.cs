using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class ExportSelectedFormFields
{
    static void Main()
    {
        // Input PDF containing form fields
        const string inputPdfPath = "input.pdf";

        // Output JSON file that will contain only the selected fields
        const string outputJsonPath = "selected_fields.json";

        // List of fully‑qualified field names to export.
        // Use the exact names as they appear in the PDF form.
        var fieldsToExport = new List<string>
        {
            "Customer.Name",
            "Customer.Email",
            "Order.Total"
        };

        // Ensure the source PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (var document = new Document(inputPdfPath))
        {
            // Prepare export options.
            // ExportFieldsToJsonOptions inherits from ExportFieldsOptions,
            // which provides a FieldSelector predicate to filter fields.
            var exportOptions = new ExportFieldsToJsonOptions
            {
                // Optional: make the JSON output indented for readability
                WriteIndented = true,

                // Filter fields based on the list defined above
                FieldSelector = field => fieldsToExport.Contains(field.FullName)
            };

            // Export the selected fields to a JSON file.
            // The overload accepts a file name and the options instance.
            document.Form.ExportToJson(outputJsonPath, exportOptions);
        }

        Console.WriteLine($"Selected form fields have been exported to '{outputJsonPath}'.");
    }
}