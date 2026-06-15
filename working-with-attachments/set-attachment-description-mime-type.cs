using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all embedded file attachments
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                // Set a custom description for the attachment
                fileSpec.Description = $"Attachment: {fileSpec.Name}";

                // Determine a MIME type based on the file extension
                string ext = Path.GetExtension(fileSpec.Name)?.ToLowerInvariant();
                fileSpec.MIMEType = ext switch
                {
                    ".txt"  => "text/plain",
                    ".pdf"  => "application/pdf",
                    ".jpg"  => "image/jpeg",
                    ".jpeg" => "image/jpeg",
                    ".png"  => "image/png",
                    ".doc"  => "application/msword",
                    ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    _       => "application/octet-stream"
                };
            }

            // Save the modified PDF; the attachment metadata is now updated
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated attachment metadata to '{outputPath}'.");
    }
}