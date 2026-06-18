using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF form, the JSON data file and the output PDF
        const string inputPdfPath  = "form_template.pdf";
        const string jsonDataPath  = "field_values.json";
        const string outputPdfPath = "filled_form.pdf";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(jsonDataPath))
        {
            Console.Error.WriteLine($"JSON data file not found: {jsonDataPath}");
            return;
        }

        // Load the JSON file into a dictionary of fieldName => fieldValue
        Dictionary<string, string> fieldValues;
        try
        {
            string jsonContent = File.ReadAllText(jsonDataPath);
            fieldValues = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read or parse JSON: {ex.Message}");
            return;
        }

        // Use Aspose.Pdf.Facades.Form to open the PDF and fill fields
        try
        {
            // Form implements IDisposable via SaveableFacade, so we wrap it in a using block
            using (Form form = new Form(inputPdfPath))
            {
                // Iterate over each entry in the JSON dictionary and fill the corresponding field
                foreach (KeyValuePair<string, string> kvp in fieldValues)
                {
                    // FillField returns true if the field was found and filled successfully
                    bool filled = form.FillField(kvp.Key, kvp.Value);
                    if (!filled)
                    {
                        Console.WriteLine($"Warning: Field \"{kvp.Key}\" not found in the PDF form.");
                    }
                }

                // Save the updated PDF to the specified output path
                form.Save(outputPdfPath);
            }

            Console.WriteLine($"Form fields filled and saved to \"{outputPdfPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF form: {ex.Message}");
        }
    }
}