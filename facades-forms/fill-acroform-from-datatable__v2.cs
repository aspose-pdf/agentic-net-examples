using System;
using System.IO;
using System.Data;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "form.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // Create a synthetic DataTable with explicit date and decimal formatting
        // ------------------------------------------------------------
        DataTable dataTable = new DataTable();

        // Columns must match the names of the AcroForm fields in the PDF
        dataTable.Columns.Add("DateField",    typeof(string));
        dataTable.Columns.Add("AmountField",  typeof(string));
        dataTable.Columns.Add("NameField",    typeof(string));

        // Sample data
        DateTime sampleDate   = new DateTime(2023, 12, 31);
        decimal  sampleAmount = 12345.6789m;
        string   sampleName   = "John Doe";

        // Explicit formatting
        string formattedDate   = sampleDate.ToString("yyyy-MM-dd"); // ISO date format
        string formattedAmount = sampleAmount.ToString("F2");       // two decimal places

        // Add a single row with the formatted strings
        dataTable.Rows.Add(formattedDate, formattedAmount, sampleName);

        // ------------------------------------------------------------
        // Fill the AcroForm fields using AutoFiller
        // ------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();

        // Bind the source PDF that contains the AcroForm
        autoFiller.BindPdf(inputPdf);

        // Import the DataTable – the method takes a single DataTable argument
        autoFiller.ImportDataTable(dataTable);

        // Save the filled PDF
        autoFiller.Save(outputPdf);

        Console.WriteLine($"Filled PDF saved to '{outputPdf}'.");
    }
}