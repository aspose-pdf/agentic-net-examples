using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string excelPath = "data.xlsx";               // Path to the Excel workbook to embed
        const string outputPdf = "portfolio_with_excel.pdf"; // Resulting PDF portfolio

        // Verify the Excel file exists
        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Create a new PDF document (acts as a portfolio)
        using (Document pdfDoc = new Document())
        {
            // Add a blank page so the PDF is not empty (optional)
            pdfDoc.Pages.Add();

            // Create a FileSpecification with a custom description
            var fileSpec = new FileSpecification(Path.GetFileName(excelPath), "Quarterly financial report (Excel)");

            // Assign the file contents via a stream (required overload)
            fileSpec.Contents = new MemoryStream(File.ReadAllBytes(excelPath));

            // Add the specification to the document's embedded files collection
            pdfDoc.EmbeddedFiles.Add(fileSpec);

            // Save the PDF portfolio
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created: {outputPdf}");
    }
}
