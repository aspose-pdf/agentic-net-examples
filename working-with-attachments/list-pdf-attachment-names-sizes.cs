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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Get the collection of embedded file attachments
            var embeddedFiles = doc.EmbeddedFiles;

            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the document.");
                return;
            }

            // Iterate over each FileSpecification in the collection
            foreach (FileSpecification fileSpec in embeddedFiles)
            {
                // Attachment name (fallback to a placeholder if null)
                string name = fileSpec.Name ?? "Unnamed";

                // Determine size – use Params.Size when available, otherwise fall back to the stream length
                long size = 0;
                if (fileSpec.Params != null)
                {
                    size = fileSpec.Params.Size; // Size is an int, implicitly convertible to long
                }
                else if (fileSpec.Contents != null && fileSpec.Contents.CanSeek)
                {
                    size = fileSpec.Contents.Length;
                }

                Console.WriteLine($"Attachment: {name}, Size: {size} bytes");
            }
        }
    }
}
