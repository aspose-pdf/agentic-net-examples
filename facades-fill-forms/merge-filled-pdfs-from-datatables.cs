using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string templatePdfPath = "template.pdf";          // PDF form template
        const string mergedPdfPath   = "merged_output.pdf";    // Final consolidated PDF

        // Example: an array of DataTables, each containing data for one filled PDF
        DataTable[] dataTables = GetDataTables(); // Replace with actual data retrieval

        // Store temporary file names for the individual filled PDFs
        var tempFiles = new string[dataTables.Length];

        try
        {
            // Generate a filled PDF for each DataTable
            for (int i = 0; i < dataTables.Length; i++)
            {
                // Create a unique temporary file name
                string tempFile = Path.Combine(Path.GetTempPath(),
                                               $"filled_{Guid.NewGuid()}.pdf");
                tempFiles[i] = tempFile;

                // Use AutoFiller to bind the template, import data, and save the result
                using (Aspose.Pdf.Facades.AutoFiller filler = new Aspose.Pdf.Facades.AutoFiller())
                {
                    filler.BindPdf(templatePdfPath);          // Bind the PDF form template
                    filler.ImportDataTable(dataTables[i]);   // Fill fields with DataTable data
                    filler.Save(tempFile);                   // Save the filled PDF to temp file
                }
            }

            // Concatenate all temporary PDFs into a single document
            Aspose.Pdf.Facades.PdfFileEditor editor = new Aspose.Pdf.Facades.PdfFileEditor();
            editor.Concatenate(tempFiles, mergedPdfPath); // Output merged PDF

            Console.WriteLine($"Merged PDF created at '{mergedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            foreach (string file in tempFiles)
            {
                if (!string.IsNullOrEmpty(file) && File.Exists(file))
                {
                    try { File.Delete(file); } catch { /* ignore cleanup errors */ }
                }
            }
        }
    }

    // Placeholder method – replace with actual logic to obtain DataTables
    static DataTable[] GetDataTables()
    {
        // Example with two dummy tables
        var table1 = new DataTable("FormData1");
        table1.Columns.Add("FirstName", typeof(string));
        table1.Columns.Add("LastName", typeof(string));
        table1.Rows.Add("John", "Doe");

        var table2 = new DataTable("FormData2");
        table2.Columns.Add("FirstName", typeof(string));
        table2.Columns.Add("LastName", typeof(string));
        table2.Rows.Add("Jane", "Smith");

        return new[] { table1, table2 };
    }
}