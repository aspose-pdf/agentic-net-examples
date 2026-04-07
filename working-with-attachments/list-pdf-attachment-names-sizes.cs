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
            // Access the collection of embedded files (attachments)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found.");
                return;
            }

            // Iterate over the attachments and display name + size
            for (int i = 1; i <= attachments.Count; i++)
            {
                FileSpecification spec = attachments[i];
                string name = spec.Name ?? "<unknown>";
                long size = 0;

                // Preferred way: size is stored in the Params object
                if (spec.Params != null && spec.Params.Size > 0)
                {
                    size = spec.Params.Size;
                }
                else if (spec.Contents != null && spec.Contents.CanSeek)
                {
                    // Fallback: read the length of the contents stream
                    long originalPos = spec.Contents.Position;
                    spec.Contents.Position = 0;
                    size = spec.Contents.Length;
                    spec.Contents.Position = originalPos;
                }

                Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
            }
        }
    }
}
