using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";               // CGM source file
        const string tempPdfPath = "temp_output.pdf";     // temporary PDF file

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        try
        {
            // Convert CGM to PDF using the static PdfProducer API
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, tempPdfPath);

            // Load PDF file information via PdfFileInfo facade
            using (PdfFileInfo info = new PdfFileInfo(tempPdfPath))
            {
                Console.WriteLine("=== PDF Information extracted from CGM ===");
                Console.WriteLine($"Title          : {info.Title}");
                Console.WriteLine($"Author         : {info.Author}");
                Console.WriteLine($"Creator        : {info.Creator}");
                Console.WriteLine($"Subject        : {info.Subject}");
                Console.WriteLine($"Keywords       : {info.Keywords}");
                Console.WriteLine($"Creation Date  : {info.CreationDate}");
                Console.WriteLine($"Modification   : {info.ModDate}");
                Console.WriteLine($"Number of Pages: {info.NumberOfPages}");
                Console.WriteLine($"Is Encrypted   : {info.IsEncrypted}");
                Console.WriteLine($"PDF Version    : {info.GetPdfVersion()}");
                Console.WriteLine($"Has Open Password : {info.HasOpenPassword}");
                Console.WriteLine($"Has Edit Password : {info.HasEditPassword}");
                Console.WriteLine($"Is PDF File    : {info.IsPdfFile}");
                Console.WriteLine($"Has Collection : {info.HasCollection}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary PDF file
            if (File.Exists(tempPdfPath))
            {
                try { File.Delete(tempPdfPath); } catch { /* ignore */ }
            }
        }
    }
}