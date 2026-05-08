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

        // Load the PDF document (using rule for disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Access the embedded files collection (attachments)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Iterate over the attachment keys and display name and size
            foreach (string key in attachments.Keys)
            {
                // Retrieve the file specification for the current key
                FileSpecification fileSpec = attachments.FindByName(key);

                // Size in bytes – use Params.Size (Data property does not exist)
                long size = fileSpec?.Params?.Size ?? 0;

                // Use the specification's Name if available, otherwise fall back to the key
                string name = fileSpec?.Name ?? key;

                Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
            }
        }
    }
}
