using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

public class BatchFiller
{
    public static void Main()
    {
        // Path to the PDF template that contains form fields
        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "template.pdf");

        if (!File.Exists(templatePath))
        {
            Console.WriteLine($"Template file not found: {templatePath}");
            return;
        }

        // Prepare a list of DataTables, each representing a distinct data set
        List<DataTable> dataSets = new List<DataTable>();

        // ----- First data set -----
        DataTable dt1 = new DataTable("DataSet1");
        dt1.Columns.Add("FirstName", typeof(string));
        dt1.Columns.Add("LastName", typeof(string));
        dt1.Columns.Add("Email", typeof(string));

        DataRow row1 = dt1.NewRow();
        row1["FirstName"] = "John";
        row1["LastName"] = "Doe";
        row1["Email"] = "john.doe@example.com";
        dt1.Rows.Add(row1);

        DataRow row2 = dt1.NewRow();
        row2["FirstName"] = "Jane";
        row2["LastName"] = "Smith";
        row2["Email"] = "jane.smith@example.com";
        dt1.Rows.Add(row2);

        dataSets.Add(dt1);

        // ----- Second data set -----
        DataTable dt2 = new DataTable("DataSet2");
        dt2.Columns.Add("FirstName", typeof(string));
        dt2.Columns.Add("LastName", typeof(string));
        dt2.Columns.Add("Email", typeof(string));

        DataRow row3 = dt2.NewRow();
        row3["FirstName"] = "Alice";
        row3["LastName"] = "Brown";
        row3["Email"] = "alice.brown@example.com";
        dt2.Rows.Add(row3);

        dataSets.Add(dt2);

        // Output folder for the generated PDFs
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "output");
        Directory.CreateDirectory(outputFolder);

        // Process each DataTable with AutoFiller
        for (int i = 0; i < dataSets.Count; i++)
        {
            DataTable currentTable = dataSets[i];

            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF
                filler.BindPdf(templatePath);

                // Configure base file name for this batch (the AutoFiller will append row index automatically)
                filler.BasicFileName = $"filledSet{i}";

                // Import the data; each row will generate a separate PDF file
                filler.ImportDataTable(currentTable);

                // Save all generated PDFs to the output folder using the non‑obsolete overload
                filler.Save(outputFolder);
            }
        }

        Console.WriteLine("Batch filling completed. Files are located in the 'output' folder.");
    }
}
