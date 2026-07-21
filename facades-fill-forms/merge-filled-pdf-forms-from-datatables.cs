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
        // Obtain the DataTables that will be merged into the template.
        DataTable[] tables = GetDataTables();
        if (tables == null || tables.Length == 0)
        {
            Console.WriteLine("No data tables provided.");
            return;
        }

        // Create a temporary folder to store the template, the individually filled PDFs and the final merged PDF.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeMergeTemp");
        Directory.CreateDirectory(tempFolder);

        // Path to the PDF template that contains form fields.
        string templatePath = Path.Combine(tempFolder, "template.pdf");
        // Path for the final merged PDF.
        string outputPath = Path.Combine(tempFolder, "merged.pdf");

        // ---------------------------------------------------------------------
        // 1. Generate a simple template PDF with form fields that match the column
        //    names of the first DataTable. This guarantees the file exists at
        //    runtime (hard‑coded‑input‑file‑generate‑inline‑first).
        // ---------------------------------------------------------------------
        CreateTemplatePdf(templatePath, tables[0]);

        // Array to hold the file names of the filled PDFs.
        string[] filledFiles = new string[tables.Length];

        // Fill the template for each DataTable and save to a temporary file.
        for (int i = 0; i < tables.Length; i++)
        {
            string tempFile = Path.Combine(tempFolder, $"filled_{i}.pdf");

            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(templatePath);
                filler.ImportDataTable(tables[i]);
                filler.Save(tempFile);
            }

            filledFiles[i] = tempFile;
        }

        // Merge all filled PDFs into a single document using PdfFileEditor.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filledFiles, outputPath);

        // Clean up temporary files (the merged PDF is kept for inspection).
        foreach (string file in filledFiles)
        {
            try { File.Delete(file); } catch { }
        }
        try { File.Delete(templatePath); } catch { }
        // NOTE: Do NOT delete the whole temp folder because it also contains the merged PDF.

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }

    // Creates a minimal PDF template with a TextBoxField for each column in the DataTable.
    private static void CreateTemplatePdf(string path, DataTable table)
    {
        using (Document doc = new Document())
        {
            // Add a single page.
            Page page = doc.Pages.Add();

            // Positioning variables for the fields.
            float startY = 700f;
            float fieldHeight = 20f;
            float verticalSpacing = 30f;

            foreach (DataColumn col in table.Columns)
            {
                // Define a rectangle for the field.
                Rectangle rect = new Rectangle(100, startY, 300, startY + fieldHeight);

                // Correct constructor: (Page, Rectangle)
                TextBoxField txt = new TextBoxField(page, rect)
                {
                    PartialName = col.ColumnName,
                    Value = string.Empty
                };

                // Add the field to the document's form collection.
                doc.Form.Add(txt);

                startY -= verticalSpacing;
            }

            doc.Save(path);
        }
    }

    // Example method that returns an array of DataTables.
    // Replace this with actual data retrieval logic.
    static DataTable[] GetDataTables()
    {
        DataTable dt1 = new DataTable("Table1");
        dt1.Columns.Add("Field1", typeof(string));
        dt1.Columns.Add("Field2", typeof(string));
        dt1.Rows.Add("Value11", "Value12");

        DataTable dt2 = new DataTable("Table2");
        dt2.Columns.Add("Field1", typeof(string));
        dt2.Columns.Add("Field2", typeof(string));
        dt2.Rows.Add("Value21", "Value22");

        return new[] { dt1, dt2 };
    }
}
