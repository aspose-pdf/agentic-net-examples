using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that contains the large dataset
        const string inputPdfPath = "input.pdf";

        // Desired output Excel file (XLSX)
        const string outputExcelPath = "output.xlsx";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Create a dedicated temporary folder for all intermediate files.
        // Using a GUID guarantees a unique folder name.
        // -----------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(),
                                         "AsposePdfTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for a temporary PDF that will be written using the TempPath option
        string tempPdfPath = Path.Combine(tempFolder, "intermediate.pdf");

        try
        {
            // -------------------------------------------------------------
            // 1. Load the original PDF.
            // -------------------------------------------------------------
            using (Document sourcePdf = new Document(inputPdfPath))
            {
                // ---------------------------------------------------------
                // 2. Save the PDF to the temporary location using PdfSaveOptions.
                //    The TempPath property forces Aspose.Pdf to use the folder
                //    we created for any internal temporary files.
                // ---------------------------------------------------------
                PdfSaveOptions pdfSaveOpts = new PdfSaveOptions
                {
                    TempPath = tempFolder
                };
                sourcePdf.Save(tempPdfPath, pdfSaveOpts);
            }

            // -------------------------------------------------------------
            // 3. Enable disk buffering via the Facades API. This tells the
            //    library to write large intermediate data to disk instead of
            //    keeping everything in memory.
            // -------------------------------------------------------------
            PdfFileEditor fileEditor = new PdfFileEditor
            {
                UseDiskBuffer = true
            };

            // -------------------------------------------------------------
            // 4. Load the temporary PDF and convert it to Excel.
            // -------------------------------------------------------------
            using (Document tempPdf = new Document(tempPdfPath))
            {
                ExcelSaveOptions excelOpts = new ExcelSaveOptions();

                // The conversion itself may generate large temporary data;
                // the previously configured PdfSaveOptions.TempPath and the
                // Facades disk buffer ensure those files are placed in the
                // temporary folder we control.
                tempPdf.Save(outputExcelPath, excelOpts);
            }

            Console.WriteLine($"Excel file successfully created at '{outputExcelPath}'.");
        }
        finally
        {
            // -------------------------------------------------------------
            // Clean up the temporary folder and all its contents.
            // Any failure during cleanup is ignored to avoid masking the
            // primary operation result.
            // -------------------------------------------------------------
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, recursive: true);
                }
            }
            catch
            {
                // Ignored
            }
        }
    }
}