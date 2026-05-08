using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for exception types

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

        // Prepare a DataTable whose column names match the PDF field names (case‑sensitive)
        DataTable data = new DataTable();
        data.Columns.Add("FirstName", typeof(string));
        data.Columns.Add("LastName",  typeof(string));
        data.Columns.Add("Address",   typeof(string));

        // Example row – in real scenarios this would come from a database or other source
        data.Rows.Add("John", "Doe", "123 Main St.");

        // AutoFiller implements IDisposable via its facade base, so we can use a using block
        using (AutoFiller filler = new AutoFiller())
        {
            try
            {
                // Bind the template PDF
                filler.BindPdf(templatePath);

                // Import the data; any mismatch between column names and PDF fields will raise an exception
                filler.ImportDataTable(data);

                // Save the merged result to a file
                filler.Save(outputPath);

                Console.WriteLine($"Success: Filled PDF saved to '{outputPath}'.");
            }
            // Specific handling for a missing template file (already checked, but kept for completeness)
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine($"File error: {ex.Message}");
            }
            // Handles cases where the PDF cannot be parsed (corrupt or wrong format)
            catch (InvalidPdfFileFormatException ex)
            {
                Console.Error.WriteLine($"Invalid PDF format: {ex.Message}");
            }
            // General PDF processing errors (e.g., missing required fields)
            catch (PdfException ex)
            {
                Console.Error.WriteLine($"PDF processing error: {ex.Message}");
            }
            // Fallback for any other unexpected exceptions
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}