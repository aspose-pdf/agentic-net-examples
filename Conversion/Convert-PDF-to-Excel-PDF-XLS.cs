using System;
using System.IO;
using Aspose.Pdf;

namespace PdfToExcelApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: PdfToExcelApp <input.pdf> <output.xlsx>");
                return;
            }

            string pdfPath = args[0];
            string excelPath = args[1];

            PdfToExcelConverter.Convert(pdfPath, excelPath);
        }
    }

    public static class PdfToExcelConverter
    {
        /// <summary>
        /// Converts a PDF file to an Excel workbook (XLS/XLSX).
        /// </summary>
        /// <param name="pdfPath">Full path to the source PDF.</param>
        /// <param name="excelPath">Full path for the output Excel file.</param>
        public static void Convert(string pdfPath, string excelPath)
        {
            // Verify that the source file exists.
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {pdfPath}");
                return;
            }

            // Load the PDF document. The using block ensures deterministic disposal.
            using (Document pdfDocument = new Document(pdfPath))
            {
                // For non‑PDF output you must supply explicit save options.
                // ExcelSaveOptions controls the Excel export behavior.
                ExcelSaveOptions saveOptions = new ExcelSaveOptions();

                // Example optional settings:
                // saveOptions.MinimizeTheNumberOfWorksheets = true; // combine pages into fewer sheets
                // saveOptions.InsertBlankColumnAtFirst = false;    // default behavior

                // Save the document as Excel. The default format is XLSX; you can change it via saveOptions.Format if needed.
                pdfDocument.Save(excelPath, saveOptions);
            }

            Console.WriteLine($"Conversion completed: '{excelPath}'");
        }
    }
}
