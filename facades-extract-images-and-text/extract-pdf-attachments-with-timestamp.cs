using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputDir = "ExtractedAttachments";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Load the PDF document.
        Document pdfDoc = new Document(pdfPath);

        // Check if there are any embedded files (attachments).
        if (pdfDoc.EmbeddedFiles == null || pdfDoc.EmbeddedFiles.Count == 0)
        {
            Console.WriteLine("No attachments found in the PDF.");
            return;
        }

        int index = 0;
        foreach (FileSpecification fileSpec in pdfDoc.EmbeddedFiles)
        {
            // Original attachment name.
            string originalName = fileSpec.Name;

            // Create a timestamp prefix; include an index to guarantee uniqueness.
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string newFileName = $"{timestamp}_{index}_{originalName}";
            string outputPath = Path.Combine(outputDir, newFileName);

            // Write the embedded file's contents to disk.
            using (FileStream outStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                if (fileSpec.Contents != null)
                {
                    if (fileSpec.Contents.CanSeek)
                        fileSpec.Contents.Position = 0;
                    fileSpec.Contents.CopyTo(outStream);
                }
            }

            Console.WriteLine($"Extracted and renamed: {newFileName}");
            index++;
        }
    }
}
