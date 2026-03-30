using System;
using System.IO;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Form field classes

class Program
{
    static void Main()
    {
        const string outputPdfPath = "filled.pdf";

        // ---------------------------------------------------------------------
        // 1. Create a simple PDF template in memory with form fields that match the
        //    column names of the DataTable (FirstName, LastName).
        // ---------------------------------------------------------------------
        Document templateDoc = new Document();
        Page page = templateDoc.Pages.Add();

        // FirstName field
        var firstNameField = new TextBoxField(templateDoc.Pages[1], new Rectangle(100, 700, 300, 720))
        {
            PartialName = "FirstName",
            Value = ""
        };
        // Add the field to the document's form collection (not directly to page annotations)
        templateDoc.Form.Add(firstNameField);

        // LastName field
        var lastNameField = new TextBoxField(templateDoc.Pages[1], new Rectangle(100, 650, 300, 670))
        {
            PartialName = "LastName",
            Value = ""
        };
        templateDoc.Form.Add(lastNameField);

        // Save the template to a memory stream (no file is written).
        using (MemoryStream templateStream = new MemoryStream())
        {
            templateDoc.Save(templateStream);
            // Reset the position so it can be read from the beginning.
            templateStream.Position = 0;

            // -----------------------------------------------------------------
            // 2. Prepare the data that will be merged into the PDF form.
            // -----------------------------------------------------------------
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("FirstName", typeof(string));
            dataTable.Columns.Add("LastName", typeof(string));
            DataRow row = dataTable.NewRow();
            row["FirstName"] = "John";
            row["LastName"] = "Doe";
            dataTable.Rows.Add(row);

            // -----------------------------------------------------------------
            // 3. Use AutoFiller to bind the PDF from the memory stream, import the
            //    data table and save the filled PDF directly to another memory
            //    stream – again without touching the file system.
            // -----------------------------------------------------------------
            using (AutoFiller filler = new AutoFiller())
            {
                filler.BindPdf(templateStream);
                filler.ImportDataTable(dataTable);

                using (MemoryStream outputStream = new MemoryStream())
                {
                    filler.Save(outputStream);
                    // Persist the final PDF to disk (this is the only file operation).
                    File.WriteAllBytes(outputPdfPath, outputStream.ToArray());
                }
            }
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}
