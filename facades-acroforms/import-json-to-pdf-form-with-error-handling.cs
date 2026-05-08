using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string jsonData = "data.json";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(jsonData))
        {
            Console.Error.WriteLine($"Data file not found: {jsonData}");
            return;
        }

        try
        {
            // Load the PDF form
            using (Form form = new Form(inputPdf))
            {
                // Open the JSON data stream
                using (FileStream jsonStream = new FileStream(jsonData, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        // Attempt to import JSON data into the form fields
                        form.ImportJson(jsonStream);
                    }
                    catch (Exception ex) // FormException is not available in the referenced version
                    {
                        // Try to treat the exception as a FormException by reflection.
                        // Aspose may expose the missing field name via a "FieldName" property.
                        string missingField = "unknown";
                        var fieldProp = ex.GetType().GetProperty("FieldName");
                        if (fieldProp != null)
                        {
                            var value = fieldProp.GetValue(ex) as string;
                            if (!string.IsNullOrEmpty(value))
                                missingField = value;
                        }
                        Console.Error.WriteLine($"Missing form field: {missingField}. Details: {ex.Message}");
                    }
                }

                // Save the updated PDF to the output path
                form.Save(outputPdf);
            }

            Console.WriteLine($"Form fields imported and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
