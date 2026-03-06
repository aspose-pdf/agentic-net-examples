using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "attachment_to_remove.bin";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Delete the embedded file (attachment) by its name
                // EmbeddedFileCollection.Delete(string) removes the specified attachment
                doc.EmbeddedFiles.Delete(attachmentName);

                // Save the modified document as PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment '{attachmentName}' deleted. Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}