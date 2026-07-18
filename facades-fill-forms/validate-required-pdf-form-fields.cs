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
        const string inputPdfPath = "template.pdf";   // PDF with AcroForm (generated inline)
        const string outputPdfPath = "filled.pdf";    // Result PDF

        // ---------------------------------------------------------------------
        // 1️⃣ Create a minimal PDF containing the required form fields.
        //    This guarantees the file exists in the sandbox at runtime.
        // ---------------------------------------------------------------------
        CreateTemplatePdf(inputPdfPath);

        // Assume dataTable is populated elsewhere
        DataTable dataTable = GetDataTable(); // placeholder for actual data source

        // Load the PDF form using the Form facade (fully qualified to avoid ambiguity)
        using (Aspose.Pdf.Facades.Form pdfForm = new Aspose.Pdf.Facades.Form(inputPdfPath))
        {
            // Validate that every required field has a matching column in the DataTable
            foreach (string fieldName in pdfForm.FieldNames)
            {
                // Check if the field is marked as required
                if (pdfForm.IsRequiredField(fieldName))
                {
                    // DataColumnCollection.Contains is case‑sensitive; field names must match exactly
                    if (!dataTable.Columns.Contains(fieldName))
                    {
                        throw new InvalidOperationException(
                            $"Required field '{fieldName}' does not have a matching column in the DataTable.");
                    }
                }
            }

            // All required fields are present – proceed with import using AutoFiller
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the same PDF template to the AutoFiller
                filler.BindPdf(inputPdfPath);

                // Import the DataTable; column names must match field names (case‑sensitive)
                filler.ImportDataTable(dataTable);

                // Save the filled PDF to the desired output path
                filler.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Form fields validated and data imported successfully to '{outputPdfPath}'.");
    }

    // ---------------------------------------------------------------------
    // Helper: generate a PDF with AcroForm fields that the example expects.
    // ---------------------------------------------------------------------
    private static void CreateTemplatePdf(string path)
    {
        // Create a new empty document and add a single page.
        Document doc = new Document();
        doc.Pages.Add();

        // Add three required text box fields: FirstName, LastName, Email.
        // The field names must match the column names used in GetDataTable().
        AddRequiredTextBox(doc, "FirstName", 100, 700, 200, 720);
        AddRequiredTextBox(doc, "LastName", 100, 650, 200, 670);
        AddRequiredTextBox(doc, "Email", 100, 600, 200, 620);

        // Save the template so it can be opened later.
        doc.Save(path);
    }

    private static void AddRequiredTextBox(Document doc, string name, double llx, double lly, double urx, double ury)
    {
        // Create a TextBoxField, set its name, required flag and a default appearance.
        TextBoxField txt = new TextBoxField(doc.Pages[1], new Aspose.Pdf.Rectangle(llx, lly, urx, ury))
        {
            PartialName = name,
            Required = true,
            Value = string.Empty
            // Visual styling (border, background) is optional and omitted to keep the code compatible
            // with all supported Aspose.Pdf versions.
        };
        // Add the field to the document's form collection.
        doc.Form.Add(txt, 1);
    }

    // Mock method to obtain a populated DataTable.
    // Replace with actual data retrieval logic.
    static DataTable GetDataTable()
    {
        DataTable table = new DataTable("FormData");
        // Example columns – ensure they match required field names in the PDF
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Email", typeof(string));
        // Add sample rows
        table.Rows.Add("John", "Doe", "john.doe@example.com");
        table.Rows.Add("Jane", "Smith", "jane.smith@example.com");
        return table;
    }
}
