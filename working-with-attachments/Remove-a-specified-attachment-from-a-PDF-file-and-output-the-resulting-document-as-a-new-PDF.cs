using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "attachment_to_remove.dat";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Remove the specified embedded file (attachment) if it exists
                // EmbeddedFiles.Delete(string) removes an attachment by its name
                doc.EmbeddedFiles.Delete(attachmentName);

                // Save the modified document as a new PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment '{attachmentName}' removed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}