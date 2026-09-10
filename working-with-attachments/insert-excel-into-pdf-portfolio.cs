using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string outputPdf = "portfolio.pdf";
        const string excelPath = "report.xlsx";
        const string description = "Quarterly financial report – Excel workbook";

        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Create a new PDF document (empty portfolio)
        using (Document pdfDoc = new Document())
        {
            // Create a file specification for the Excel workbook.
            // The constructor loads the file content and sets the display name.
            FileSpecification excelFileSpec = new FileSpecification(excelPath, description);

            // Add the Excel file to the PDF's embedded files collection.
            pdfDoc.EmbeddedFiles.Add(excelFileSpec);

            // Save the PDF portfolio.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}
