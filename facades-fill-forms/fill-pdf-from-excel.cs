using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

namespace PdfFillConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Create a simple PDF template in memory with a single text field named "CompanyName".
            using (MemoryStream templateStream = new MemoryStream())
            {
                using (Document templateDoc = new Document())
                {
                    Page page = templateDoc.Pages.Add();
                    Rectangle fieldRect = new Rectangle(100, 700, 300, 730);
                    TextBoxField textField = new TextBoxField(page, fieldRect);
                    textField.PartialName = "CompanyName";
                    templateDoc.Form.Add(textField);
                    // Save the template PDF to the memory stream.
                    templateDoc.Save(templateStream);
                }

                // Reset the stream position so AutoFiller can read from the beginning.
                templateStream.Position = 0;

                // 2. Build a DataTable that matches the form field name.
                DataTable dataTable = new DataTable("FormData");
                DataColumn column = new DataColumn("CompanyName", typeof(string));
                dataTable.Columns.Add(column);
                DataRow row = dataTable.NewRow();
                row["CompanyName"] = "Acme Corporation";
                dataTable.Rows.Add(row);

                // 3. Use AutoFiller to bind the template and fill the fields.
                using (MemoryStream filledStream = new MemoryStream())
                {
                    using (AutoFiller filler = new AutoFiller())
                    {
                        filler.BindPdf(templateStream);
                        filler.ImportDataTable(dataTable);
                        filler.Save(filledStream);
                    }

                    // Reset the stream position before saving to file.
                    filledStream.Position = 0;
                    // 4. Save the filled PDF to disk.
                    using (FileStream fileStream = new FileStream("filled.pdf", FileMode.Create, FileAccess.Write))
                    {
                        filledStream.CopyTo(fileStream);
                    }
                }
            }

            Console.WriteLine("PDF filled and saved as 'filled.pdf'.");
        }
    }
}
