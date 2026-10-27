using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;

class Program
{
    static async Task Main()
    {
        // Paths for the source PDF and the resulting PDF with attachment
        const string sourcePdfPath = "input.pdf";
        const string outputPdfPath = "output_with_attachment.pdf";

        // URL of the file to download and attach
        const string fileUrl = "https://example.com/file-to-attach.bin";

        // Ensure the source PDF exists
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Download the file into a byte array
        byte[] fileBytes;
        using (HttpClient http = new HttpClient())
        {
            try
            {
                fileBytes = await http.GetByteArrayAsync(fileUrl);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to download attachment: {ex.Message}");
                return;
            }
        }

        try
        {
            // Copy the original PDF to the output location (preserve the original)
            File.Copy(sourcePdfPath, outputPdfPath, overwrite: true);

            // Load the copied PDF using Aspose.Pdf.Document
            Document pdfDoc = new Document(outputPdfPath);

            // Create a FileSpecification from the downloaded bytes and add it to the EmbeddedFiles collection
            using (var attachmentStream = new MemoryStream(fileBytes))
            {
                var fileSpec = new FileSpecification(attachmentStream, "attachment.bin", "Downloaded attachment")
                {
                    MIMEType = "application/octet-stream",
                    AFRelationship = AFRelationship.Data
                };
                pdfDoc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the changes back to the output file
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"Attachment added successfully. Output saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during attachment process: {ex.Message}");
        }
    }
}
