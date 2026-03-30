using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

public class Program
{
    public static void Main()
    {
        string pdfTemplatePath = "template.pdf";
        string outputPdfPath = "filled_form.pdf";

        if (!File.Exists(pdfTemplatePath))
        {
            Console.Error.WriteLine("PDF template not found: " + pdfTemplatePath);
            return;
        }

        // Create a DataTable that mimics the Excel worksheet.
        // The first row of the worksheet is used as column headers.
        DataTable dataTable = CreateSampleDataTable();

        // Load the PDF form template
        using (Document pdfDocument = new Document(pdfTemplatePath))
        {
            // Fill the PDF form fields with the DataTable values
            AutoFiller filler = new AutoFiller();
            filler.BindPdf(pdfDocument);
            filler.ImportDataTable(dataTable);

            // Save the filled PDF
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine("PDF form filled and saved to " + outputPdfPath);
    }

    private static DataTable CreateSampleDataTable()
    {
        // Simulate reading an XLSX file where the first row contains column names.
        DataTable dt = new DataTable();
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Date", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));

        // Add sample rows – in a real scenario these would come from the Excel file.
        dt.Rows.Add("John Doe", DateTime.Today.ToShortDateString(), 123.45m);
        dt.Rows.Add("Jane Smith", DateTime.Today.AddDays(-1).ToShortDateString(), 678.90m);

        return dt;
    }
}