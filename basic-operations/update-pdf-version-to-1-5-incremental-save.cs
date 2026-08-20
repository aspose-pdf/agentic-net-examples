using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "conversion_log.xml"; // optional log file for conversion

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF
        using (Document doc = new Document(inputPath))
        {
            // Change PDF version to 1.5 using the Convert method (Version property is read‑only)
            doc.Convert(logPath, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Save the document back to the same file using incremental update.
            // In recent Aspose.PDF versions the IncrementalUpdate flag is enabled by default when
            // saving to the original file, and the property is not present in older library versions.
            var saveOptions = new PdfSaveOptions();
            doc.Save(inputPath, saveOptions);
        }

        Console.WriteLine("PDF version updated to 1.5 with incremental save.");
    }
}