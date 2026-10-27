using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatesFolder = "Templates";          // folder containing PDF templates
        const string csvPath        = "data.csv";           // CSV file with data rows
        const string outputFolder   = "Output";            // folder for generated PDFs

        if (!Directory.Exists(templatesFolder))
        {
            Console.Error.WriteLine($"Templates folder not found: {templatesFolder}");
            return;
        }

        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Read CSV – first line is header
        string[] csvLines = File.ReadAllLines(csvPath);
        if (csvLines.Length < 2)
        {
            Console.Error.WriteLine("CSV file does not contain data rows.");
            return;
        }

        string[] headers = csvLines[0].Split(',');

        // Process each template file
        foreach (string templatePath in Directory.GetFiles(templatesFolder, "*.pdf"))
        {
            string templateFileName = Path.GetFileNameWithoutExtension(templatePath);

            // Process each data row
            for (int rowIndex = 1; rowIndex < csvLines.Length; rowIndex++)
            {
                string[] values = csvLines[rowIndex].Split(',');

                // Build a dictionary of column name → value for quick lookup
                var rowData = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Length; i++)
                {
                    string value = i < values.Length ? values[i] : string.Empty;
                    rowData[headers[i].Trim()] = value.Trim();
                }

                // Load the template PDF
                using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(templatePath))
                {
                    // Iterate over all form fields in the document
                    foreach (Aspose.Pdf.Forms.Field field in doc.Form.Fields)
                    {
                        // Try to find a matching column in the CSV row
                        if (rowData.TryGetValue(field.Name, out string? fieldValue) && fieldValue != null)
                        {
                            // Handle TextBox fields directly
                            if (field is TextBoxField txtField)
                            {
                                txtField.Value = fieldValue;
                            }
                            // For other field types (including CheckBox), set the generic Value property.
                            // Aspose.Pdf will interpret "true", "1", "On" etc. appropriately for check boxes.
                            else
                            {
                                field.Value = fieldValue;
                            }
                        }
                    }

                    // Build output file name: TemplateName_RowNumber.pdf
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"{templateFileName}_Row{rowIndex}.pdf");

                    // Save the filled PDF
                    doc.Save(outputPath);
                    Console.WriteLine($"Saved: {outputPath}");
                }
            }
        }

        Console.WriteLine("Batch processing completed.");
    }
}
