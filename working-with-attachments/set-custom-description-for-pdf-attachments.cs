using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_custom.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use Document.EmbeddedFiles collection (Attachments property does not exist)
        using (Document doc = new Document(inputPath))
        {
            int idx = 1;
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Custom description
                    fileSpec.Description = $"Attachment {idx} description";

                    // Determine MIME type based on file extension
                    string ext = Path.GetExtension(fileSpec.Name)?.ToLowerInvariant();
                    switch (ext)
                    {
                        case ".txt":
                            fileSpec.MIMEType = "text/plain";
                            break;
                        case ".jpg":
                        case ".jpeg":
                            fileSpec.MIMEType = "image/jpeg";
                            break;
                        case ".png":
                            fileSpec.MIMEType = "image/png";
                            break;
                        case ".pdf":
                            fileSpec.MIMEType = "application/pdf";
                            break;
                        default:
                            fileSpec.MIMEType = "application/octet-stream";
                            break;
                    }
                    idx++;
                }
            }

            // Save the PDF (no explicit SaveOptions required for this task)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated attachment metadata to '{outputPath}'.");
    }
}
