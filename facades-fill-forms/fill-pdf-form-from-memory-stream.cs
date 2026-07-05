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
        // ---------------------------------------------------------------------
        // 1. Create a PDF template *in memory* that contains the form fields we
        //    want to fill. This removes the need for an external "template.pdf"
        //    file and prevents FileNotFoundException in the sandbox.
        // ---------------------------------------------------------------------
        byte[] templateBytes;
        using (var templateDoc = new Document())
        {
            var page = templateDoc.Pages.Add();

            // Create three text box fields whose PartialName matches the DataTable columns
            var firstNameField = new TextBoxField(templateDoc)
            {
                PartialName = "FirstName",
                Rect = new Rectangle(100, 700, 300, 720) // left, bottom, right, top
            };
            var lastNameField = new TextBoxField(templateDoc)
            {
                PartialName = "LastName",
                Rect = new Rectangle(100, 650, 300, 670)
            };
            var emailField = new TextBoxField(templateDoc)
            {
                PartialName = "Email",
                Rect = new Rectangle(100, 600, 300, 620)
            };

            // Add the fields to the page
            page.Paragraphs.Add(firstNameField);
            page.Paragraphs.Add(lastNameField);
            page.Paragraphs.Add(emailField);

            // Save the template to a byte array
            using (var ms = new MemoryStream())
            {
                templateDoc.Save(ms);
                templateBytes = ms.ToArray();
            }
        }

        // ---------------------------------------------------------------------
        // 2. Prepare the data that will be merged into the form fields.
        // ---------------------------------------------------------------------
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName", typeof(string));
        data.Columns.Add("Email", typeof(string));

        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Email"]     = "john.doe@example.com";
        data.Rows.Add(row);

        // ---------------------------------------------------------------------
        // 3. Fill the PDF using AutoFiller, working completely with streams.
        // ---------------------------------------------------------------------
        using (var inputStream = new MemoryStream(templateBytes))
        using (var autoFiller = new AutoFiller())
        {
            // Bind the in‑memory template
            autoFiller.BindPdf(inputStream);

            // Import the DataTable – column names must match field names (case‑sensitive)
            autoFiller.ImportDataTable(data);

            // Save the filled PDF to another memory stream
            using (var outputStream = new MemoryStream())
            {
                autoFiller.Save(outputStream);

                // Optional: write the result to a file so you can inspect it locally.
                File.WriteAllBytes("filled_output.pdf", outputStream.ToArray());
            }
        }
    }
}
