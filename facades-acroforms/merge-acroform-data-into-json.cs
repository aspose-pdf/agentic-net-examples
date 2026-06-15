using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF files containing AcroForm fields
        string[] pdfFiles = { "form1.pdf", "form2.pdf", "form3.pdf" };
        // Output JSON file that will contain merged form data
        const string outputJsonPath = "mergedFormData.json";

        // List to hold each PDF's form data as a dictionary
        List<Dictionary<string, object>> mergedData = new List<Dictionary<string, object>>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(pdfPath))
            {
                // Initialize the Form facade with the loaded document
                Form formFacade = new Form(doc);

                // Export form fields to a memory stream in JSON format
                using (MemoryStream jsonStream = new MemoryStream())
                {
                    // The second argument (false) indicates that empty fields are not included
                    formFacade.ExportJson(jsonStream, false);
                    jsonStream.Position = 0;

                    // Read the JSON string from the stream
                    using (StreamReader reader = new StreamReader(jsonStream))
                    {
                        string json = reader.ReadToEnd();

                        // Deserialize the JSON into a dictionary for easy aggregation
                        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                        if (dict != null)
                        {
                            mergedData.Add(dict);
                        }
                    }
                }
            }
        }

        // Serialize the aggregated list of dictionaries into a single JSON array
        string finalJson = JsonSerializer.Serialize(mergedData, new JsonSerializerOptions { WriteIndented = true });

        // Write the merged JSON to the output file
        File.WriteAllText(outputJsonPath, finalJson);

        Console.WriteLine($"Merged form data saved to '{outputJsonPath}'.");
    }
}