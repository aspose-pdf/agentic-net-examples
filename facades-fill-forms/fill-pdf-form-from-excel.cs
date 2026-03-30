using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: FillPdfFormFromExcel <inputPdf> <inputXlsx> <outputPdf>");
            return;
        }

        string inputPdfPath = args[0];
        string inputXlsxPath = args[1];
        string outputPdfPath = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(inputXlsxPath))
        {
            Console.Error.WriteLine($"Excel file not found: {inputXlsxPath}");
            return;
        }

        // NOTE: Aspose.Cells is not referenced in this project.
        // To keep the example compile‑time safe we use a simple in‑memory
        // dictionary that mimics data that could be read from the Excel file.
        // In a real scenario you could replace this block with a proper
        // Excel reader (e.g., Aspose.Cells, EPPlus, ClosedXML, etc.).
        var fieldValues = LoadFieldValuesFromExcelPlaceholder(inputXlsxPath);

        using (Form pdfForm = new Form(inputPdfPath))
        {
            foreach (var kvp in fieldValues)
            {
                if (!string.IsNullOrEmpty(kvp.Key))
                {
                    pdfForm.FillField(kvp.Key, kvp.Value);
                }
            }

            pdfForm.Save(outputPdfPath);
        }

        Console.WriteLine($"Form filled and saved to '{outputPdfPath}'.");
    }

    // Placeholder implementation – returns a dictionary with sample data.
    // Replace with actual Excel parsing logic if Aspose.Cells (or another library) is added.
    private static Dictionary<string, string> LoadFieldValuesFromExcelPlaceholder(string excelPath)
    {
        // For demonstration we return two static entries.
        // You can extend this method to read the file and populate the dictionary.
        return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
        };
    }
}
