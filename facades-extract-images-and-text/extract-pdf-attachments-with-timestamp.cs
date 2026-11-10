using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF that contains attachments
        const string pdfPath = "input.pdf";

        // Folder where extracted (and renamed) attachments will be saved
        const string outputFolder = "ExtractedAttachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF – this class exposes the EmbeddedFiles collection
        Document pdfDoc = new Document(pdfPath);

        // Get all embedded file specifications (attachments)
        var embeddedFiles = pdfDoc.EmbeddedFiles;
        if (embeddedFiles == null || embeddedFiles.Count == 0)
        {
            Console.WriteLine("No attachments found in the PDF.");
            return;
        }

        int counter = 0;
        foreach (FileSpecification fileSpec in embeddedFiles)
        {
            try
            {
                // Original attachment name stored in the PDF
                string originalName = fileSpec.Name;

                // Build a timestamp prefix (yyyyMMddHHmmssfff) and a unique counter
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string newFileName = $"{timestamp}_{counter}_{originalName}";
                counter++;

                string outPath = Path.Combine(outputFolder, newFileName);

                // Write the attachment contents to disk using the new name
                using (FileStream outStream = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    // Ensure the stream is positioned at the beginning
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;

                    fileSpec.Contents.CopyTo(outStream);
                }

                Console.WriteLine($"Extracted and renamed: {outPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing attachment '{fileSpec.Name}': {ex.Message}");
            }
        }
    }
}
