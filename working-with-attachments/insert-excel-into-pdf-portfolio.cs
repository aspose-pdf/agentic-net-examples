using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "portfolio_input.pdf";   // Existing PDF (can be empty)
        const string excelPath = "report.xlsx";           // Excel workbook to embed
        const string outputPdfPath = "portfolio_with_excel.pdf";
        const string description = "Quarterly financial report – Excel workbook";

        // Verify files exist
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

        // Load the PDF (portfolio) and embed the Excel file
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Open the Excel file as a stream and create a FileSpecification with description
            using (FileStream excelStream = File.OpenRead(excelPath))
            {
                // The first argument is the name that will appear in the PDF portfolio
                FileSpecification fileSpec = new FileSpecification("report.xlsx", description);
                // Assign the file contents via the stream
                fileSpec.Contents = excelStream;
                // Add the file specification to the PDF's embedded files collection
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the updated PDF portfolio
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Excel workbook embedded successfully into '{outputPdfPath}'.");
    }
}
