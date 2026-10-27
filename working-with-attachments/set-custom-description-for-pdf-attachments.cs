using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all embedded files (attachments)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Set a custom description (example uses the file name)
                    fileSpec.Description = $"Attachment: {fileSpec.Name}";

                    // Determine a MIME type based on the file extension
                    string ext = Path.GetExtension(fileSpec.Name)?.ToLowerInvariant();
                    switch (ext)
                    {
                        case ".jpg":
                        case ".jpeg":
                            fileSpec.MIMEType = "image/jpeg";
                            break;
                        case ".png":
                            fileSpec.MIMEType = "image/png";
                            break;
                        case ".txt":
                            fileSpec.MIMEType = "text/plain";
                            break;
                        case ".pdf":
                            fileSpec.MIMEType = "application/pdf";
                            break;
                        default:
                            // Fallback for unknown types
                            fileSpec.MIMEType = "application/octet-stream";
                            break;
                    }
                }
            }

            // Save the PDF (PDF format, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with updated attachment metadata to '{outputPath}'.");
    }
}
