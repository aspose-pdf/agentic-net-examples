using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string templatePath = "Template.pdf";
        const string outputDirectory = "GeneratedForms";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        // Replace this with real database retrieval logic.
        DataTable records = GetSampleData();

        int recordNumber = 1;
        foreach (DataRow row in records.Rows)
        {
            // Load a fresh copy of the template for each record.
            using (Document pdf = new Document(templatePath))
            {
                // Iterate over each column; column names must match PDF field names.
                foreach (DataColumn column in records.Columns)
                {
                    string fieldName = column.ColumnName;
                    string fieldValue = row[column].ToString();

                    // Access the field; if it does not exist, skip.
                    var field = pdf.Form[fieldName];
                    if (field == null) continue;

                    // Fill based on the concrete field type.
                    if (field is TextBoxField textBox)
                    {
                        textBox.Value = fieldValue;
                    }
                    else if (field is CheckboxField checkBox)
                    {
                        // Accept "true"/"false" or "1"/"0" as boolean values.
                        bool isChecked = fieldValue.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                                         fieldValue == "1";
                        checkBox.Checked = isChecked;
                    }
                    // Additional field types (RadioButtonField, ListBoxField, etc.) can be handled here.
                }

                string outputPath = Path.Combine(outputDirectory, $"Form_{recordNumber}.pdf");
                pdf.Save(outputPath);
                Console.WriteLine($"Generated filled form: {outputPath}");
            }

            recordNumber++;
        }
    }

    // Mock method to simulate data retrieval; replace with actual DB access code.
    static DataTable GetSampleData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Subscribe", typeof(string)); // Corresponds to a checkbox field.

        table.Rows.Add("John", "Doe", "true");
        table.Rows.Add("Jane", "Smith", "false");
        return table;
    }
}