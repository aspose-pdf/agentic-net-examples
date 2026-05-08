using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as necessary
        const string excelPath = "Report.xlsx";          // Excel workbook to embed
        const string outputPdf = "PortfolioWithExcel.pdf";

        // Verify the Excel file exists
        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Create a new PDF document (portfolio) and add a blank page
        Document pdfDoc = new Document();
        pdfDoc.Pages.Add(); // a page makes the PDF viewable

        // Create a FileSpecification for the Excel workbook
        // First argument – file name as it will appear in the portfolio
        // Second argument – custom description shown in the UI
        var fileSpec = new FileSpecification("Report.xlsx", "Quarterly financial report – Excel workbook");

        // Assign the file contents via a stream (required overload)
        fileSpec.Contents = new MemoryStream(File.ReadAllBytes(excelPath));

        // Add the specification to the document's embedded files collection
        pdfDoc.EmbeddedFiles.Add(fileSpec);

        // Save the resulting PDF portfolio
        pdfDoc.Save(outputPdf);

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}
