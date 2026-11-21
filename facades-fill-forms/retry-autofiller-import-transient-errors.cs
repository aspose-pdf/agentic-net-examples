using System;
using System.Data;
using System.IO;
using System.Threading;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "template.pdf";      // PDF with form fields
        const string dataFilePath   = "formdata.xml";      // source data for AutoFiller (XML)
        const string outputPdfPath  = "filled_output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(dataFilePath))
        {
            Console.Error.WriteLine($"Data file not found: {dataFilePath}");
            return;
        }

        // Load the PDF document inside a using block (mandatory for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load XML data into a DataTable – AutoFiller works with DataTable/DataSet, not directly with XML files
            DataTable dataTable = LoadDataTableFromXml(dataFilePath);
            if (dataTable == null)
            {
                Console.Error.WriteLine("Failed to load data from XML.");
                return;
            }

            // AutoFiller does NOT implement IDisposable in older versions, so we do NOT wrap it in using
            AutoFiller filler = new AutoFiller();

            const int maxRetries = 3;
            const int delayMs    = 500; // milliseconds

            bool importSucceeded = false;
            for (int attempt = 1; attempt <= maxRetries && !importSucceeded; attempt++)
            {
                try
                {
                    // Bind the PDF document to the filler and import the data table
                    filler.BindPdf(pdfDoc);
                    filler.ImportDataTable(dataTable);
                    importSucceeded = true; // success, exit loop
                }
                catch (IOException ex) when (IsTransient(ex))
                {
                    Console.WriteLine($"Transient I/O error on attempt {attempt}: {ex.Message}");
                    if (attempt < maxRetries)
                        Thread.Sleep(delayMs);
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Access error on attempt {attempt}: {ex.Message}");
                    if (attempt < maxRetries)
                        Thread.Sleep(delayMs);
                }
                catch (Exception ex)
                {
                    // Non‑transient exception – rethrow
                    Console.Error.WriteLine($"Fatal error during AutoFiller import: {ex.Message}");
                    throw;
                }
            }

            if (!importSucceeded)
            {
                Console.Error.WriteLine("Failed to import data after multiple attempts.");
                return;
            }

            // Save the filled PDF using AutoFiller
            filler.Save(outputPdfPath);
            Console.WriteLine($"Form filled PDF saved to '{outputPdfPath}'.");
        }
    }

    // Load the first DataTable from an XML file (the format expected by AutoFiller)
    private static DataTable LoadDataTableFromXml(string xmlPath)
    {
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlPath);
            // AutoFiller expects a table where the first column is the field name and the second column is the value
            return ds.Tables.Count > 0 ? ds.Tables[0] : null;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading XML data file: {ex.Message}");
            return null;
        }
    }

    // Determines whether an IOException is likely transient (e.g., file locked, sharing violation)
    private static bool IsTransient(IOException ex)
    {
        // HResult 0x20 (32) = sharing violation, 0x21 (33) = lock violation, etc.
        const int ERROR_SHARING_VIOLATION = unchecked((int)0x80070020);
        const int ERROR_LOCK_VIOLATION    = unchecked((int)0x80070021);
        return ex.HResult == ERROR_SHARING_VIOLATION ||
               ex.HResult == ERROR_LOCK_VIOLATION;
    }
}
