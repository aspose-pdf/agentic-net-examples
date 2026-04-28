using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the source PDF file.
        const string inputPath = "input.pdf";

        // Ensure the file exists before processing.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document, change its version to 1.5,
        // and save it using incremental update (the Save() overload without parameters).
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF version 1.5.
            // A conversion log file is required by the API.
            const string conversionLog = "conversion_log.txt";
            doc.Convert(conversionLog, PdfFormat.v_1_5, ConvertErrorAction.Delete);

            // Save the document with incremental updates.
            // This overload writes the changes back to the same file using the
            // incremental update technique.
            doc.Save();
        }

        Console.WriteLine("PDF version updated to 1.5 with incremental save.");
    }
}