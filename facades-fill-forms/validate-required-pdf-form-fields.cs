using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Path to the PDF form template (created at runtime if missing)
    private const string PdfFormPath = "TemplateForm.pdf";

    static void Main()
    {
        // Ensure the template PDF exists – create a minimal one if it does not.
        if (!File.Exists(PdfFormPath))
        {
            CreatePdfTemplate(PdfFormPath);
        }

        // Example DataTable that would come from a database or other source
        DataTable data = GetSampleDataTable();

        // Validate that every required form field has a matching column in the DataTable
        ValidateRequiredFields(PdfFormPath, data);

        // If validation passes, you can proceed with filling the form (not shown here)
        Console.WriteLine("All required fields are present in the DataTable.");
    }

    // Creates a very small PDF containing the fields we need for the demo.
    private static void CreatePdfTemplate(string path)
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Helper to add a TextBoxField with the required flag.
        void AddTextBox(string name, bool required, float llx, float lly, float urx, float ury)
        {
            TextBoxField field = new TextBoxField(page, new Rectangle(llx, lly, urx, ury));
            field.PartialName = name;
            // In older Aspose.Pdf versions the property is "Required" instead of "IsRequired".
            // Use reflection to set whichever property exists.
            var requiredProp = field.GetType().GetProperty("IsRequired") ?? field.GetType().GetProperty("Required");
            if (requiredProp != null && requiredProp.CanWrite)
            {
                requiredProp.SetValue(field, required);
            }
            // The Add overload that accepts a page number expects a valid page index (1‑based).
            // The document has only one page, so we always pass 1.
            doc.Form.Add(field, 1);
        }

        // Required fields
        AddTextBox("FirstName", true, 100, 700, 300, 720);
        AddTextBox("LastName", true, 100, 650, 300, 670);
        AddTextBox("Email", true, 100, 600, 300, 620);
        // Non‑required field
        AddTextBox("Comments", false, 100, 500, 400, 540);

        doc.Save(path);
    }

    // Creates a sample DataTable for demonstration purposes
    private static DataTable GetSampleDataTable()
    {
        DataTable table = new DataTable("FormData");
        // Column names must match the PDF field names (case‑sensitive)
        table.Columns.Add("FirstName", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Email", typeof(string));
        table.Columns.Add("Comments", typeof(string));

        // Populate with a single row (optional)
        DataRow row = table.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Email"] = "john.doe@example.com";
        row["Comments"] = "Sample comment";
        table.Rows.Add(row);

        return table;
    }

    // Checks that each required field in the PDF form exists as a column in the DataTable
    private static void ValidateRequiredFields(string pdfPath, DataTable dataTable)
    {
        Document doc = new Document(pdfPath);
        List<string> missingFields = new List<string>();

        foreach (Field field in doc.Form.Fields)
        {
            // Most form field types expose a "Required" property (older versions) or "IsRequired" (newer).
            // Use reflection to support both without breaking older SDKs.
            bool isRequired = false;
            var requiredProp = field.GetType().GetProperty("IsRequired") ?? field.GetType().GetProperty("Required");
            if (requiredProp != null)
            {
                var value = requiredProp.GetValue(field);
                if (value != null)
                {
                    isRequired = (bool)value;
                }
            }

            if (isRequired)
            {
                string fieldName = field.PartialName; // the programmatic name of the field
                if (!dataTable.Columns.Contains(fieldName))
                {
                    missingFields.Add(fieldName);
                }
            }
        }

        if (missingFields.Count > 0)
        {
            string message = "The following required form fields are missing in the DataTable: " +
                             string.Join(", ", missingFields);
            throw new InvalidOperationException(message);
        }
    }
}
