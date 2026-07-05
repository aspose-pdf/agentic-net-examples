using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";   // PDF with form fields
        const string outputPath   = "filled_output.pdf";

        // Verify that the template file exists before proceeding
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Error: Template file not found – '{templatePath}'.");
            return;
        }

        // Build a DataTable whose column names match the PDF field names (case‑sensitive)
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Columns.Add("Address",   typeof(string));
        data.Columns.Add("City",      typeof(string));
        data.Columns.Add("Country",   typeof(string));

        // Populate a single row with sample data
        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Address"]   = "123 Main St.";
        row["City"]      = "Metropolis";
        row["Country"]   = "Neverland";
        data.Rows.Add(row);

        // AutoFiller implements IDisposable – use a using block for deterministic cleanup
        try
        {
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the PDF template (throws if the file cannot be opened)
                filler.BindPdf(templatePath);

                // Import the data; any mismatch between column names and PDF fields
                // will raise a PdfException which we catch below
                filler.ImportDataTable(data);

                // Save the merged result to a file
                filler.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully filled and saved to '{outputPath}'.");
        }
        // Specific handling for missing template or inaccessible file
        catch (FileNotFoundException fnfEx)
        {
            Console.Error.WriteLine($"File error: {fnfEx.Message}");
        }
        // Aspose.Pdf throws PdfException for format problems, missing fields, etc.
        catch (PdfException pdfEx)
        {
            Console.Error.WriteLine($"PDF processing error: {pdfEx.Message}");
            if (pdfEx.InnerException != null)
                Console.Error.WriteLine($"Inner error: {pdfEx.InnerException.Message}");
        }
        // General fallback for any other unexpected errors
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
