using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string excelPath = "workbook.xlsx";
        const string outputPdf = "portfolio.pdf";

        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Ensure the document has a collection for portfolio files
            if (pdfDoc.Collection == null)
                pdfDoc.Collection = new Collection();

            // Create a file specification for the Excel workbook with a custom description
            var fileSpec = new FileSpecification(excelPath, "Quarterly financial report")
            {
                // Embed the file content
                Contents = new MemoryStream(File.ReadAllBytes(excelPath))
            };

            // Add the file specification to the portfolio collection
            pdfDoc.Collection.Add(fileSpec);

            // Save the PDF portfolio
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF portfolio created at '{outputPdf}'.");
    }
}