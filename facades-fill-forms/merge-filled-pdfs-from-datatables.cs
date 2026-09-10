using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // <-- added for Border and BorderStyle

class Program
{
    static void Main()
    {
        // Path to the PDF form template that contains fillable fields
        const string templatePath = "template.pdf";

        // Example: three separate DataTables that hold data for three PDFs
        DataTable[] tables = new DataTable[3];
        for (int i = 0; i < tables.Length; i++)
        {
            tables[i] = CreateSampleDataTable(i);
        }

        // Ensure the template PDF exists – create it on‑the‑fly if it does not.
        // The template must contain a TextBoxField for each column name.
        if (!File.Exists(templatePath))
        {
            // Use the first table to derive the column list (all tables share the same schema).
            CreateTemplateFromDataTable(templatePath, tables[0]);
        }

        // Generate filled PDFs – one per DataTable – using AutoFiller
        string[] filledPdfPaths = new string[tables.Length];
        for (int i = 0; i < tables.Length; i++)
        {
            string outPath = $"filled_{i}.pdf";
            filledPdfPaths[i] = outPath;

            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF (new API – replaces the obsolete InputFileName property)
                filler.BindPdf(templatePath);

                // Import data; column names must match field names in the template
                filler.ImportDataTable(tables[i]);

                // Save the filled document
                filler.Save(outPath);
            }
        }

        // Merge all filled PDFs into a single consolidated PDF using PdfFileEditor (Facades API)
        const string mergedPdfPath = "merged.pdf";
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(filledPdfPaths, mergedPdfPath);

        // (Optional) Clean up temporary files
        // foreach (var path in filledPdfPaths) File.Delete(path);
    }

    // Helper method to create a simple DataTable with sample data
    static DataTable CreateSampleDataTable(int index)
    {
        DataTable dt = new DataTable("MailMerge");
        dt.Columns.Add("CompanyName", typeof(string));
        dt.Columns.Add("ContactName", typeof(string));
        dt.Columns.Add("Address", typeof(string));
        dt.Columns.Add("PostalCode", typeof(string));
        dt.Columns.Add("City", typeof(string));
        dt.Columns.Add("Country", typeof(string));
        dt.Columns.Add("Heading", typeof(string));

        DataRow row = dt.NewRow();
        row["CompanyName"] = $"Company {index}";
        row["ContactName"] = $"Contact {index}";
        row["Address"] = $"123 Street {index}";
        row["PostalCode"] = $"000{index}";
        row["City"] = $"City {index}";
        row["Country"] = $"Country {index}";
        row["Heading"] = $"Dear {row["CompanyName"]},";
        dt.Rows.Add(row);

        return dt;
    }

    // Creates a PDF template containing a TextBoxField for each column in the supplied DataTable.
    static void CreateTemplateFromDataTable(string path, DataTable dt)
    {
        // Create a new empty PDF document.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Simple layout: fields are placed one under another.
            float startY = 750f;               // Top of the first field.
            const float left = 100f;           // Left margin.
            const float fieldWidth = 300f;
            const float fieldHeight = 20f;
            const float verticalSpacing = 30f;

            foreach (DataColumn col in dt.Columns)
            {
                Rectangle rect = new Rectangle(left, startY, left + fieldWidth, startY + fieldHeight);

                // Create the field first, then configure its properties.
                TextBoxField txtField = new TextBoxField(page, rect);
                txtField.PartialName = col.ColumnName;
                txtField.Value = string.Empty;
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };
                txtField.Color = Color.Black;

                doc.Form.Add(txtField);
                startY -= verticalSpacing;
            }

            doc.Save(path);
        }
    }
}
