using System;
using System.Data;
using System.IO;
using Aspose.Pdf.Facades;

namespace AutoFillerConsoleApp
{
    class Program
    {
        /// <summary>
        /// Entry point.
        /// Usage: dotnet run <excelFilePath> <outputPdfPath> [templatePdfPath]
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: <excelFilePath> <outputPdfPath> [templatePdfPath]");
                return;
            }

            string excelPath = args[0];
            string outputPdfPath = args[1];
            string templatePath = args.Length >= 3 ? args[2] : Path.Combine(Directory.GetCurrentDirectory(), "Templates", "template.pdf");

            if (!File.Exists(excelPath))
            {
                Console.WriteLine($"Excel file not found: {excelPath}");
                return;
            }

            if (!File.Exists(templatePath))
            {
                Console.WriteLine($"PDF template not found: {templatePath}");
                return;
            }

            // ------------------------------------------------------------
            // Convert the uploaded Excel file to a DataTable.
            // This example creates a dummy DataTable; replace with real
            // Excel parsing logic as needed (e.g., using ClosedXML, EPPlus, etc.).
            // ------------------------------------------------------------
            DataTable dataTable = CreateDummyDataTable();

            // ------------------------------------------------------------
            // Use Aspose.Pdf.Facades.AutoFiller to merge the data into the
            // PDF template and produce a single merged PDF file.
            // ------------------------------------------------------------
            try
            {
                using (AutoFiller autoFiller = new AutoFiller())
                {
                    // Bind the PDF template file.
                    autoFiller.BindPdf(templatePath);

                    // Import the DataTable; column names must match field names.
                    autoFiller.ImportDataTable(dataTable);

                    // Save the merged PDF into a memory stream first (optional).
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        autoFiller.Save(outputStream);
                        outputStream.Position = 0;
                        File.WriteAllBytes(outputPdfPath, outputStream.ToArray());
                    }
                }

                Console.WriteLine($"Filled PDF generated successfully at: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PDF generation: {ex.Message}");
            }
        }

        /// <summary>
        /// Creates a dummy DataTable that mimics the structure expected by the PDF form.
        /// Replace this method with real Excel‑to‑DataTable conversion logic.
        /// </summary>
        private static DataTable CreateDummyDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CompanyName", typeof(string));
            dt.Columns.Add("ContactName", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("PostalCode", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("Country", typeof(string));
            dt.Columns.Add("Heading", typeof(string));

            dt.Rows.Add(
                "Acme Corp",
                "John Doe",
                "123 Main St",
                "12345",
                "Metropolis",
                "USA",
                "Dear Acme Corp,"
            );

            return dt;
        }
    }
}
