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
            // Load the PDF document inside a using block (ensures disposal)
            using (Document doc = new Document(inputPath))
            {
                // Attempt to delete the specified embedded file (attachment) by name
                // If the attachment does not exist, Delete will silently ignore it
                doc.EmbeddedFiles.Delete(attachmentName);

                // Save the resulting PDF to a new file
                doc.Save(outputPath);
            }

            Console.WriteLine($"Attachment \"{attachmentName}\" removed. Saved as \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}