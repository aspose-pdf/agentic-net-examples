using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where JSON files will be written
        const string outputFolder = "FormJson";

        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each PDF file in the input folder
        foreach (string pdfPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                // Load the PDF inside a using block for deterministic disposal
                using (Document doc = new Document(pdfPath))
                {
                    var formData = new Dictionary<string, object>();

                    // Extract form fields if the document contains a form
                    if (doc.Form != null && doc.Form.Fields != null)
                    {
                        foreach (var field in doc.Form.Fields)
                        {
                            // Retrieve the field's value; some field types may throw, so guard with try/catch
                            object value = null;
                            try
                            {
                                value = field.Value;
                            }
                            catch
                            {
                                // Ignore fields that cannot provide a value
                            }

                            // Use FullName when available, otherwise fall back to Name
                            string key = field.FullName ?? field.Name;
                            formData[key] = value;
                        }
                    }

                    // Serialize the dictionary to formatted JSON
                    string json = JsonSerializer.Serialize(formData, new JsonSerializerOptions { WriteIndented = true });

                    // Determine the output JSON file path
                    string jsonFileName = Path.GetFileNameWithoutExtension(pdfPath) + ".json";
                    string jsonPath = Path.Combine(outputFolder, jsonFileName);

                    // Write JSON to disk
                    File.WriteAllText(jsonPath, json);
                    Console.WriteLine($"Extracted form data to {jsonPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}