using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for PdfException

class Program
{
    static void Main()
    {
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Prepare a DataTable whose column names match the PDF form field names
        DataTable data = new DataTable();
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Rows.Add("John", "Doe");

        try
        {
            // AutoFiller implements IDisposable – use a using block for deterministic cleanup
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the PDF template; throws PdfException if the file cannot be opened
                filler.BindPdf(templatePath);

                // Import the data table into the form fields
                filler.ImportDataTable(data);

                // NOTE: In recent Aspose.Pdf versions the ImportResult property and
                // FormImportResult class have been removed. If you need detailed import
                // diagnostics you must use the newer API (e.g., AutoFiller.GetImportResult())
                // but for this example we simply proceed.

                // Save the filled PDF to a file via a stream using the newer Save(stream) overload
                using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    filler.Save(outStream);
                }
            }

            Console.WriteLine($"PDF successfully filled and saved to '{outputPath}'.");
        }
        catch (PdfException ex)               // Handles errors specific to Aspose.Pdf processing
        {
            Console.Error.WriteLine($"PDF processing error: {ex.Message}");
        }
        catch (FileNotFoundException ex)      // Handles missing template file
        {
            Console.Error.WriteLine($"Template file not found: {ex.FileName}");
        }
        catch (Exception ex)                  // Catch‑all for any other unexpected issues
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
