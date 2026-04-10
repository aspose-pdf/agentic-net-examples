using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled_output.pdf";

        // Prepare a simple DataTable with field names matching the PDF form fields.
        DataTable data = new DataTable("FormData");
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));

        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        data.Rows.Add(row);

        try
        {
            // Create and configure the AutoFiller.
            using (AutoFiller autoFiller = new AutoFiller())
            {
                // Bind the template PDF file.
                autoFiller.BindPdf(templatePath);

                // Import the data table into the form.
                autoFiller.ImportDataTable(data);

                // NOTE: In recent versions of Aspose.PDF the AutoFiller class does not expose an
                // ImportResult collection. If you need detailed import diagnostics, use the
                // ImportDataTable overload that returns an ImportResultCollection, or inspect the
                // PDF manually after filling. For this example we simply proceed.

                // Save the filled PDF to the specified output file.
                autoFiller.Save(outputPath);
            }

            Console.WriteLine("PDF filled and saved successfully.");
        }
        catch (FileNotFoundException ex)
        {
            // Handles missing template file errors.
            Console.Error.WriteLine($"Template file not found: {ex.FileName}");
        }
        catch (PdfException ex)
        {
            // Handles PDF processing errors, such as invalid PDF format.
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handles any other unexpected errors.
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
