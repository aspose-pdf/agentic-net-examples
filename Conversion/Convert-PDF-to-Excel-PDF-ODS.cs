using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPath))
        {
            // Set up Excel save options to produce ODS output
            ExcelSaveOptions saveOptions = new ExcelSaveOptions
            {
                // The property to specify the desired format is "Format",
                // not "ExcelFormat" (which is the name of the nested enum).
                Format = ExcelSaveOptions.ExcelFormat.ODS
            };

            // Save the document as ODS
            pdfDoc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed: {outputPath}");
    }
}
