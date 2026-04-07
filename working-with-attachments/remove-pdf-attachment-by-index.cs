using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        int attachmentIndex = 0; // zero‑based index of the attachment to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Verify that the index is within the range of existing attachments
            if (doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the document.");
            }
            else if (attachmentIndex < 0 || attachmentIndex >= doc.EmbeddedFiles.Count)
            {
                Console.WriteLine($"Invalid attachment index. Available count: {doc.EmbeddedFiles.Count}");
            }
            else
            {
                // Retrieve the name of the embedded file at the specified zero‑based index
                string attachmentName = doc.EmbeddedFiles[attachmentIndex].Name;

                // Remove the attachment by its name (EmbeddedFileCollection has Delete(string))
                doc.EmbeddedFiles.Delete(attachmentName);
                Console.WriteLine($"Removed attachment: {attachmentName}");
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}