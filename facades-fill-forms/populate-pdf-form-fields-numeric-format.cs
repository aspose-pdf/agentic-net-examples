using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath = "filled_output.pdf";

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        using (Document pdfDocument = new Document(templatePath))
        {
            DataTable dataTable = new DataTable("FormData");
            dataTable.Columns.Add("Amount", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            DataRow dataRow = dataTable.NewRow();
            decimal amountValue = 1234.567m;
            dataRow["Amount"] = amountValue.ToString("N2"); // format with two decimal places and thousand separator
            dataRow["Description"] = "Sample item";
            dataTable.Rows.Add(dataRow);

            AutoFiller autoFiller = new AutoFiller();
            autoFiller.BindPdf(pdfDocument);
            autoFiller.ImportDataTable(dataTable);
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}