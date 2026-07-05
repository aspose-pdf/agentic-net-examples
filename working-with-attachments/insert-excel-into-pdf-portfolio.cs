using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "portfolio.pdf";   // output PDF portfolio
        const string excelPath = "report.xlsx";   // Excel workbook to embed
        const string description = "Quarterly financial report";

        // Verify the Excel file exists before proceeding
        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Create a new PDF document (empty) and ensure deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Add a blank page – a PDF portfolio must contain at least one page
            pdfDoc.Pages.Add();

            // Ensure the document has a Collection object (required for portfolios)
            if (pdfDoc.Collection == null)
                pdfDoc.Collection = new Collection();

            // Create a FileSpecification for the Excel workbook, set its description
            // and the file contents, then add it to the collection.
            var fileSpec = new FileSpecification(excelPath, description)
            {
                Contents = new MemoryStream(File.ReadAllBytes(excelPath))
            };
            pdfDoc.Collection.Add(fileSpec);

            // Save the resulting PDF portfolio.
            pdfDoc.Save(pdfPath);
        }

        Console.WriteLine($"PDF portfolio created: {pdfPath}");
    }
}
