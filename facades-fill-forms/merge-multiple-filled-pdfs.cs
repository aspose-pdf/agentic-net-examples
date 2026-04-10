using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Path to the PDF template that contains form fields.
        const string templatePath = "template.pdf";

        // Ensure the template exists – create a minimal one if it does not.
        EnsureTemplateExists(templatePath);

        // Example: assume we have three DataTables, each representing data for one filled PDF.
        DataTable[] tables = new DataTable[3];
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i] = CreateSampleDataTable(i);
        }

        // Generate a filled PDF for each DataTable using AutoFiller.
        string[] filledPdfPaths = new string[tables.Length];
        for (int i = 0; i < tables.Length; i++)
        {
            string outputPath = $"filled_{i}.pdf";

            // AutoFiller implements IDisposable, so wrap it in a using block.
            using (AutoFiller filler = new AutoFiller())
            {
                // The InputFileName property is obsolete – use BindPdf instead.
                filler.BindPdf(templatePath);          // initialize with the template PDF
                filler.ImportDataTable(tables[i]);     // bind data
                filler.Save(outputPath);               // save filled PDF
            }

            filledPdfPaths[i] = outputPath;
        }

        // Merge all filled PDFs into a single consolidated PDF using PdfFileEditor (Facades API).
        const string mergedOutputPath = "merged.pdf";
        // PdfFileEditor does NOT implement IDisposable, so do NOT use a using statement.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filledPdfPaths, mergedOutputPath);

        // Optional: clean up temporary filled PDFs.
        foreach (string path in filledPdfPaths)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        Console.WriteLine($"Merged PDF created at '{mergedOutputPath}'.");
    }

    // Helper method to create a simple DataTable with sample data.
    static DataTable CreateSampleDataTable(int index)
    {
        DataTable dt = new DataTable($"DataTable{index}");
        dt.Columns.Add("Field1", typeof(string));
        dt.Columns.Add("Field2", typeof(string));

        DataRow row = dt.NewRow();
        row["Field1"] = $"Value1_{index}";
        row["Field2"] = $"Value2_{index}";
        dt.Rows.Add(row);

        return dt;
    }

    // Creates a minimal PDF template with two text fields if the file does not exist.
    static void EnsureTemplateExists(string path)
    {
        if (File.Exists(path))
            return;

        // Create a new PDF document.
        Document doc = new Document();
        Page page = doc.Pages.Add();
        page.PageInfo.Width = 595;   // A4 width in points
        page.PageInfo.Height = 842;  // A4 height in points

        // Add first text field.
        TextBoxField field1 = new TextBoxField(page, new Rectangle(100, 700, 300, 720))
        {
            PartialName = "Field1",
            Value = string.Empty
        };
        doc.Form.Add(field1, 1);

        // Add second text field.
        TextBoxField field2 = new TextBoxField(page, new Rectangle(100, 650, 300, 670))
        {
            PartialName = "Field2",
            Value = string.Empty
        };
        doc.Form.Add(field2, 1);

        // Save the template.
        doc.Save(path);
    }
}
