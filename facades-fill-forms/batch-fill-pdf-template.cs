using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the PDF template that contains form fields
        string templatePath = "template.pdf";

        // Ensure the template file exists – create a minimal PDF if it does not.
        if (!File.Exists(templatePath))
        {
            var emptyDoc = new Aspose.Pdf.Document();
            emptyDoc.Save(templatePath);
        }

        // Configure output folder and base file name (filled0.pdf, filled1.pdf, ...)
        string generatingPath = "./";          // folder where files will be written
        string baseFileName = "filled";        // base name used when we build the file name ourselves

        // Prepare a collection of DataTables, each representing a different data set
        DataTable[] dataSets = new DataTable[2];

        // First data set
        DataTable table1 = new DataTable("DataSet1");
        table1.Columns.Add("FirstName", typeof(string));
        table1.Columns.Add("LastName", typeof(string));
        DataRow row1 = table1.NewRow();
        row1["FirstName"] = "John";
        row1["LastName"] = "Doe";
        table1.Rows.Add(row1);
        dataSets[0] = table1;

        // Second data set
        DataTable table2 = new DataTable("DataSet2");
        table2.Columns.Add("FirstName", typeof(string));
        table2.Columns.Add("LastName", typeof(string));
        DataRow row2 = table2.NewRow();
        row2["FirstName"] = "Jane";
        row2["LastName"] = "Smith";
        table2.Rows.Add(row2);
        dataSets[1] = table2;

        // Fill the template for each DataTable and save a separate PDF file
        int fileIndex = 0;
        foreach (DataTable dataTable in dataSets)
        {
            // Create a fresh AutoFiller for each iteration (no Reset method in newer versions)
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the template PDF file
                autoFiller.BindPdf(templatePath);

                // Import the current data set into the form fields
                autoFiller.ImportDataTable(dataTable);

                // Build the destination file name (filled0.pdf, filled1.pdf, ...)
                string destination = Path.Combine(generatingPath,
                                                $"{baseFileName}{fileIndex}.pdf");

                // Save the filled PDF
                autoFiller.Save(destination);
            }

            fileIndex++;
        }

        Console.WriteLine("Batch filling completed.");
    }
}
