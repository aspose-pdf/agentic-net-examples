using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    // Maximum allowed PDF size in bytes (example: 5 MB)
    const long MaxPdfSizeBytes = 5 * 1024 * 1024;

    static void Main()
    {
        // Paths
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";
        const string extraPdfPath = "extra.pdf"; // optional PDF to insert pages from

        // Validate input files
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Template not found: {templatePath}");
            return;
        }

        // Prepare a DataTable with sample data
        DataTable table = new DataTable();
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Amount", typeof(decimal));
        table.Rows.Add("Alice", 123.45m);
        table.Rows.Add("Bob",   678.90m);
        // ... add more rows as needed ...

        // Fill the PDF form fields using the DataTable
        using (Document doc = new Document(templatePath))
        {
            // Example: assume the template has fields named "Name_1", "Amount_1", "Name_2", etc.
            // Iterate over rows and set corresponding fields.
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                string nameField   = $"Name_{i + 1}";
                string amountField = $"Amount_{i + 1}";

                // Set field values if they exist
                if (doc.Form != null && doc.Form.HasField(nameField))
                {
                    // Most simple text fields are TextBoxField; cast accordingly
                    if (doc.Form[nameField] is TextBoxField nameTxt)
                    {
                        nameTxt.Value = row["Name"].ToString();
                    }
                }

                if (doc.Form != null && doc.Form.HasField(amountField))
                {
                    if (doc.Form[amountField] is TextBoxField amountTxt)
                    {
                        amountTxt.Value = row["Amount"].ToString();
                    }
                }
            }

            // Save the filled document to a temporary file
            string tempPath = Path.GetTempFileName();
            doc.Save(tempPath);

            // OPTIONAL: Insert additional pages from another PDF using PdfFileEditor
            if (File.Exists(extraPdfPath))
            {
                // Insert all pages of extraPdfPath after the last page of the filled document
                PdfFileEditor editor = new PdfFileEditor();
                using (Document extraDoc = new Document(extraPdfPath))
                {
                    int pageCount = extraDoc.Pages.Count; // 1‑based count
                    int[] pagesToInsert = new int[pageCount];
                    for (int p = 1; p <= pageCount; p++)
                        pagesToInsert[p - 1] = p;

                    // Insert at the end of the original document (position = int.MaxValue)
                    editor.Insert(tempPath, int.MaxValue, extraPdfPath, pagesToInsert, outputPath);
                }
            }
            else
            {
                // No extra pages – just move temp file to final output
                File.Copy(tempPath, outputPath, true);
            }

            // Clean up temporary file
            File.Delete(tempPath);
        }

        // Validate the final PDF size
        FileInfo resultInfo = new FileInfo(outputPath);
        if (resultInfo.Length > MaxPdfSizeBytes)
        {
            Console.Error.WriteLine($"Error: PDF size {resultInfo.Length} bytes exceeds limit of {MaxPdfSizeBytes} bytes.");
        }
        else
        {
            Console.WriteLine($"Success: PDF size {resultInfo.Length} bytes is within the limit.");
        }
    }
}
