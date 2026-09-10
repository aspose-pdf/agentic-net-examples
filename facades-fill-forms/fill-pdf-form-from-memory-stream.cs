using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;          // <-- added for TextBoxField and other form field types
using Aspose.Pdf.Annotations;    // <-- added for Border and BorderStyle
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare data for the form fields – column names must match field names exactly
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName", typeof(string));
        data.Columns.Add("Address", typeof(string));

        // Add a single row of data (multiple rows are also supported)
        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"] = "Doe";
        row["Address"] = "123 Main St, Anytown";
        data.Rows.Add(row);

        // Create a PDF template in memory that contains form fields matching the DataTable columns
        byte[] templateBytes = CreatePdfTemplate(data);

        // Fill the PDF using AutoFiller and obtain the result as a byte array
        byte[] filledPdf = FillPdfTemplate(templateBytes, data);

        // For demonstration, write the result to a file (no intermediate files were used during filling)
        File.WriteAllBytes("filled_output.pdf", filledPdf);
        Console.WriteLine("PDF filled and saved to 'filled_output.pdf'.");
    }

    /// <summary>
    /// Generates a minimal PDF template containing a TextBoxField for each column in the supplied DataTable.
    /// The template is returned as a byte array.
    /// </summary>
    static byte[] CreatePdfTemplate(DataTable dataTable)
    {
        using (var doc = new Document())
        {
            // Add a single page – the fields will be placed vertically
            var page = doc.Pages.Add();

            // Simple layout parameters
            const float left = 100f;
            const float width = 300f;
            const float height = 20f;
            const float verticalSpacing = 30f;
            float yPos = 750f; // start near top of page

            foreach (DataColumn col in dataTable.Columns)
            {
                var rect = new Rectangle(left, yPos, left + width, yPos + height);
                var txtField = new TextBoxField(page, rect)
                {
                    PartialName = col.ColumnName,
                    Value = string.Empty
                };
                // Optional visual styling
                txtField.Border = new Border(txtField)
                {
                    Style = BorderStyle.Solid,
                    Width = 1
                };
                txtField.Color = Color.Black;
                doc.Form.Add(txtField);

                yPos -= verticalSpacing;
            }

            // Save the template to a memory stream and return the bytes
            using (var ms = new MemoryStream())
            {
                doc.Save(ms);
                return ms.ToArray();
            }
        }
    }

    /// <summary>
    /// Fills a PDF template using Aspose.Pdf.Facades.AutoFiller.
    /// The template is supplied as a byte array (memory stream) and the result is returned as a byte array.
    /// </summary>
    static byte[] FillPdfTemplate(byte[] templatePdfBytes, DataTable data)
    {
        using (var inputStream = new MemoryStream(templatePdfBytes))
        using (var outputStream = new MemoryStream())
        using (var autoFiller = new AutoFiller())
        {
            // Bind the PDF template from the input stream
            autoFiller.BindPdf(inputStream);

            // Import the data table; column names must match form field names (case‑sensitive)
            autoFiller.ImportDataTable(data);

            // Save the generated PDF to the output stream
            autoFiller.Save(outputStream);

            return outputStream.ToArray();
        }
    }
}
