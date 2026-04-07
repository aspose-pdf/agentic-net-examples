using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all embedded file attachments
            for (int i = 0; i < doc.EmbeddedFiles.Count; i++)
            {
                FileSpecification fileSpec = doc.EmbeddedFiles[i];

                // Set a custom description
                fileSpec.Description = $"Attachment {i + 1}";

                // Set MIME type based on file extension (simple mapping)
                string ext = Path.GetExtension(fileSpec.Name)?.ToLowerInvariant();
                fileSpec.MIMEType = GetMimeType(ext);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated attachment metadata to '{outputPath}'.");
    }

    // Helper method to map file extensions to MIME types
    static string GetMimeType(string extension)
    {
        switch (extension)
        {
            case ".pdf":  return "application/pdf";
            case ".txt":  return "text/plain";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            case ".png":  return "image/png";
            case ".doc":  return "application/msword";
            case ".docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            default:      return "application/octet-stream";
        }
    }
}