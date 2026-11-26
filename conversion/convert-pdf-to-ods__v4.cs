using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF API and SaveOptions classes
using Aspose.Pdf;                     // ExcelSaveOptions resides here (no Aspose.Pdf.Saving namespace)

class Program
{
    static void Main()
    {
        const string inputPath  = "data_sheet.pdf";
        const string outputPath = "data_sheet.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Configure ExcelSaveOptions to produce ODS format
            ExcelSaveOptions excelOpts = new ExcelSaveOptions
            {
                Format = ExcelSaveOptions.ExcelFormat.ODS
            };

            // Save the document as ODS using the explicit SaveOptions
            pdfDoc.Save(outputPath, excelOpts);
        }

        Console.WriteLine($"Conversion completed: '{outputPath}'");
    }
}