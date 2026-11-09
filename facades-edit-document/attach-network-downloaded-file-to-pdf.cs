using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        const string sourcePdfPath = "input.pdf";               // existing PDF
        const string outputPdfPath = "output_attached.pdf";     // result PDF
        const string attachmentUrl = "https://example.com/file.bin"; // file to download
        const string attachmentFileName = "downloaded.bin";     // name to appear in PDF

        // Verify source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Download the file into a byte array
        byte[] attachmentData;
        using (HttpClient http = new HttpClient())
        {
            try
            {
                attachmentData = await http.GetByteArrayAsync(attachmentUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download attachment: {ex.Message}");
                return;
            }
        }

        // Load the source PDF using Document (PdfFileEditor does not provide AttachFile)
        Document pdfDoc;
        try
        {
            pdfDoc = new Document(sourcePdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load source PDF: {ex.Message}");
            return;
        }

        // Create a FileSpecification from the byte array and add it to the PDF's EmbeddedFiles collection
        try
        {
            using (var ms = new MemoryStream(attachmentData))
            {
                // Use the constructor that takes a file name and description, then assign the stream to Contents.
                var fileSpec = new FileSpecification(attachmentFileName, attachmentFileName);
                fileSpec.Contents = ms; // embed the stream content
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error attaching file: {ex.Message}");
        }
        finally
        {
            pdfDoc?.Dispose();
        }
    }
}
