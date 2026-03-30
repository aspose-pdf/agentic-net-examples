using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths (files are expected to be in the working directory)
        string templatePath = "template.pdf";
        string mergedOutputPath = "merged.pdf";

        // Ensure a template PDF exists – create a minimal one if it does not.
        EnsureTemplateExists(templatePath);

        // Prepare sample data tables
        DataTable firstTable = CreateFirstDataTable();
        DataTable secondTable = CreateSecondDataTable();

        // Fill the template with each table and save to temporary files
        string firstFilledPath = "filled_1.pdf";
        FillTemplateAndSave(templatePath, firstTable, firstFilledPath);

        string secondFilledPath = "filled_2.pdf";
        FillTemplateAndSave(templatePath, secondTable, secondFilledPath);

        // Merge the filled PDFs into a single document
        using (Document mergedDocument = new Document())
        {
            mergedDocument.Merge(firstFilledPath, secondFilledPath);
            mergedDocument.Save(mergedOutputPath);
        }

        Console.WriteLine($"Merged PDF created at '{mergedOutputPath}'.");
    }

    // Creates a very simple one‑page PDF to act as a template if none is present.
    private static void EnsureTemplateExists(string path)
    {
        if (File.Exists(path))
            return;

        // Create a blank PDF with a single page. AutoFiller can import a DataTable
        // into such a document, automatically generating a table layout.
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(path);
        }
    }

    static DataTable CreateFirstDataTable()
    {
        DataTable table = new DataTable("First");
        table.Columns.Add(new DataColumn("Name", typeof(string)));
        table.Columns.Add(new DataColumn("Address", typeof(string)));

        DataRow row1 = table.NewRow();
        row1["Name"] = "Alice";
        row1["Address"] = "123 Main St";
        table.Rows.Add(row1);

        DataRow row2 = table.NewRow();
        row2["Name"] = "Bob";
        row2["Address"] = "456 Oak Ave";
        table.Rows.Add(row2);

        return table;
    }

    static DataTable CreateSecondDataTable()
    {
        DataTable table = new DataTable("Second");
        table.Columns.Add(new DataColumn("Product", typeof(string)));
        table.Columns.Add(new DataColumn("Price", typeof(string)));

        DataRow row1 = table.NewRow();
        row1["Product"] = "Widget";
        row1["Price"] = "$10";
        table.Rows.Add(row1);

        DataRow row2 = table.NewRow();
        row2["Product"] = "Gadget";
        row2["Price"] = "$15";
        table.Rows.Add(row2);

        return table;
    }

    static void FillTemplateAndSave(string templateFile, DataTable data, string outputFile)
    {
        // AutoFiller implements IDisposable – use a using block for deterministic cleanup.
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templateFile);
            filler.ImportDataTable(data);
            filler.Save(outputFile);
        }
    }
}
