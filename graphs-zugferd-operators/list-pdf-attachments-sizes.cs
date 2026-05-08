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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // If there are no embedded files (attachments), inform the user
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Iterate over each embedded file using reflection to avoid a direct dependency on the concrete type
            foreach (var embedded in doc.EmbeddedFiles)
            {
                // Retrieve the "Name" property (string)
                var nameProp = embedded.GetType().GetProperty("Name");
                string name = nameProp?.GetValue(embedded) as string ?? "(unnamed)";

                // Retrieve the "Data" property (byte[]). Some versions expose it as a byte[]; if not present, fall back to 0 size.
                var dataProp = embedded.GetType().GetProperty("Data");
                byte[] data = dataProp?.GetValue(embedded) as byte[];
                long size = data?.Length ?? 0;

                Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
            }
        }
    }
}
