using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an ExcelSaveOptions instance using its default constructor.
        ExcelSaveOptions excelOptions = new ExcelSaveOptions();

        // Example configuration: specify the output format (optional).
        excelOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX;

        // The instance is ready to be used with Document.Save for Excel export.
        Console.WriteLine("ExcelSaveOptions instance created successfully.");
    }
}