using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // needed for form field handling

namespace AsposePdfApi
{
    /// <summary>
    /// Simple file logger that appends timestamped messages to a log file.
    /// </summary>
    internal static class Logger
    {
        private static readonly object _lock = new();
        // Initialise with an empty string to satisfy the non‑nullable warning.
        private static string _logFilePath = string.Empty;

        public static void Init(string logFilePath)
        {
            _logFilePath = logFilePath;
            // Ensure a fresh log for each run
            try
            {
                if (File.Exists(_logFilePath))
                    File.Delete(_logFilePath);
            }
            catch { /* ignore */ }
        }

        public static void Info(string message)
        {
            Write($"INFO [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
        }

        private static void Write(string line)
        {
            lock (_lock)
            {
                try
                {
                    File.AppendAllText(_logFilePath, line + Environment.NewLine);
                }
                catch
                {
                    // In a real‑world scenario we would handle I/O errors appropriately.
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // -----------------------------------------------------------------
            // 1. Configuration
            // -----------------------------------------------------------------
            const string templatePdfPath = "template.pdf";          // PDF with form fields matching column names
            const string outputDir       = "GeneratedPages";       // Folder for per‑row PDFs
            const string mergedPdfPath   = "merged_output.pdf";    // Final merged document
            const string logFilePath     = "process.log";          // Log file

            // Ensure the output directory exists
            Directory.CreateDirectory(outputDir);
            Logger.Init(Path.Combine(outputDir, logFilePath));
            Logger.Info("--- PDF generation started ---");

            // -----------------------------------------------------------------
            // 2. Prepare a sample DataTable (replace with real data source)
            // -----------------------------------------------------------------
            DataTable table = new DataTable();
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("Email", typeof(string));

            // Sample rows – in production you would fill the table from a DB, CSV, etc.
            table.Rows.Add("John", "Doe", "john.doe@example.com");
            table.Rows.Add("Jane", "Smith", "jane.smith@example.com");
            table.Rows.Add("Bob", "Johnson", "bob.johnson@example.com");

            // -----------------------------------------------------------------
            // 3. Process each row: fill the template and save a single‑page PDF
            // -----------------------------------------------------------------
            int rowIndex = 0;
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;
                string outputPdfPath = Path.Combine(outputDir, $"page_{rowIndex}.pdf");

                try
                {
                    // Load the template PDF
                    Document pdfDocument = new Document(templatePdfPath);

                    // Fill form fields – field names must match column names
                    foreach (DataColumn col in table.Columns)
                    {
                        string fieldName = col.ColumnName;
                        string fieldValue = row[col]?.ToString() ?? string.Empty;

                        // Retrieve the field from the PDF form collection
                        var field = pdfDocument.Form[fieldName] as Field;
                        if (field != null)
                        {
                            // Set the value – PartialName is read‑only, no need to assign it.
                            field.Value = fieldValue;
                        }
                    }

                    // Flatten the form so the values become part of the page content (optional)
                    pdfDocument.Form.Flatten();

                    // Save the per‑row PDF
                    pdfDocument.Save(outputPdfPath);

                    // Log the processed row and the page number created (each PDF has a single page)
                    Logger.Info($"Row {rowIndex} processed – generated page {rowIndex} at '{outputPdfPath}'.");
                }
                catch (Exception ex)
                {
                    Logger.Info($"Error processing row {rowIndex}: {ex.Message}");
                }
            }

            // -----------------------------------------------------------------
            // 4. Merge all generated PDFs into a single document
            // -----------------------------------------------------------------
            try
            {
                PdfFileEditor pdfEditor = new PdfFileEditor();
                string[] filesToMerge = Directory.GetFiles(outputDir, "page_*.pdf");
                Array.Sort(filesToMerge); // Ensure deterministic order
                pdfEditor.Concatenate(filesToMerge, mergedPdfPath);
                Logger.Info($"Merged {filesToMerge.Length} pages into '{mergedPdfPath}'.");
            }
            catch (Exception ex)
            {
                Logger.Info($"Error during merge: {ex.Message}");
            }

            Logger.Info("--- PDF generation completed ---");
        }
    }
}
