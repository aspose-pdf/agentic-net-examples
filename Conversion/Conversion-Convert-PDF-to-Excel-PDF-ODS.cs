using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.ods";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPath))
            {
                // Configure Excel save options to produce an ODS file
                Aspose.Pdf.ExcelSaveOptions saveOptions = new Aspose.Pdf.ExcelSaveOptions
                {
                    Format = Aspose.Pdf.ExcelSaveOptions.ExcelFormat.ODS
                };

                // Save the document as ODS
                pdfDoc.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"PDF successfully converted to ODS: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}