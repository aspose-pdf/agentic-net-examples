using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // <-- added for Field class

class Program
{
    static void Main()
    {
        // Paths
        const string csvPath = "data.csv";
        const string templatePath = "template.pdf";
        const string outputDir = "output";

        // Verify input files exist
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"PDF template not found: {templatePath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load CSV data into a DataTable
        DataTable dataTable = new DataTable();
        using (StreamReader reader = new StreamReader(csvPath))
        {
            bool headerRead = false;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] fields = line.Split(',');

                if (!headerRead)
                {
                    // First line contains column names
                    foreach (string col in fields)
                        dataTable.Columns.Add(col.Trim());
                    headerRead = true;
                }
                else
                {
                    // Subsequent lines contain data rows
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < fields.Length && i < dataTable.Columns.Count; i++)
                        row[i] = fields[i].Trim();
                    dataTable.Rows.Add(row);
                }
            }
        }

        // Process each data row: fill the template and save a new PDF
        int recordIndex = 1;
        foreach (DataRow dataRow in dataTable.Rows)
        {
            // Load the PDF template (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(templatePath))
            {
                // Iterate over columns and set corresponding form fields
                foreach (DataColumn column in dataTable.Columns)
                {
                    string fieldName = column.ColumnName;
                    string fieldValue = dataRow[column]?.ToString() ?? string.Empty;

                    // Access the form field; if it exists, assign the value
                    // NOTE: Form indexer returns a WidgetAnnotation; cast to Field to use the Value property
                    Field field = pdfDoc.Form[fieldName] as Field;
                    if (field != null)
                        field.Value = fieldValue;
                }

                // Build output file name and save (lifecycle rule: use Save)
                string outputPath = Path.Combine(outputDir, $"filled_{recordIndex}.pdf");
                pdfDoc.Save(outputPath);
                Console.WriteLine($"Saved filled PDF: {outputPath}");
            }

            recordIndex++;
        }
    }
}
