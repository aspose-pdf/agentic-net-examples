using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "example.txt";

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
                // Check if the attachment exists and delete it by name
                if (doc.EmbeddedFiles.FindByName(attachmentName) != null)
                {
                    doc.EmbeddedFiles.Delete(attachmentName);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment '{attachmentName}' removed. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}