using System;
using System.Data;
using Aspose.Pdf.Facades;

namespace AutoFillerConsoleApp
{
    class Program
    {
        /// <summary>
        /// Simple console application that demonstrates how to use Aspose.Pdf.Facades.AutoFiller
        /// to fill a PDF form with static data. The original ASP.NET Core controller has been
        /// removed because the project does not reference Microsoft.AspNetCore.* packages.
        ///
        /// Usage: dotnet run <templatePdfPath> <outputPdfPath>
        /// Example: dotnet run "Templates/FormTemplate.pdf" "filled.pdf"
        /// </summary>
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: <templatePdfPath> <outputPdfPath>");
                return;
            }

            string templatePdfPath = args[0];
            string outputPdfPath   = args[1];

            if (!System.IO.File.Exists(templatePdfPath))
            {
                Console.WriteLine($"Template PDF not found: {templatePdfPath}");
                return;
            }

            // ---------------------------------------------------------------------
            // Prepare sample data that would normally come from an Excel file.
            // The original code used Aspose.Cells to read an .xlsx file, but the
            // project does not reference that package, which caused CS0234.
            // Instead we create a DataTable manually.
            // ---------------------------------------------------------------------
            var dataTable = new DataTable();
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Date");
            dataTable.Rows.Add("John Doe", DateTime.Today.ToShortDateString());

            try
            {
                using (var filler = new AutoFiller())
                {
                    filler.BindPdf(templatePdfPath);
                    filler.ImportDataTable(dataTable);
                    filler.Save(outputPdfPath);
                }

                Console.WriteLine($"PDF successfully filled and saved to: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
