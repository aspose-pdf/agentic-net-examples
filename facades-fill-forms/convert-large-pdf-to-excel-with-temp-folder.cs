using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the final Excel output
        const string inputPdfPath = "large_input.pdf";
        const string outputExcelPath = "result.xlsx";

        // Create a unique temporary folder for intermediate files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the intermediate PDF that will be saved in the temporary folder
        string intermediatePdfPath = Path.Combine(tempFolder, "intermediate.pdf");

        try
        {
            // Load the original PDF
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // Configure PDF save options to use the temporary folder for any internal temp files
                PdfSaveOptions pdfSaveOptions = new PdfSaveOptions
                {
                    TempPath = tempFolder
                };

                // Save the PDF to the intermediate location using the configured options
                pdfDocument.Save(intermediatePdfPath, pdfSaveOptions);
            }

            // Load the intermediate PDF (this step isolates the large conversion work)
            using (Document intermediateDoc = new Document(intermediatePdfPath))
            {
                // Initialize Excel save options (default constructor is sufficient)
                ExcelSaveOptions excelSaveOptions = new ExcelSaveOptions();

                // Save the document as an Excel file
                intermediateDoc.Save(outputExcelPath, excelSaveOptions);
            }

            Console.WriteLine($"Excel file saved to '{outputExcelPath}'.");
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
                // If cleanup fails, ignore – the OS will eventually reclaim the temp data
            }
        }
    }
}