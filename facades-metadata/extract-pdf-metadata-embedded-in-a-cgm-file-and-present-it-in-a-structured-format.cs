using System;
using System.IO;
using System.Text.Json;
using Aspose.Pdf.Facades;
using Aspose.Pdf; // for ImportFormat enum

class Program
{
    static void Main()
    {
        const string cgmPath = "input.cgm";
        const string tempPdfPath = "temp.pdf";

        if (!File.Exists(cgmPath))
        {
            Console.Error.WriteLine($"CGM file not found: {cgmPath}");
            return;
        }

        // Convert CGM to PDF using PdfProducer
        using (FileStream pdfStream = File.Create(tempPdfPath))
        {
            // Produce PDF from CGM file
            PdfProducer.Produce(cgmPath, ImportFormat.Cgm, pdfStream);
        }

        // Extract metadata from the generated PDF using PdfFileInfo
        using (PdfFileInfo pdfInfo = new PdfFileInfo(tempPdfPath))
        {
            var metadata = new
            {
                Title = pdfInfo.Title,
                Author = pdfInfo.Author,
                Subject = pdfInfo.Subject,
                Keywords = pdfInfo.Keywords,
                Creator = pdfInfo.Creator,
                Producer = pdfInfo.Producer,
                CreationDate = pdfInfo.CreationDate,
                ModDate = pdfInfo.ModDate,
                NumberOfPages = pdfInfo.NumberOfPages,
                IsEncrypted = pdfInfo.IsEncrypted,
                IsPdfFile = pdfInfo.IsPdfFile
            };

            // Serialize metadata to formatted JSON and output
            string json = JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }

        // Clean up temporary PDF file
        try
        {
            File.Delete(tempPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to delete temporary file: {ex.Message}");
        }
    }
}