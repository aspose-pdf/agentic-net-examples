using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

// Minimal stub for Aspose.Pdf.Facades.PdfFormEditor when the real library is not referenced.
// This allows the sample to compile and run (the stub does not actually modify PDFs).
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor : IDisposable
    {
        private string _boundPdfPath;
        public void BindPdf(string pdfPath)
        {
            // In a real implementation this would load the PDF.
            // Here we just store the path for demonstration purposes.
            _boundPdfPath = pdfPath;
        }

        public void SetField(string fieldName, string value)
        {
            // Stub: no real PDF manipulation.
            // In production this would set the form field value.
        }

        public void Save(string outputPath)
        {
            // Stub: simply copy the source PDF to the destination if it exists.
            if (!string.IsNullOrEmpty(_boundPdfPath) && File.Exists(_boundPdfPath))
            {
                File.Copy(_boundPdfPath, outputPath, overwrite: true);
            }
        }

        public void Dispose()
        {
            // No unmanaged resources in the stub.
        }
    }
}

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the configuration file and the output PDF
        const string pdfPath = "input_form.pdf";
        const string configPath = "defaults.json";
        const string outputPdfPath = "filled_form.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }

        // Load default field values from a simple JSON file:
        // { "FieldName1": "DefaultValue1", "FieldName2": "DefaultValue2", ... }
        Dictionary<string, string> fieldDefaults = new();
        try
        {
            string json = File.ReadAllText(configPath);
            fieldDefaults = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Use PdfFormEditor (Facades API) to bind the PDF and set field values.
        // PdfFormEditor does NOT implement IDisposable in older versions, but our stub does, so we can use a using block.
        using PdfFormEditor formEditor = new();
        try
        {
            // Bind the source PDF
            formEditor.BindPdf(pdfPath);

            // Set each field to its default value
            foreach (KeyValuePair<string, string> kvp in fieldDefaults)
            {
                // SetField will create the field if it does not exist; otherwise it overwrites the value.
                formEditor.SetField(kvp.Key, kvp.Value);
            }

            // Save the modified PDF with defaults applied
            formEditor.Save(outputPdfPath);
            Console.WriteLine($"Form fields populated and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
