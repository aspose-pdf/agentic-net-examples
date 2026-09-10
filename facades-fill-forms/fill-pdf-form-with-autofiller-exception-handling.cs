using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class AutoFillerExample
{
    static void Main()
    {
        // Paths for the template PDF and the generated output PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_output.pdf";

        // Verify that the template file exists before proceeding
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Error: Template file not found – '{templatePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Prepare a simple DataTable with field names matching the PDF form
        // ------------------------------------------------------------
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Columns.Add("Address",   typeof(string));

        // Add a single row of sample data
        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St, Anytown";
        data.Rows.Add(row);

        // ------------------------------------------------------------
        // AutoFiller usage with robust exception handling
        // ------------------------------------------------------------
        try
        {
            // AutoFiller implements IDisposable via ISaveableFacade, so use a using block
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF file
                filler.BindPdf(templatePath);

                // Import the data table; this may throw if column names do not match field names
                filler.ImportDataTable(data);

                // In Aspose.PDF 26.6.0 the FillForm method is not required – the data is applied
                // when Save is called after ImportDataTable. If a future version re‑introduces
                // FillForm, the call can be added back without breaking the current code.

                // Save the merged result to the specified output file
                filler.Save(outputPath);
                Console.WriteLine($"PDF successfully generated: '{outputPath}'.");
            }
        }
        catch (FileNotFoundException fnfEx)
        {
            // Handles cases where the template file path is invalid
            Console.Error.WriteLine($"File error: {fnfEx.Message}");
        }
        catch (Aspose.Pdf.PdfException pdfEx)
        {
            // Handles generic Aspose.Pdf errors (e.g., missing form field, invalid PDF format)
            Console.Error.WriteLine($"PDF processing error: {pdfEx.Message}");
            if (pdfEx.InnerException != null)
            {
                Console.Error.WriteLine($"Inner error: {pdfEx.InnerException.Message}");
            }
        }
        catch (Exception ex)
        {
            // Catch‑all for any other unexpected exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
