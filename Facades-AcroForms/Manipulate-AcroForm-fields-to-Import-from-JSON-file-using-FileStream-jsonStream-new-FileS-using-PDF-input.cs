using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Paths for the source PDF, JSON data file and the resulting PDF
        string pdfPath = "input.pdf";
        string jsonPath = "data.json";
        string outputPath = "output.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }
        if (!File.Exists(jsonPath))
        {
            Console.Error.WriteLine($"JSON file not found: {jsonPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(pdfPath);

            // Open the JSON file as a stream and deserialize it into a dictionary
            using (FileStream jsonStream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
            {
                var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                Dictionary<string, string>? fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonStream, jsonOptions);

                if (fieldValues != null)
                {
                    // Iterate over each key/value pair and assign the value to the matching form field
                    foreach (var kvp in fieldValues)
                    {
                        string fieldName = kvp.Key;
                        string fieldValue = kvp.Value ?? string.Empty;

                        try
                        {
                            // Retrieve the field from the PDF form; the indexer returns a WidgetAnnotation,
                            // which must be cast to Aspose.Pdf.Forms.Field before accessing the Value property.
                            Field? field = pdfDocument.Form[fieldName] as Field;
                            if (field != null)
                            {
                                field.Value = fieldValue;
                            }
                            else
                            {
                                Console.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                            }
                        }
                        catch (KeyNotFoundException)
                        {
                            // Field name not present in the PDF; log and continue
                            Console.WriteLine($"Field '{fieldName}' not found in the PDF form.");
                        }
                    }
                }
            }

            // Save the modified PDF document
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved with imported data to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
