using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "myfile.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Verify the attachment exists
            var fileSpec = doc.EmbeddedFiles.FindByName(attachmentName);
            if (fileSpec != null)
            {
                // Delete the attachment by its filename
                doc.EmbeddedFiles.Delete(attachmentName);
                Console.WriteLine($"Attachment '{attachmentName}' deleted.");
            }
            else
            {
                Console.WriteLine($"Attachment '{attachmentName}' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}