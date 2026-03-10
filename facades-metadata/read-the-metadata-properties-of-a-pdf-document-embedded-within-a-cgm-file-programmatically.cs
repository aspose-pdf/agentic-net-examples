using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";          // CGM source file
        const string tempPdfPath = "temp_from_cgm.pdf"; // Temporary PDF produced from CGM

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Convert CGM to PDF using PdfProducer (Facades API)
            // -----------------------------------------------------------------
            // Produce() writes the PDF directly to a file; no need for a Document.
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, tempPdfPath);

            // -----------------------------------------------------------------
            // 2. Open the generated PDF with PdfFileInfo to read metadata
            // -----------------------------------------------------------------
            using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath))
            {
                // Standard metadata properties
                Console.WriteLine($"Title          : {pdfInfo.Title}");
                Console.WriteLine($"Author         : {pdfInfo.Author}");
                Console.WriteLine($"Subject        : {pdfInfo.Subject}");
                Console.WriteLine($"Keywords       : {pdfInfo.Keywords}");
                Console.WriteLine($"Creator        : {pdfInfo.Creator}");
                Console.WriteLine($"Producer       : {pdfInfo.Producer}");
                Console.WriteLine($"Creation Date  : {pdfInfo.CreationDate}");
                Console.WriteLine($"Modification   : {pdfInfo.ModDate}");
                Console.WriteLine($"Number of Pages: {pdfInfo.NumberOfPages}");
                Console.WriteLine($"Is Encrypted   : {pdfInfo.IsEncrypted}");
                Console.WriteLine($"Is PDF File    : {pdfInfo.IsPdfFile}");

                // Custom metadata (if any) – example retrieving a custom key
                string customKey = "CustomProperty";
                string customValue = pdfInfo.GetMetaInfo(customKey);
                if (!string.IsNullOrEmpty(customValue))
                {
                    Console.WriteLine($"{customKey} : {customValue}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary PDF file
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore */ }
            }
        }
    }
}