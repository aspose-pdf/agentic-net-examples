using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int attachmentIndex = 0; // zero‑based index of the attachment to remove

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

            // Validate the index against the collection count
            if (attachmentIndex < 0 || attachmentIndex >= attachments.Count)
            {
                Console.Error.WriteLine($"Invalid attachment index: {attachmentIndex}");
            }
            else
            {
                // Retrieve the attachment name at the specified index
                string name = attachments[attachmentIndex].Name;

                // Remove the attachment by its name
                attachments.Delete(name);
                Console.WriteLine($"Removed attachment '{name}' at index {attachmentIndex}.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}