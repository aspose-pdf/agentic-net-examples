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
        const string configPath = "mapping.json"; // JSON: { "ExcelColumnName": "PdfFieldName", ... }
        const string excelPath = "data.csv";      // Simple CSV, first line = headers, subsequent lines = data rows
        const string pdfTemplatePath = "template.pdf";
        const string outputFolder = "output";

        if (!File.Exists(configPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configPath}");
            return;
        }
        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel CSV file not found: {excelPath}");
            return;
        }
        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {pdfTemplatePath}");
            return;
        }

        // Load column‑to‑field mapping
        string configJson = File.ReadAllText(configPath);
        Dictionary<string, string> columnToFieldMap = JsonSerializer.Deserialize<Dictionary<string, string>>(configJson);
        if (columnToFieldMap == null)
        {
            Console.Error.WriteLine("Failed to deserialize mapping configuration.");
            return;
        }

        // Read CSV content
        string[] csvLines = File.ReadAllLines(excelPath);
        if (csvLines.Length < 2)
        {
            Console.Error.WriteLine("CSV file does not contain data rows.");
            return;
        }
        string[] headers = csvLines[0].Split(',');

        // Ensure output folder exists
        Directory.CreateDirectory(outputFolder);

        for (int rowIndex = 1; rowIndex < csvLines.Length; rowIndex++)
        {
            string[] values = csvLines[rowIndex].Split(',');
            if (values.Length != headers.Length)
            {
                Console.Error.WriteLine($"Row {rowIndex} column count mismatch; skipping.");
                continue;
            }

            // Load a fresh copy of the template for each row
            using (Document pdfDocument = new Document(pdfTemplatePath))
            {
                // Initialise the Form facade on the document
                Form pdfForm = new Form(pdfDocument);

                // Iterate through each column and fill the mapped PDF field if a mapping exists
                for (int col = 0; col < headers.Length; col++)
                {
                    string excelHeader = headers[col].Trim();
                    if (columnToFieldMap.ContainsKey(excelHeader))
                    {
                        string pdfFieldName = columnToFieldMap[excelHeader];
                        string fieldValue = values[col].Trim();
                        // Fill the field using the fully qualified field name
                        pdfForm.FillField(pdfFieldName, fieldValue);
                    }
                }

                // Save the filled PDF – output file name is simple (no path in Save call)
                string outputFileName = $"filled_{rowIndex}.pdf";
                string outputPath = Path.Combine(outputFolder, outputFileName);
                // Change current directory to output folder so Save receives only the file name
                string originalDirectory = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(outputFolder);
                pdfDocument.Save(outputFileName);
                Directory.SetCurrentDirectory(originalDirectory);
            }
        }

        Console.WriteLine("All rows processed and PDFs generated.");
    }
}