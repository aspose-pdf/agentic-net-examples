using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // existing PDF (can be empty)
        const string excelPath = "workbook.xlsx";    // Excel workbook to embed
        const string outputPdfPath = "portfolio.pdf"; // result PDF Portfolio
        const string customDesc = "Quarterly financial report – Excel workbook";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Load the source PDF
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has a collection – this makes it a PDF Portfolio
            if (pdfDoc.Collection == null)
                pdfDoc.Collection = new Collection();

            // Create a file specification for the Excel workbook with a custom description
            var fileSpec = new FileSpecification(Path.GetFileName(excelPath), customDesc)
            {
                Contents = new MemoryStream(File.ReadAllBytes(excelPath))
            };

            // Add the file specification to the portfolio collection
            pdfDoc.Collection.Add(fileSpec);

            // Save the resulting PDF Portfolio
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF Portfolio created: {outputPdfPath}");
    }
}
