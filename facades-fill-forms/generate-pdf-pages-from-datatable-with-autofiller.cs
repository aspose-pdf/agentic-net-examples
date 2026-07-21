using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputDir    = "Output";
        const string combinedPdf  = "combined.pdf"; // single PDF that will contain all generated pages

        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // -------------------------------------------------
        // Build a sample DataTable – each row will become a PDF page
        // -------------------------------------------------
        DataTable data = new DataTable("Data");
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));
        data.Columns.Add("Date",    typeof(string));

        for (int i = 1; i <= 5; i++)
        {
            data.Rows.Add($"Customer {i}",
                          $"Street {i}",
                          DateTime.Now.AddDays(i).ToShortDateString());
        }

        // -------------------------------------------------
        // Use the AutoFiller facade to create one PDF page per DataTable row
        // -------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            // Bind the template PDF
            filler.BindPdf(templatePath);

            // Import the DataTable – each row produces a separate PDF document internally
            filler.ImportDataTable(data);

            // Save the generated PDFs into a *single* combined file (the overload that accepts a string)
            string combinedPath = Path.Combine(outputDir, combinedPdf);
            filler.Save(combinedPath);
        }

        // -------------------------------------------------
        // Log each processed row together with the page (document) number
        // -------------------------------------------------
        for (int i = 0; i < data.Rows.Count; i++)
        {
            int pageNumber = i + 1; // 1‑based page numbering
            Console.WriteLine($"Row {i} processed → generated PDF page #{pageNumber} (saved in '{combinedPdf}')");
        }

        Console.WriteLine("All rows processed and PDFs saved.");
    }
}
