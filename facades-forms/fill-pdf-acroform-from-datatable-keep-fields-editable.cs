using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // needed for Border and BorderStyle
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF (with an AcroForm) and the result PDF
        const string inputPdf = "template.pdf";
        const string outputPdf = "filled.pdf";

        // Fields that must stay editable after the fill operation
        string[] unflattenFields = new[] { "CustomerName", "InvoiceDate" };

        // -------------------------------------------------
        // Ensure a PDF template exists with matching form fields
        // -------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            // Create a minimal one‑page PDF and add a TextBoxField for each column name
            var doc = new Document();
            var page = doc.Pages.Add();
            float yPos = 750f;               // start near top of page
            const float left = 100f;
            const float width = 300f;
            const float height = 20f;
            const float verticalSpacing = 30f;

            foreach (string fieldName in unflattenFields)
            {
                var rect = new Rectangle(left, yPos, left + width, yPos + height);
                var txtField = new TextBoxField(page, rect)
                {
                    PartialName = fieldName,
                    Value = string.Empty,
                    Color = Color.Black // field border colour (the actual border colour is set via the Border object)
                };
                // Set the border – must be done after the field variable is created
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };
                // Add the field to the document's form collection (not directly to page annotations)
                doc.Form.Add(txtField);
                yPos -= verticalSpacing;
            }

            doc.Save(inputPdf);
        }

        // -------------------------------------------------
        // Build a synthetic DataTable that matches the form
        // -------------------------------------------------
        DataTable formData = new DataTable();
        foreach (string field in unflattenFields)
        {
            formData.Columns.Add(field, typeof(string));
        }
        DataRow row = formData.NewRow();
        row["CustomerName"] = "Acme Corp.";
        row["InvoiceDate"] = DateTime.Today.ToShortDateString();
        formData.Rows.Add(row);

        // -------------------------------------------------
        // Fill the AcroForm using AutoFiller
        // -------------------------------------------------
        var filler = new AutoFiller();
        filler.BindPdf(inputPdf);
        // Specify which fields should stay editable (not flattened)
        filler.UnFlattenFields = unflattenFields;
        // Import the DataTable – each column maps to a field with the same PartialName
        filler.ImportDataTable(formData);
        // Save the filled PDF. The fields listed in UnFlattenFields remain editable.
        filler.Save(outputPdf);
        filler.Close();

        Console.WriteLine($"PDF filled and saved to '{outputPdf}'. Fields {string.Join(", ", unflattenFields)} remain editable.");
    }
}
