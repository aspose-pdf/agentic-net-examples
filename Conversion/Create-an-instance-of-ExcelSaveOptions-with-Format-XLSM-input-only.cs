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

        // The options can now be passed to Document.Save(...)
        Console.WriteLine($"ExcelSaveOptions.Format = {saveOptions.Format}");
    }
}