using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an instance of ExcelSaveOptions
        ExcelSaveOptions saveOptions = new ExcelSaveOptions();

        // Set the output format to macro‑enabled XLSM
        saveOptions.Format = ExcelSaveOptions.ExcelFormat.XLSM;

        // Example usage (optional):
        // var pdfDoc = new Document("input.pdf");
        // pdfDoc.Save("output.xlsm", saveOptions);
    }
}