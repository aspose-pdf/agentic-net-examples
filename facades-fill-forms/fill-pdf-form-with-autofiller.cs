using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the PDF form, the data source and the output PDF
        const string pdfFormPath = "form.pdf";
        const string dataFilePath = "data.csv"; // could be XML, JSON, etc.
        const string outputPath   = "filled_form.pdf";

        // Verify that the required files exist
        if (!File.Exists(pdfFormPath))
        {
            Console.Error.WriteLine($"Form PDF not found: {pdfFormPath}");
            return;
        }
        if (!File.Exists(dataFilePath))
        {
            Console.Error.WriteLine($"Data file not found: {dataFilePath}");
            return;
        }

        // --------------------------------------------------------------------
        // Create a DataTable whose column names match the PDF field names.
        // The column values will be filled from the external data source later.
        // --------------------------------------------------------------------
        DataTable mappingTable = new DataTable();

        // FirstName column mapping (PDF field name: FirstNameField)
        mappingTable.Columns.Add("FirstNameField", typeof(string));

        // LastName column mapping (PDF field name: LastNameField)
        mappingTable.Columns.Add("LastNameField", typeof(string));

        // Email column mapping (PDF field name: EmailField)
        mappingTable.Columns.Add("EmailField", typeof(string));

        // Add additional custom columns required by the PDF form here
        // mappingTable.Columns.Add("CustomColumn", typeof(string)); // PDF field name = "CustomColumn"

        // --------------------------------------------------------------------
        // Load the external data (CSV) into the DataTable.
        // For simplicity, this example assumes the CSV has the same column order
        // as the DataTable defined above.
        // --------------------------------------------------------------------
        using (var reader = new StreamReader(dataFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Simple CSV split – replace with a robust parser for real data.
                var parts = line.Split(',');
                if (parts.Length != mappingTable.Columns.Count)
                    continue; // skip malformed rows

                var row = mappingTable.NewRow();
                for (int i = 0; i < parts.Length; i++)
                    row[i] = parts[i].Trim();
                mappingTable.Rows.Add(row);
            }
        }

        // --------------------------------------------------------------------
        // Use AutoFiller to import the DataTable into the PDF form.
        // AutoFiller maps a column to a field when the column name equals the
        // field's PartialName. No ImportData method exists on FormEditor.
        // --------------------------------------------------------------------
        var filler = new AutoFiller();
        filler.BindPdf(pdfFormPath);
        filler.ImportDataTable(mappingTable);
        filler.Save(outputPath);
        filler.Close();

        Console.WriteLine($"Form filled and saved to '{outputPath}'.");
    }
}
