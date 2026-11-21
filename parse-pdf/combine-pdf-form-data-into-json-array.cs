using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the PDF files to process
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file
        const string jsonOutputPath = "combined_form_data.json";

        // List that will hold each PDF's form data as a dictionary
        var allFormsData = new List<Dictionary<string, string>>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load each PDF inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Dictionary to hold field name/value pairs for the current PDF
                var formData = new Dictionary<string, string>();

                // Access the AcroForm attached to the document
                Form form = doc.Form;
                if (form?.Fields != null)
                {
                    // Iterate over all form fields
                    foreach (Field field in form.Fields)
                    {
                        // Field name (PartialName) and its value as string
                        string name = field?.PartialName ?? string.Empty;
                        string value = field?.Value?.ToString() ?? string.Empty;
                        formData[name] = value;
                    }
                }

                // Add the current PDF's form data to the collection
                allFormsData.Add(formData);
            }
        }

        // Serialize the list of dictionaries to a JSON array
        JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(allFormsData, jsonOptions);

        // Write JSON to the output file
        File.WriteAllText(jsonOutputPath, json);

        Console.WriteLine($"Combined form data saved to '{jsonOutputPath}'.");
    }
}
