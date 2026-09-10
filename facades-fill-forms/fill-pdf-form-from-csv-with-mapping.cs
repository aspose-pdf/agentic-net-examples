using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string configPath   = "mapping.json";   // JSON: { "ExcelColumnName": "PdfFieldName", ... }
        const string csvPath      = "data.csv";       // Simple CSV, first line = headers
        const string templatePdf  = "template.pdf";   // PDF form template
        const string outputFolder = "FilledForms";

        // Validate input files
        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Config file not found: {configPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePdf))
        {
            Console.Error.WriteLine($"PDF template not found: {templatePdf}");
            return;
        }

        // Load column‑to‑field mapping
        Dictionary<string, string> columnToPdfField;
        try
        {
            columnToPdfField = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(configPath));
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to parse mapping file: {ex.Message}");
            return;
        }

        // Read CSV data
        string[] csvLines = File.ReadAllLines(csvPath);
        if (csvLines.Length < 2)
        {
            Console.Error.WriteLine("CSV file does not contain data rows.");
            return;
        }

        // First line = headers
        string[] headers = csvLines[0].Split(',');

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Process each data row
        for (int rowIndex = 1; rowIndex < csvLines.Length; rowIndex++)
        {
            string[] values = csvLines[rowIndex].Split(',');

            // Guard against malformed rows
            if (values.Length != headers.Length)
            {
                Console.Error.WriteLine($"Row {rowIndex} column count mismatch – skipped.");
                continue;
            }

            // Create a new Form facade bound to the template PDF
            using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form())
            {
                pdfForm.BindPdf(templatePdf);

                // Iterate over each column, map to PDF field if a mapping exists
                for (int col = 0; col < headers.Length; col++)
                {
                    string excelColumn = headers[col].Trim();
                    string cellValue   = values[col].Trim();

                    if (columnToPdfField.TryGetValue(excelColumn, out string pdfFieldName))
                    {
                        // Fill the PDF field; ignore empty values if desired
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            bool filled = pdfForm.FillField(pdfFieldName, cellValue);
                            if (!filled)
                            {
                                Console.Error.WriteLine($"Row {rowIndex}: Field \"{pdfFieldName}\" not found in PDF.");
                            }
                        }
                    }
                }

                // Save the filled PDF – one file per data row
                string outputPdfPath = Path.Combine(outputFolder, $"Filled_{rowIndex}.pdf");
                pdfForm.Save(outputPdfPath);
                Console.WriteLine($"Row {rowIndex} saved to \"{outputPdfPath}\"");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}