using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Required for form field classes

class AutoFillPdfExample
{
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Obtain the PDF template as a byte array.
        //    In a real scenario this could come from a DB, an API, or an
        //    embedded resource. For the purpose of a self‑contained demo
        //    we generate a minimal PDF with a single text box field if the
        //    template file does not exist.
        // ------------------------------------------------------------
        byte[] sourcePdfBytes;
        const string templatePath = "Template.pdf";
        if (File.Exists(templatePath))
        {
            sourcePdfBytes = File.ReadAllBytes(templatePath);
        }
        else
        {
            // Create a simple PDF with form fields in memory
            using (var doc = new Document())
            {
                var page = doc.Pages.Add();

                // Create a text box field named "FirstName"
                var firstNameField = new TextBoxField(page, new Rectangle(100, 600, 300, 620))
                {
                    PartialName = "FirstName",
                    Value = string.Empty
                };

                // Create a text box field named "LastName"
                var lastNameField = new TextBoxField(page, new Rectangle(100, 560, 300, 580))
                {
                    PartialName = "LastName",
                    Value = string.Empty
                };

                // Create a text box field named "Email"
                var emailField = new TextBoxField(page, new Rectangle(100, 520, 300, 540))
                {
                    PartialName = "Email",
                    Value = string.Empty
                };

                // **Correct way** – add fields to the document's Form collection,
                // not directly to page.Annotations.
                doc.Form.Add(firstNameField);
                doc.Form.Add(lastNameField);
                doc.Form.Add(emailField);

                using (var ms = new MemoryStream())
                {
                    doc.Save(ms);
                    sourcePdfBytes = ms.ToArray();
                }
            }
        }

        // ------------------------------------------------------------
        // 2. Fill the PDF using AutoFiller – all operations stay in memory.
        // ------------------------------------------------------------
        using (var sourceStream = new MemoryStream(sourcePdfBytes))
        using (var outputStream = new MemoryStream())
        using (var autoFiller = new AutoFiller())
        {
            // Bind the template PDF from the memory stream (no file I/O)
            autoFiller.BindPdf(sourceStream);

            // Prepare data that matches the form field names (case‑sensitive)
            var data = new DataTable("FormData");
            data.Columns.Add("FirstName", typeof(string));
            data.Columns.Add("LastName", typeof(string));
            data.Columns.Add("Email", typeof(string));

            var row = data.NewRow();
            row["FirstName"] = "John";
            row["LastName"] = "Doe";
            row["Email"] = "john.doe@example.com";
            data.Rows.Add(row);

            // Import the data into the PDF form fields
            autoFiller.ImportDataTable(data);

            // Save the filled PDF to the output memory stream
            autoFiller.Save(outputStream);

            // Optional: write the result to a file for verification
            File.WriteAllBytes("FilledOutput.pdf", outputStream.ToArray());
        }
    }
}
