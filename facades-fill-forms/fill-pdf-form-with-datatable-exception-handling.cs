using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the template PDF and the resulting filled PDF
        const string templatePath = "template.pdf";
        const string outputPath   = "filled.pdf";

        // Build a DataTable whose column names match the form field names in the PDF
        DataTable data = new DataTable();
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Columns.Add("Age",       typeof(string));

        DataRow row = data.NewRow();
        row["FirstName"] = "John";
        row["LastName"]  = "Doe";
        row["Age"]       = "30";
        data.Rows.Add(row);

        try
        {
            // AutoFiller implements IDisposable – use a using block for deterministic cleanup
            using (AutoFiller filler = new AutoFiller())
            {
                // Bind the template PDF file (throws FileNotFoundException if missing)
                filler.BindPdf(templatePath);

                // Import the DataTable – fields that exist in the PDF will be filled
                filler.ImportDataTable(data);

                // NOTE: In recent versions of Aspose.Pdf the ImportResult property was removed.
                // If you need detailed import information, use the overload that returns a
                // FormImportResult[] directly:
                // FormImportResult[] importResults = filler.ImportDataTable(data);
                // foreach (var result in importResults)
                //     Console.WriteLine(result);
                // For this example we simply proceed to save the document.

                // Save the merged output PDF to a file
                filler.Save(outputPath);
            }

            Console.WriteLine($"PDF successfully saved to '{outputPath}'.");
        }
        // Specific exception when the template file cannot be located
        catch (FileNotFoundException ex)
        {
            Console.Error.WriteLine($"Template file not found: {ex.FileName}");
        }
        // General Aspose.Pdf errors (e.g., corrupted PDF, permission issues, invalid format)
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Aspose.Pdf error: {ex.Message}");
        }
        // Fallback for any other unexpected errors
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
