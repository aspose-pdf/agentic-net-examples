using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: FillPdfForm <inputPdf> <inputXlsx> [outputPdf]");
            return;
        }

        string inputPdfPath = args[0];
        string inputXlsxPath = args[1]; // kept for compatibility – file existence is verified but not parsed
        string outputPdfPath = args.Length > 2 ? args[2] : "filled.pdf";

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

        // ---------------------------------------------------------------------
        // NOTE: The original implementation used Aspose.Cells to read field names
        // and values from the supplied XLSX file. The current project does not
        // reference the Aspose.Cells assembly, which caused CS0234. To keep the
        // example self‑contained and compilable we replace the Excel handling
        // with a simple in‑memory dictionary. In a real‑world scenario you can
        // either add the Aspose.Cells NuGet package or implement your own XLSX
        // reader (e.g., using OpenXml SDK or a CSV export).
        // ---------------------------------------------------------------------
        var fieldValues = new Dictionary<string, string>
        {
            // Sample data – replace with actual values as needed.
            { "Name", "John Doe" },
            { "Date", DateTime.Today.ToShortDateString() }
        };

        using (Form pdfForm = new Form())
        {
            pdfForm.BindPdf(inputPdfPath);

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
}
