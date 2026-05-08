using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_attachments.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Access the collection of embedded files (attachments)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            // If there are no attachments, nothing to modify
            if (attachments != null && attachments.Count > 0)
            {
                // Iterate over each attachment and set custom description and MIME type
                for (int i = 1; i <= attachments.Count; i++) // 1‑based indexing
                {
                    FileSpecification fileSpec = attachments[i];

                    // Example custom values – replace with your own logic as needed
                    fileSpec.Description = $"Custom description for {fileSpec.Name}";
                    fileSpec.MIMEType   = "application/octet-stream"; // generic MIME type; adjust per file type
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with updated attachment metadata: {outputPdf}");
    }
}