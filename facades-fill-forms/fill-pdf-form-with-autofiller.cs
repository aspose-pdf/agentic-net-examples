using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";

        try
        {
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException("Input PDF file not found.", inputPath);
            }

            // Prepare a DataTable with field names matching the PDF form fields
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            DataRow dataRow = dataTable.NewRow();
            dataRow["FirstName"] = "John";
            dataRow["LastName"] = "Doe";
            dataTable.Rows.Add(dataRow);

            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the source PDF file
                autoFiller.BindPdf(inputPath);

                // Import data; if a column does not correspond to any form field, an exception may be thrown
                autoFiller.ImportDataTable(dataTable);

                // Save the filled PDF
                autoFiller.Save(outputPath);
            }

            Console.WriteLine($"Form filled and saved to '{outputPath}'.");
        }
        catch (FileNotFoundException fileEx)
        {
            Console.Error.WriteLine($"File error: {fileEx.Message}");
        }
        catch (InvalidOperationException invEx)
        {
            Console.Error.WriteLine($"Field error: {invEx.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}