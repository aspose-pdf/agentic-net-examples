using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "large_input.pdf";
        const string outputExcelPath = "output.xlsx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Create a temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Enable disk buffering for large files using PdfFileEditor
            PdfFileEditor editor = new PdfFileEditor
            {
                UseDiskBuffer = true
            };

            // Load the source PDF
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Save the PDF to a temporary file with a specified TempPath
                string tempPdfPath = Path.Combine(tempFolder, "intermediate.pdf");
                PdfSaveOptions pdfSaveOpts = new PdfSaveOptions
                {
                    TempPath = tempFolder
                };
                pdfDoc.Save(tempPdfPath, pdfSaveOpts);

                // Convert the temporary PDF to Excel using ExcelSaveOptions
                using (Document tempDoc = new Document(tempPdfPath))
                {
                    ExcelSaveOptions excelSaveOpts = new ExcelSaveOptions();
                    tempDoc.Save(outputExcelPath, excelSaveOpts);
                }
            }

            Console.WriteLine($"Excel file saved to '{outputExcelPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder
            try
            {
                Directory.Delete(tempFolder, true);
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }
}