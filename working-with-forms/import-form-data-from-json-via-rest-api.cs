using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfTemplatePath = "template.pdf";
        const string outputPath = "filled.pdf";
        const string apiUrl = "https://example.com/api/formdata";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"Template not found: {pdfTemplatePath}");
            return;
        }

        // Retrieve JSON data from the REST API
        string jsonData;
        using (HttpClient client = new HttpClient())
        {
            try
            {
                jsonData = client.GetStringAsync(apiUrl).Result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error fetching JSON: {ex.Message}");
                return;
            }
        }

        // Load the PDF, populate the form fields from JSON, and save the result
        using (Document doc = new Document(pdfTemplatePath))
        {
            // Verify that the PDF contains a form
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.Error.WriteLine("No form fields found in the PDF.");
                return;
            }

            // Enable automatic recalculation (if the form contains JavaScript calculations)
            doc.Form.AutoRecalculate = true;

            // Parse the JSON and assign values to matching form fields
            try
            {
                using (JsonDocument jsonDoc = JsonDocument.Parse(jsonData))
                {
                    if (jsonDoc.RootElement.ValueKind == JsonValueKind.Object)
                    {
                        foreach (JsonProperty prop in jsonDoc.RootElement.EnumerateObject())
                        {
                            // Find a field whose Name or FullName matches the JSON property name
                            var field = doc.Form.Fields?.FirstOrDefault(f =>
                                string.Equals(f.Name, prop.Name, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(f.FullName, prop.Name, StringComparison.OrdinalIgnoreCase));

                            if (field != null)
                            {
                                // Convert the JSON value to a string representation suitable for the field
                                string value = prop.Value.ValueKind switch
                                {
                                    JsonValueKind.String => prop.Value.GetString(),
                                    JsonValueKind.Number => prop.Value.GetRawText(),
                                    JsonValueKind.True => "True",
                                    JsonValueKind.False => "False",
                                    _ => prop.Value.GetRawText()
                                };

                                field.Value = value;
                            }
                        }
                    }
                }
            }
            catch (JsonException je)
            {
                Console.Error.WriteLine($"Failed to parse JSON data: {je.Message}");
                return;
            }

            // Save the populated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Form populated and saved to '{outputPath}'.");
    }
}
