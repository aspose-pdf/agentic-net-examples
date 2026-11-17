using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "portfolio.pdf";
        const string excelPath = "data.xlsx";
        const string outputPath = "portfolio_with_excel.pdf";
        const string embeddedFileName = "DataWorkbook.xlsx";
        const string description = "Quarterly financial data";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(excelPath))
        {
            Console.Error.WriteLine($"Excel file not found: {excelPath}");
            return;
        }

        // Open the existing PDF portfolio
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Open the Excel workbook stream
            using (FileStream excelStream = File.OpenRead(excelPath))
            {
                // Create a FileSpecification for the Excel workbook with a custom description
                FileSpecification fileSpec = new FileSpecification(excelStream, embeddedFileName, description)
                {
                    // Optional: set MIME type and relationship for better PDF/A‑3 compliance
                    MIMEType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    AFRelationship = AFRelationship.Data
                };

                // Add the file specification to the document's embedded files collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF portfolio
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Excel workbook embedded and saved to '{outputPath}'.");
    }
}
