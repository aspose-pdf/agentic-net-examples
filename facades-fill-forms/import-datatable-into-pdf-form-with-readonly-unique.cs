using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form_template.pdf";
        const string outputPdf = "filled_form.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Template not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Create a DataTable and configure column constraints before
        //    importing it into the PDF form.
        // ------------------------------------------------------------
        DataTable table = new DataTable("MyTable");

        DataColumn idColumn = new DataColumn("ID", typeof(int))
        {
            ReadOnly = true,   // column is read‑only
            Unique   = true    // enforce unique values
        };
        table.Columns.Add(idColumn);

        DataColumn nameColumn = new DataColumn("Name", typeof(string));
        table.Columns.Add(nameColumn);

        // Populate rows – duplicate IDs would throw because of Unique = true
        table.Rows.Add(1, "Alice");
        table.Rows.Add(2, "Bob");
        // table.Rows.Add(1, "Charlie"); // <-- would raise an exception

        // ------------------------------------------------------------
        // 2. Load the PDF and import the DataTable using AutoFiller.
        //    AutoFiller is the correct class for ImportDataTable.
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // AutoFiller works with a Document (or a file path) to fill form fields.
            AutoFiller filler = new AutoFiller();
            filler.BindPdf(doc);

            // Import the DataTable. Field names in the PDF must match the column names.
            filler.ImportDataTable(table);

            // Save the filled PDF.
            filler.Save(outputPdf);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdf}'.");
    }
}
