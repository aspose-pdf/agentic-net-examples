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
        // Paths
        const string templatePath = "template.pdf";
        const string outputDir = "Output";
        const string mergedPdfPath = "merged.pdf"; // final PDF containing all pages

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);
        string mergedPdfFullPath = Path.Combine(outputDir, mergedPdfPath);

        // -----------------------------------------------------------------
        // Build a sample DataTable (in real life this would come from a DB)
        // -----------------------------------------------------------------
        DataTable dataTable = new DataTable("MailMerge");
        dataTable.Columns.Add("Field1", typeof(string));
        dataTable.Columns.Add("Field2", typeof(string));
        for (int i = 0; i < 5; i++)
        {
            dataTable.Rows.Add($"Value1_{i}", $"Value2_{i}");
        }

        // -----------------------------------------------------------------
        // Create a PDF template with form fields that match the DataTable columns
        // (self‑contained – no external files are required)
        // -----------------------------------------------------------------
        if (!File.Exists(templatePath))
        {
            using (Document templateDoc = new Document())
            {
                Page page = templateDoc.Pages.Add();
                float yPos = 750;
                const float left = 100, width = 300, height = 20, vSpace = 30;
                foreach (DataColumn col in dataTable.Columns)
                {
                    Rectangle rect = new Rectangle(left, yPos, left + width, yPos + height);
                    TextBoxField txt = new TextBoxField(page, rect)
                    {
                        PartialName = col.ColumnName,
                        Value = string.Empty
                    };
                    templateDoc.Form.Add(txt);
                    yPos -= vSpace;
                }
                templateDoc.Save(templatePath);
            }
        }

        // -----------------------------------------------------------------
        // Log each DataTable row before processing (row‑level logging)
        // -----------------------------------------------------------------
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            Console.WriteLine($"Processing DataTable row {i + 1} of {dataTable.Rows.Count}");
        }

        // -----------------------------------------------------------------
        // Use AutoFiller (new API) to merge the data into the template.
        // -----------------------------------------------------------------
        AutoFiller autoFiller = new AutoFiller();
        // Initialize the facade with the template PDF (replaces obsolete InputFileName)
        autoFiller.BindPdf(templatePath);
        // Import the DataTable – this prepares the filler.
        autoFiller.ImportDataTable(dataTable);
        // Save generates a single PDF where each row becomes a separate page.
        autoFiller.Save(mergedPdfFullPath);

        // -----------------------------------------------------------------
        // Log the page numbers that were created in the output PDF.
        // -----------------------------------------------------------------
        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            Console.WriteLine($"Created PDF page {i + 1} in {mergedPdfFullPath}");
        }

        // Clean up resources.
        autoFiller.Close();
    }
}
