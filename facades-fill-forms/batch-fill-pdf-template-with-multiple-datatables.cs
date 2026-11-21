using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class BatchPdfFiller
{
    static void Main()
    {
        // Path to the PDF form template
        const string templatePath = "template.pdf";

        // Verify that the template exists
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Prepare a list of DataTable objects, each representing a data set.
        // In a real scenario these would be populated from a database, CSV, etc.
        List<DataTable> dataSets = GetDataSets();

        // Output directory for the filled PDFs
        const string outputDir = "FilledOutputs";
        Directory.CreateDirectory(outputDir);

        // Process each data set
        for (int i = 0; i < dataSets.Count; i++)
        {
            DataTable table = dataSets[i];

            // Load the template PDF into a Document instance
            Document pdfDocument = new Document(templatePath);

            // Enable automatic recalculation of JavaScript fields (if any)
            pdfDocument.Form.AutoRecalculate = true;

            // Fill the form fields using column names that match the PDF field names
            FillFormFromDataTable(pdfDocument, table);

            // Define the output file name (e.g., Filled_1.pdf, Filled_2.pdf, ...)
            string outputPath = Path.Combine(outputDir, $"Filled_{i + 1}.pdf");

            // Save the filled PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Saved filled PDF: {outputPath}");
        }

        Console.WriteLine("Batch filling completed.");
    }

    /// <summary>
    /// Populates the fields of a PDF document using the first row of the supplied DataTable.
    /// Column names must match the PDF field names.
    /// </summary>
    private static void FillFormFromDataTable(Document doc, DataTable table)
    {
        if (table == null || table.Rows.Count == 0)
            return;

        DataRow row = table.Rows[0];
        foreach (DataColumn col in table.Columns)
        {
            // Find the field with the same name as the column
            Field field = doc.Form?.Fields?.FirstOrDefault(f => f.Name == col.ColumnName);
            if (field != null)
            {
                // Convert the cell value to string (handle DBNull)
                object cellValue = row[col];
                string value = cellValue == DBNull.Value ? string.Empty : cellValue.ToString();
                field.Value = value;
            }
        }
    }

    // Mock method to generate example DataTables.
    // Replace this with actual data retrieval logic.
    static List<DataTable> GetDataSets()
    {
        var list = new List<DataTable>();

        // Example DataTable 1
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("FirstName");
        dt1.Columns.Add("LastName");
        dt1.Columns.Add("Email");
        dt1.Rows.Add("John", "Doe", "john.doe@example.com");
        list.Add(dt1);

        // Example DataTable 2
        DataTable dt2 = new DataTable();
        dt2.Columns.Add("FirstName");
        dt2.Columns.Add("LastName");
        dt2.Columns.Add("Email");
        dt2.Rows.Add("Jane", "Smith", "jane.smith@example.com");
        list.Add(dt2);

        // Add more DataTables as needed
        return list;
    }
}
