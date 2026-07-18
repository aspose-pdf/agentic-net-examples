using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing attachments
        const string outputPath = "output.pdf"; // PDF after removal
        const int attachmentIndex = 0;           // zero‑based index to remove

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of embedded files (attachments)
            EmbeddedFileCollection attachments = doc.EmbeddedFiles;

            // Validate index against the collection count
            if (attachmentIndex < 0 || attachmentIndex >= attachments.Count)
            {
                Console.Error.WriteLine($"Invalid attachment index: {attachmentIndex}");
                return;
            }

            // Retrieve the attachment name at the specified zero‑based index using reflection
            // This avoids a direct compile‑time dependency on the EmbeddedFile type.
            string nameToRemove = null;
            int current = 0;
            foreach (var item in attachments)
            {
                if (current == attachmentIndex)
                {
                    var nameProp = item.GetType().GetProperty("Name");
                    if (nameProp != null)
                    {
                        nameToRemove = nameProp.GetValue(item) as string;
                    }
                    break;
                }
                current++;
            }

            if (string.IsNullOrEmpty(nameToRemove))
            {
                Console.Error.WriteLine($"Could not determine name of attachment at index {attachmentIndex}.");
                return;
            }

            // Remove the attachment by its name (the collection only supports Delete(string))
            attachments.Delete(nameToRemove);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Attachment at index {attachmentIndex} removed. Saved to '{outputPath}'.");
    }
}
