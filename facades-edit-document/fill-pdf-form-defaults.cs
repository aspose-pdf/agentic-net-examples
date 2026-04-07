using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "form.pdf";
        const string outputPdfPath = "filled_form.pdf";
        const string configFilePath = "form_defaults.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
            return;
        }

        // Load the PDF form with an explicit output file name.
        Form pdfForm = new Form(inputPdfPath, outputPdfPath);

        // Read default values from a simple key=value text file.
        Dictionary<string, string> defaultValues = new Dictionary<string, string>();
        using (StreamReader reader = new StreamReader(configFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Skip empty lines and comments.
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                {
                    continue;
                }
                int separatorIndex = line.IndexOf('=');
                if (separatorIndex > 0)
                {
                    string fieldName = line.Substring(0, separatorIndex).Trim();
                    string fieldValue = line.Substring(separatorIndex + 1).Trim();
                    defaultValues[fieldName] = fieldValue;
                }
            }
        }

        // Fill each field with its default value.
        foreach (KeyValuePair<string, string> entry in defaultValues)
        {
            bool filled = pdfForm.FillField(entry.Key, entry.Value);
            if (!filled)
            {
                Console.Error.WriteLine($"Warning: field '{entry.Key}' not found or could not be filled.");
            }
        }

        // Save the updated PDF.
        pdfForm.Save();
        Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
    }
}