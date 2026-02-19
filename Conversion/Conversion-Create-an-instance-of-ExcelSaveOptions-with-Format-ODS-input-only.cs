using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Define input PDF and output ODS paths
        string dataDir = "Data";
        string pdfPath = Path.Combine(dataDir, "sample.pdf");
        string odsPath = Path.Combine(dataDir, "output.ods");

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Create ExcelSaveOptions and set the format to ODS
        ExcelSaveOptions saveOptions = new ExcelSaveOptions();
        saveOptions.Format = ExcelSaveOptions.ExcelFormat.ODS;

        // Save the document as an ODS file using the specified options
        pdfDocument.Save(odsPath, saveOptions);
    }
}