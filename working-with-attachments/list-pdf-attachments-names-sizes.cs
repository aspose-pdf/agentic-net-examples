using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Access the collection of embedded files (attachments)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Iterate over each attachment key
            foreach (string key in attachments.Keys)
            {
                // Retrieve the file specification for the current key
                FileSpecification fileSpec = attachments.FindByName(key);
                if (fileSpec == null)
                {
                    Console.WriteLine($"Unable to retrieve attachment with key '{key}'.");
                    continue;
                }

                // Use the Name property to get the original file name
                string fileName = fileSpec.Name ?? key;

                // Determine the size of the attachment
                long sizeInBytes = 0;

                // Prefer the size reported in the Params dictionary if available
                if (fileSpec.Params != null && fileSpec.Params.Size > 0)
                {
                    sizeInBytes = fileSpec.Params.Size;
                }
                // Fallback: read the length of the Contents stream
                else if (fileSpec.Contents != null)
                {
                    try
                    {
                        sizeInBytes = fileSpec.Contents.Length;
                    }
                    catch (NotSupportedException)
                    {
                        // Stream does not support Length; read to memory to determine size
                        using (var ms = new MemoryStream())
                        {
                            fileSpec.Contents.CopyTo(ms);
                            sizeInBytes = ms.Length;
                        }
                    }
                }

                Console.WriteLine($"Attachment: {fileName}, Size: {sizeInBytes} bytes");
            }
        }
    }
}
