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
            // Access the collection of embedded file attachments
            var embeddedFiles = doc.EmbeddedFiles;

            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // The EmbeddedFiles collection is 1‑based, iterate accordingly
            for (int i = 1; i <= embeddedFiles.Count; i++)
            {
                FileSpecification spec = embeddedFiles[i];
                string name = spec.Name;
                long size = 0;

                // Prefer the size reported in the file parameters
                if (spec.Params != null && spec.Params.Size != 0)
                {
                    size = spec.Params.Size;
                }
                else if (spec.Contents != null)
                {
                    // Fallback: determine size from the contents stream
                    if (spec.Contents.CanSeek)
                        spec.Contents.Position = 0;
                    size = spec.Contents.Length;
                }

                Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
            }
        }
    }
}
