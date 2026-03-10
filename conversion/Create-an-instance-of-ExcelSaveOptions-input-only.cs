using System;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create an instance of ExcelSaveOptions using its default constructor.
        ExcelSaveOptions excelOptions = new ExcelSaveOptions();

        // Example: set the desired output format (optional).
        excelOptions.Format = ExcelSaveOptions.ExcelFormat.XLSX;

        // The instance is now ready for use with Document.Save(..., excelOptions).
        Console.WriteLine("ExcelSaveOptions instance created successfully.");
    }
}