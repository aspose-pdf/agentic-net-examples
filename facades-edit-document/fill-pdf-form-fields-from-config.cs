using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "filled.pdf";
        const string configFile = "defaults.cfg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(configFile))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFile}");
            return;
        }

        // Load default values from a simple key=value configuration file.
        var defaults = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var line in File.ReadAllLines(configFile))
        {
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                continue; // Skip empty lines and comments.

            var parts = line.Split(new[] { '=' }, 2);
            if (parts.Length == 2)
            {
                var fieldName = parts[0].Trim();
                var fieldValue = parts[1].Trim();
                if (!string.IsNullOrEmpty(fieldName))
                    defaults[fieldName] = fieldValue;
            }
        }

        // Use Aspose.Pdf.Facades.Form to fill the fields.
        using (Form form = new Form(inputPdf))
        {
            foreach (var kvp in defaults)
            {
                // Fill each field; FillField returns false if the field name is not found.
                bool success = form.FillField(kvp.Key, kvp.Value);
                if (!success)
                {
                    Console.WriteLine($"Warning: field '{kvp.Key}' not found in the PDF.");
                }
            }

            // Save the updated PDF.
            form.Save(outputPdf);
        }

        Console.WriteLine($"Form fields populated and saved to '{outputPdf}'.");
    }
}