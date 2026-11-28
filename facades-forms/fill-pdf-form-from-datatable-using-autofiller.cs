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
        // -------------------------------------------------
        // 1. Create a simple PDF template with a form field
        // -------------------------------------------------
        MemoryStream templateStream = new MemoryStream();
        using (Document templateDoc = new Document())
        {
            // Add a page to the document
            Page page = templateDoc.Pages.Add();

            // Define a text box form field named "Name"
            TextBoxField nameField = new TextBoxField(templateDoc, new Rectangle(100, 600, 300, 620))
            {
                PartialName = "Name"
            };

            // Add the field to the document's form (not directly to page.Annotations)
            // The second argument is the page number (1‑based)
            templateDoc.Form.Add(nameField, 1);

            // Save the template PDF into the memory stream
            templateDoc.Save(templateStream);
        }

        // Reset the stream position so AutoFiller can read from the beginning
        templateStream.Position = 0;

        // -------------------------------------------------
        // 2. Build a synthetic DataTable matching the field name
        // -------------------------------------------------
        DataTable data = new DataTable();
        data.Columns.Add("Name", typeof(string));
        DataRow row = data.NewRow();
        row["Name"] = "John Doe";
        data.Rows.Add(row);

        // -------------------------------------------------
        // 3. Fill the template using AutoFiller
        // -------------------------------------------------
        AutoFiller filler = new AutoFiller();
        filler.BindPdf(templateStream);          // Load the template from the memory stream
        filler.ImportDataTable(data);            // Import the DataTable values

        // -------------------------------------------------
        // 4. Save the filled PDF to an output MemoryStream
        // -------------------------------------------------
        MemoryStream outputStream = new MemoryStream();
        filler.Save(outputStream);               // Write the filled PDF into the stream
        filler.Close();                          // Release resources held by AutoFiller

        // -------------------------------------------------
        // 5. Persist the result as a local PDF file
        // -------------------------------------------------
        File.WriteAllBytes("filled_output.pdf", outputStream.ToArray());

        // Clean up streams
        templateStream.Dispose();
        outputStream.Dispose();
    }
}
