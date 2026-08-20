using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each embedded file (attachment)
            foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
            {
                if (fileSpec == null)
                    continue;

                // Get the attachment name (original file name)
                string fileName = fileSpec.Name;

                // Determine the size of the attachment (in bytes)
                long size = 0;
                Stream contentStream = fileSpec.Contents;
                if (contentStream != null)
                {
                    if (contentStream.CanSeek)
                    {
                        size = contentStream.Length;
                    }
                    else
                    {
                        // Fallback: copy to a MemoryStream to obtain length
                        using (var ms = new MemoryStream())
                        {
                            contentStream.CopyTo(ms);
                            size = ms.Length;
                        }
                    }
                }

                // Output the attachment details
                Console.WriteLine($"Attachment: {fileName}, Size: {size} bytes");
            }
        }
    }
}
