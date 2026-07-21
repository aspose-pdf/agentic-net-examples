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
        // -------------------------------------------------------------------
        // 1. Create a PDF template with the required form fields entirely in memory.
        // -------------------------------------------------------------------
        using (MemoryStream templateStream = new MemoryStream())
        {
            Document templateDoc = new Document();
            Page page = templateDoc.Pages.Add();

            // FirstName field
            TextBoxField firstName = new TextBoxField(templateDoc.Pages[1], new Rectangle(100, 700, 300, 720))
            {
                PartialName = "FirstName",
                Value = string.Empty
            };
            page.Paragraphs.Add(firstName);

            // LastName field
            TextBoxField lastName = new TextBoxField(templateDoc.Pages[1], new Rectangle(100, 660, 300, 680))
            {
                PartialName = "LastName",
                Value = string.Empty
            };
            page.Paragraphs.Add(lastName);

            // Email field
            TextBoxField email = new TextBoxField(templateDoc.Pages[1], new Rectangle(100, 620, 300, 640))
            {
                PartialName = "Email",
                Value = string.Empty
            };
            page.Paragraphs.Add(email);

            // Save the template into the memory stream and rewind it.
            templateDoc.Save(templateStream);
            templateStream.Position = 0;

            // -------------------------------------------------------------------
            // 2. Fill the template using AutoFiller without touching the file system.
            // -------------------------------------------------------------------
            using (MemoryStream filledPdfStream = new MemoryStream())
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the in‑memory template.
                autoFiller.BindPdf(templateStream);

                // Prepare data that matches the form field names.
                DataTable formData = new DataTable("FormData");
                formData.Columns.Add("FirstName", typeof(string));
                formData.Columns.Add("LastName", typeof(string));
                formData.Columns.Add("Email", typeof(string));

                DataRow row = formData.NewRow();
                row["FirstName"] = "John";
                row["LastName"] = "Doe";
                row["Email"] = "john.doe@example.com";
                formData.Rows.Add(row);

                // Import data and save the filled PDF to the output stream.
                autoFiller.ImportDataTable(formData);
                autoFiller.Save(filledPdfStream);

                // Example: write the result to a file (optional).
                File.WriteAllBytes("filled.pdf", filledPdfStream.ToArray());
            }
        }
    }
}
