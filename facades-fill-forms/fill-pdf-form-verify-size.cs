using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";
        const long maxSizeBytes   = 5 * 1024 * 1024; // 5 MB

        // ------------------------------------------------------------
        // Prepare sample data (replace with real data in production)
        // ------------------------------------------------------------
        DataTable data = new DataTable();
        data.Columns.Add("Name",    typeof(string));
        data.Columns.Add("Address", typeof(string));
        for (int i = 0; i < 100; i++)
        {
            data.Rows.Add($"Customer {i + 1}", $"Address {i + 1}");
        }

        // ------------------------------------------------------------
        // Ensure a PDF template with matching form fields exists.
        // ------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            CreateTemplate(templatePath, data);
        }

        // ------------------------------------------------------------
        // Fill the template using AutoFiller.
        // ------------------------------------------------------------
        using (AutoFiller filler = new AutoFiller())
        {
            filler.BindPdf(templatePath);   // Load the template PDF
            filler.ImportDataTable(data);   // Merge all rows
            filler.Save(outputPath);        // Save the result
        }

        // ------------------------------------------------------------
        // Verify that the generated PDF does not exceed the size limit.
        // ------------------------------------------------------------
        FileInfo resultInfo = new FileInfo(outputPath);
        if (resultInfo.Length > maxSizeBytes)
        {
            Console.Error.WriteLine(
                $"Error: Output PDF size {resultInfo.Length} bytes exceeds the limit of {maxSizeBytes} bytes.");
        }
        else
        {
            Console.WriteLine(
                $"Success: Output PDF size {resultInfo.Length} bytes is within the allowed limit.");
        }
    }

    // --------------------------------------------------------------------
    // Helper: creates a minimal PDF template containing a TextBoxField for
    // each column in the supplied DataTable. Field.PartialName must match
    // the column name for AutoFiller to bind correctly.
    // --------------------------------------------------------------------
    private static void CreateTemplate(string path, DataTable schema)
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();

        float yPos = 750;                     // starting vertical position
        const float left = 100;               // left margin
        const float fieldWidth = 300;
        const float fieldHeight = 20;
        const float verticalSpacing = 30;    // space between fields

        foreach (DataColumn col in schema.Columns)
        {
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                left,
                yPos,
                left + fieldWidth,
                yPos + fieldHeight);

            TextBoxField txt = new TextBoxField(page, rect)
            {
                PartialName = col.ColumnName,
                Value = string.Empty
            };
            doc.Form.Add(txt);
            yPos -= verticalSpacing;
        }

        doc.Save(path);
    }
}
