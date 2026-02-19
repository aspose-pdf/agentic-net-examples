using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        string pdfPath = "input.pdf";

        // Desired output file (CSV format).
        string csvPath = "output.csv";

        // Load the PDF document.
        using (Document pdfDocument = new Document(pdfPath))
        {
            // Create ExcelSaveOptions and set the output format to CSV.
            ExcelSaveOptions saveOptions = new ExcelSaveOptions();
            saveOptions.Format = ExcelSaveOptions.ExcelFormat.CSV;

            // Save the PDF as a CSV file using the specified options.
            pdfDocument.Save(csvPath, saveOptions);
        }
    }
}