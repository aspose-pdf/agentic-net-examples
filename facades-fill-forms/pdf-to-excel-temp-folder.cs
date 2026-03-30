using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputExcelPath = "output.xlsx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string intermediatePdfPath = Path.Combine(tempFolder, "intermediate.pdf");

        try
        {
            // Configure DocSaveOptions to store temporary data in the temp folder
            DocSaveOptions docSaveOptions = new DocSaveOptions();
            docSaveOptions.MemorySaveModePath = tempFolder;

            // Load the original PDF and save it to the intermediate file using the temporary folder
            using (Document sourceDocument = new Document(inputPdfPath))
            {
                sourceDocument.Save(intermediatePdfPath, docSaveOptions);
            }

            // Load the intermediate PDF and convert it to Excel
            using (Document intermediateDocument = new Document(intermediatePdfPath))
            {
                ExcelSaveOptions excelOptions = new ExcelSaveOptions();
                intermediateDocument.Save(outputExcelPath, excelOptions);
            }

            Console.WriteLine($"PDF successfully converted to Excel: {outputExcelPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder and its contents
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup exceptions
            }
        }
    }
}