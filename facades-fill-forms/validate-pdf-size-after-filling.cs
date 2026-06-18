using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

namespace ValidatePdfSizeAfterFilling
{
    class Program
    {
        static void Main(string[] args)
        {
            // Step 1: Create a simple PDF with a text field named "Name"
            using (Document templateDoc = new Document())
            {
                // Add a page (1‑based indexing)
                Page page = templateDoc.Pages.Add();

                // Create a text box field on the page
                Rectangle rect = new Rectangle(100, 700, 300, 730);
                TextBoxField nameField = new TextBoxField(page, rect);
                nameField.PartialName = "Name";
                nameField.Value = string.Empty;
                templateDoc.Form.Add(nameField, 1);

                // Save the template PDF (self‑contained example)
                templateDoc.Save("template.pdf");
            }

            // Step 2: Prepare a DataTable with up to 4 rows (evaluation mode limit)
            DataTable dataTable = new DataTable("FormData");
            DataColumn nameColumn = new DataColumn("Name", typeof(string));
            dataTable.Columns.Add(nameColumn);

            DataRow row1 = dataTable.NewRow();
            row1["Name"] = "Alice";
            dataTable.Rows.Add(row1);

            DataRow row2 = dataTable.NewRow();
            row2["Name"] = "Bob";
            dataTable.Rows.Add(row2);

            DataRow row3 = dataTable.NewRow();
            row3["Name"] = "Charlie";
            dataTable.Rows.Add(row3);

            DataRow row4 = dataTable.NewRow();
            row4["Name"] = "Diana";
            dataTable.Rows.Add(row4);

            // Step 3: Use AutoFiller to merge the data into the template PDF
            AutoFiller autoFiller = new AutoFiller();
            autoFiller.BindPdf("template.pdf");
            autoFiller.ImportDataTable(dataTable);
            string outputPdfPath = "output.pdf";
            autoFiller.Save(outputPdfPath);
            autoFiller.Close();

            // Step 4: Validate that the output PDF size does not exceed the limit
            long maxSizeBytes = 50000; // Example limit: 50 KB
            FileInfo outputInfo = new FileInfo(outputPdfPath);
            if (outputInfo.Length > maxSizeBytes)
            {
                Console.WriteLine($"PDF size {outputInfo.Length} bytes exceeds the limit of {maxSizeBytes} bytes.");
            }
            else
            {
                Console.WriteLine($"PDF size {outputInfo.Length} bytes is within the allowed limit.");
            }
        }
    }
}
