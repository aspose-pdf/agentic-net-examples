using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string finalExcel = "output.xlsx";

        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposePdfTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the intermediate Excel file
        string tempExcel = Path.Combine(tempFolder, "intermediate.xlsx");

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPdf))
            {
                // Initialize Excel save options
                ExcelSaveOptions saveOptions = new ExcelSaveOptions();

                // Save to the temporary Excel file
                pdfDocument.Save(tempExcel, saveOptions);
            }

            // Move the intermediate file to the final location (simple filename)
            File.Move(tempExcel, finalExcel, true);
            Console.WriteLine($"PDF converted to Excel: {finalExcel}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
    }
}
