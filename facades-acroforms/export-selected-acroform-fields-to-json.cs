using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class ExportSelectedFormFieldsToJson
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";      // source PDF with form fields
        const string outputJsonPath = "selected_fields.json"; // result JSON file

        // List of fully‑qualified field names that should be exported
        string[] fieldsToExport = { "FirstName", "LastName", "Email" };

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect JSON fragments for each requested field
            List<string> fieldJsonFragments = new List<string>();

            foreach (string fieldName in fieldsToExport)
            {
                // Check whether the form contains the field
                if (pdfDoc.Form.HasField(fieldName))
                {
                    // Retrieve the widget annotation representing the field
                    WidgetAnnotation widget = pdfDoc.Form[fieldName];

                    // Export the single field to a temporary memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        widget.ExportToJson(ms);
                        string json = Encoding.UTF8.GetString(ms.ToArray());

                        // The ExportToJson method returns a JSON object for the field.
                        // Store it for later aggregation.
                        fieldJsonFragments.Add(json);
                    }
                }
                else
                {
                    Console.WriteLine($"Field not found: {fieldName}");
                }
            }

            // Combine individual field objects into a single JSON array
            string combinedJson = "[" + string.Join(",", fieldJsonFragments) + "]";

            // Write the combined JSON to the output file
            File.WriteAllText(outputJsonPath, combinedJson, Encoding.UTF8);
        }

        Console.WriteLine($"Selected form fields exported to '{outputJsonPath}'.");
    }
}