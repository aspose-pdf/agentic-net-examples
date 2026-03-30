using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static async Task Main(string[] args)
    {
        const string xlsxPath = "source.xlsx";
        const string outputPdf = "output.pdf";

        if (!File.Exists(xlsxPath))
        {
            Console.Error.WriteLine($"File not found: {xlsxPath}");
            return;
        }

        // Asynchronously read the XLSX file into a byte array
        byte[] xlsxBytes = await File.ReadAllBytesAsync(xlsxPath);

        // Create a new PDF document
        using (Document pdfDocument = new Document())
        {
            // Add a page with a simple text indicating the attachment
            Page page = pdfDocument.Pages.Add();
            TextFragment textFragment = new TextFragment("PDF with attached XLSX file.");
            page.Paragraphs.Add(textFragment);

            // Embed the XLSX file as an embedded file using FileSpecification
            // Use the constructor that takes a file path and a description, then assign the contents via a stream
            var fileSpec = new FileSpecification(xlsxPath, Path.GetFileName(xlsxPath));
            fileSpec.Contents = new MemoryStream(xlsxBytes);
            pdfDocument.EmbeddedFiles.Add(fileSpec);

            // Save the PDF to a memory stream first
            using (MemoryStream memoryStream = new MemoryStream())
            {
                pdfDocument.Save(memoryStream);
                // Asynchronously write the PDF bytes to the output file
                await File.WriteAllBytesAsync(outputPdf, memoryStream.ToArray());
            }
        }

        Console.WriteLine($"PDF saved to '{outputPdf}' with attached XLSX.");
    }
}