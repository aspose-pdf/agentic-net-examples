using System;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Border and BorderStyle

class Program
{
    static void Main()
    {
        // Paths to the template PDF and the resulting filled PDF
        const string templatePdfPath = "template.pdf";
        const string outputPdfPath   = "filled_output.pdf";

        // -----------------------------------------------------------------
        // 1. Prepare a DataTable with data (in real scenarios this would come
        //    from a database, CSV, etc.).  Column names may NOT match the
        //    PDF field identifiers.
        // -----------------------------------------------------------------
        DataTable dataTable = new DataTable("FormData");
        dataTable.Columns.Add("ColFirstName", typeof(string));
        dataTable.Columns.Add("ColLastName",  typeof(string));
        dataTable.Columns.Add("ColAddress",   typeof(string));

        // Sample data rows
        dataTable.Rows.Add("John",  "Doe",   "123 Main St");
        dataTable.Rows.Add("Jane",  "Smith", "456 Oak Ave");

        // -----------------------------------------------------------------
        // 2. Ensure a PDF template with the required form fields exists.
        //    If it does not, create it on‑the‑fly.  The field PartialName must
        //    match the final column names that will be used for AutoFiller.
        // -----------------------------------------------------------------
        if (!File.Exists(templatePdfPath))
        {
            // The final column names after mapping (see step 4).
            var requiredFieldNames = new[] { "FirstName", "LastName", "Address" };

            using (Document templateDoc = new Document())
            {
                Page page = templateDoc.Pages.Add();
                float yPos = 750;
                const float left = 100, width = 300, height = 20, verticalSpacing = 30;

                foreach (string fieldName in requiredFieldNames)
                {
                    Rectangle rect = new Rectangle(left, yPos, left + width, yPos + height);
                    // Create the field first, then set its properties (cannot use the field
                    // inside its own initializer because the variable is not yet assigned).
                    TextBoxField txtField = new TextBoxField(page, rect);
                    txtField.PartialName = fieldName;
                    txtField.Value = string.Empty;
                    txtField.Border = new Border(txtField) { Style = BorderStyle.Solid, Width = 1 };
                    txtField.Color = Color.Black;
                    templateDoc.Form.Add(txtField);
                    yPos -= verticalSpacing;
                }

                templateDoc.Save(templatePdfPath);
            }
        }

        // -----------------------------------------------------------------
        // 3. Retrieve the field names from the PDF template.
        //    Document.Form provides the collection of fields.
        // -----------------------------------------------------------------
        string[] pdfFieldNames;
        using (Document tmplDoc = new Document(templatePdfPath))
        {
            pdfFieldNames = tmplDoc.Form.Fields.Select(f => f.PartialName).ToArray();
        }

        // -----------------------------------------------------------------
        // 4. Define a mapping from DataTable column names to PDF field names.
        //    This mapping should be built according to your business logic.
        // -----------------------------------------------------------------
        var columnToFieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "ColFirstName", "FirstName" },
            { "ColLastName",  "LastName"  },
            { "ColAddress",   "Address"   }
        };

        // -----------------------------------------------------------------
        // 5. Rename DataTable columns so that they exactly match the PDF
        //    field identifiers.  AutoFiller.ImportDataTable requires an
        //    exact case‑sensitive match.
        // -----------------------------------------------------------------
        foreach (var kvp in columnToFieldMap)
        {
            if (dataTable.Columns.Contains(kvp.Key))
            {
                dataTable.Columns[kvp.Key].ColumnName = kvp.Value;
            }
        }

        // Optional sanity check: ensure every required PDF field has a matching column.
        if (pdfFieldNames != null)
        {
            foreach (string fieldName in pdfFieldNames)
            {
                if (!dataTable.Columns.Contains(fieldName))
                {
                    Console.WriteLine($"Warning: No data column found for PDF field '{fieldName}'.");
                }
            }
        }

        // -----------------------------------------------------------------
        // 6. Use AutoFiller to bind the template, import the adjusted DataTable,
        //    and save the filled PDF.
        // -----------------------------------------------------------------
        using (AutoFiller autoFiller = new AutoFiller())
        {
            // Bind the template PDF.
            autoFiller.BindPdf(templatePdfPath);

            // Import the DataTable with correctly named columns.
            autoFiller.ImportDataTable(dataTable);

            // Save the resulting PDF (single merged document).
            autoFiller.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF form filled and saved to '{outputPdfPath}'.");
    }
}
