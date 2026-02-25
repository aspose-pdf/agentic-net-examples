using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputXls = "output.xls";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Configure Excel save options
                ExcelSaveOptions excelOpts = new ExcelSaveOptions
                {
                    // Explicitly disable insertion of a blank column at the first position
                    InsertBlankColumnAtFirst = false
                };

                // Save the document as Excel
                pdfDoc.Save(outputXls, excelOpts);
            }

            Console.WriteLine($"PDF successfully converted to Excel: {outputXls}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}