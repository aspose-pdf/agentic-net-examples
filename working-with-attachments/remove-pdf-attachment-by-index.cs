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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the index is within the collection bounds
                if (attachmentIndex < 0 || attachmentIndex >= doc.EmbeddedFiles.Count)
                {
                    Console.Error.WriteLine("Invalid attachment index.");
                }
                else
                {
                    // Retrieve the embedded file at the specified index
                    var embeddedFile = doc.EmbeddedFiles[attachmentIndex];

                    // Remove the attachment by its name
                    doc.EmbeddedFiles.Delete(embeddedFile.Name);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment removed and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}