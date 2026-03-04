using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Directory containing the source Markdown file.
        const string dataDir = "YOUR_DATA_DIRECTORY";

        // Input Markdown file.
        string mdFile = Path.Combine(dataDir, "input.md");

        // Temporary PDF file created from the Markdown.
        string tempPdf = Path.Combine(dataDir, "temp.pdf");

        // Final XLSM output file.
        string outputXlsm = Path.Combine(dataDir, "output.xlsm");

        if (!File.Exists(mdFile))
        {
            Console.Error.WriteLine($"Markdown file not found: {mdFile}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the Markdown file and save it as PDF.
        // -----------------------------------------------------------------
        MdLoadOptions mdLoadOptions = new MdLoadOptions();

        using (Document pdfDocument = new Document(mdFile, mdLoadOptions))
        {
            // Save the intermediate PDF.
            pdfDocument.Save(tempPdf);
        }

        // -----------------------------------------------------------------
        // 2. Demonstrate usage of a Facade (PdfConverter) to bind the PDF.
        //    No page manipulation is performed here, but the facade is
        //    correctly instantiated and disposed.
        // -----------------------------------------------------------------
        using (PdfConverter converter = new PdfConverter())
        {
            converter.BindPdf(tempPdf);
            // Placeholder for any page‑level operations using the facade.
        }

        // -----------------------------------------------------------------
        // 3. Convert the PDF to Excel format (XLSX) and rename to XLSM.
        // -----------------------------------------------------------------
        using (Document pdfDocument = new Document(tempPdf))
        {
            ExcelSaveOptions excelOptions = new ExcelSaveOptions();

            // Save as XLSX first.
            string tempXlsx = Path.Combine(dataDir, "temp.xlsx");
            pdfDocument.Save(tempXlsx, excelOptions);

            // Rename the XLSX file to XLSM (macro‑enabled workbook).
            if (File.Exists(outputXlsm))
                File.Delete(outputXlsm);
            File.Move(tempXlsx, outputXlsm);
        }

        Console.WriteLine($"Conversion completed: {outputXlsm}");
    }
}