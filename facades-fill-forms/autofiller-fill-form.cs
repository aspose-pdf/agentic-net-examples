using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "filled.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file '{inputPdf}' not found.");
            return;
        }

        // Prepare a DataTable containing field names and their corresponding values
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("FieldName", typeof(string));
        dataTable.Columns.Add("FieldValue", typeof(string));

        DataRow row1 = dataTable.NewRow();
        row1["FieldName"] = "FirstName";
        row1["FieldValue"] = "John";
        dataTable.Rows.Add(row1);

        DataRow row2 = dataTable.NewRow();
        row2["FieldName"] = "LastName";
        row2["FieldValue"] = "Doe";
        dataTable.Rows.Add(row2);

        try
        {
            using (AutoFiller autofiller = new AutoFiller())
            {
                // Bind the source PDF document
                autofiller.BindPdf(inputPdf);

                // Import the field values – may throw if a field name does not exist in the form
                autofiller.ImportDataTable(dataTable);

                // Save the filled PDF to the output file
                autofiller.Save(outputPdf);
            }

            Console.WriteLine($"Form filled successfully. Output saved to '{outputPdf}'.");
        }
        catch (FileNotFoundException fileEx)
        {
            Console.Error.WriteLine($"File error: {fileEx.Message}");
        }
        catch (IOException ioEx)
        {
            Console.Error.WriteLine($"I/O error: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}