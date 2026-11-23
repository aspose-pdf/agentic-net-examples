using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";            // existing PDF
        const string attachmentPath = "attachment.txt"; // file to embed
        const string outputPath = "output.pdf";

        // Verify source files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a FileSpecification for the attachment and add it to EmbeddedFiles
            var fileSpec = new FileSpecification(attachmentPath, "Embedded attachment");
            // Optionally set additional parameters, e.g., modification date
            fileSpec.Params.ModDate = DateTime.UtcNow;
            doc.EmbeddedFiles.Add(fileSpec);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Re‑open the saved PDF to enumerate embedded files and verify count
        using (Document resultDoc = new Document(outputPath))
        {
            int attachmentCount = resultDoc.EmbeddedFiles.Count;
            Console.WriteLine($"Number of embedded files: {attachmentCount}");

            // Enumerate and display each embedded file name
            foreach (FileSpecification spec in resultDoc.EmbeddedFiles)
            {
                Console.WriteLine($"Embedded file: {spec.Name}");
            }

            // Verify that exactly one attachment exists
            if (attachmentCount == 1)
            {
                Console.WriteLine("Verification succeeded: exactly one attachment present.");
            }
            else
            {
                Console.WriteLine("Verification failed: attachment count is not one.");
            }
        }
    }
}
