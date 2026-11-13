using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Folder containing the source PDFs
        const string inputFolder = "InputPdfs";
        // Folder where JSON files will be written
        const string outputFolder = "FormDataJson";

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

                    // Check that the document contains a form
                    if (doc.Form != null && doc.Form.Fields != null)
                    {
                        // Iterate over all form fields
                        foreach (var field in doc.Form.Fields)
                        {
                            // Resolve a field name (full name preferred)
                            string name = field.FullName ?? field.PartialName ?? string.Empty;

                            object value = null;

                            // Extract the value based on the concrete field type
                            switch (field)
                            {
                                case TextBoxField txt:
                                    value = txt.Value;
                                    break;
                                case CheckboxField chk:
                                    value = chk.Checked;
                                    break;
                                case RadioButtonOptionField rad:
                                    value = rad.Value;
                                    break;
                                case ListBoxField list:
                                    value = list.SelectedItems;
                                    break;
                                case ComboBoxField combo:
                                    value = combo.Value;
                                    break;
                                case SignatureField sig:
                                    value = sig.Signature != null ? "Signed" : null;
                                    break;
                                default:
                                    // Fallback: try to read a generic 'Value' property via reflection
                                    var prop = field.GetType().GetProperty("Value");
                                    if (prop != null)
                                        value = prop.GetValue(field);
                                    break;
                            }

                            formData[name] = value;
                        }
                    }

                    // Serialize the collected data to formatted JSON
                    string json = JsonSerializer.Serialize(formData, new JsonSerializerOptions { WriteIndented = true });

                    // Build the output JSON file path (same base name as the PDF)
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