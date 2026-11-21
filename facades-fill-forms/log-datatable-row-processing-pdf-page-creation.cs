using System;
using System.Data;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input DataTable (replace with actual data source)
        DataTable table = GetSampleDataTable();

        // Log file path
        string logPath = "process.log";

        // Output PDF path
        string outputPdf = "output.pdf";

        // Ensure log file is created/cleared
        using (StreamWriter logWriter = new StreamWriter(logPath, false))
        {
            logWriter.WriteLine($"Processing started at {DateTime.Now}");
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            int rowIndex = 0;

            // Process each DataRow
            foreach (DataRow row in table.Rows)
            {
                rowIndex++;

                // Add a new page for the current row
                Page page = pdfDoc.Pages.Add();

                // Simple text representation of the row (customize as needed)
                string rowText = $"Row {rowIndex}";
                foreach (DataColumn col in table.Columns)
                {
                    rowText += $"\n{col.ColumnName}: {row[col]}";
                }

                // Add the text to the page
                TextFragment tf = new TextFragment(rowText);
                tf.Position = new Position(50, 750); // top-left margin
                page.Paragraphs.Add(tf);

                // Log the processing information
                using (StreamWriter logWriter = new StreamWriter(logPath, true))
                {
                    logWriter.WriteLine($"Row {rowIndex} processed – page {pdfDoc.Pages.Count} created.");
                }
            }

            // Save the PDF (Document.Save inside using ensures proper disposal)
            pdfDoc.Save(outputPdf);
        }

        // Example usage of a Facades class (PdfFileEditor) – copy the generated PDF to another file
        PdfFileEditor editor = new PdfFileEditor();
        string copyPdf = "output_copy.pdf";
        // Concatenate overload expects an array of source file paths (string[]) or Document[]
        // Using the string[] overload here
        editor.Concatenate(new string[] { outputPdf }, copyPdf);

        // Final log entry
        using (StreamWriter logWriter = new StreamWriter(logPath, true))
        {
            logWriter.WriteLine($"PDF generation completed at {DateTime.Now}");
            logWriter.WriteLine($"Output PDF: {outputPdf}");
            logWriter.WriteLine($"Copied PDF via PdfFileEditor: {copyPdf}");
        }

        Console.WriteLine("Processing completed. Check the log file for details.");
    }

    // Sample method to generate a DataTable (replace with real data retrieval)
    static DataTable GetSampleDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Amount", typeof(decimal));

        dt.Rows.Add(1, "Alice", 123.45m);
        dt.Rows.Add(2, "Bob", 678.90m);
        dt.Rows.Add(3, "Charlie", 234.56m);

        return dt;
    }
}
