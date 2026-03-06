using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Path to the source PDF
        const string outputPath = "output.pdf";         // Path for the updated PDF
        const string attachmentName = "sample.txt";     // Name of the attachment to delete

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete the specified embedded file (attachment) if it exists
            // The EmbeddedFiles collection provides Delete(string) to remove by name
            try
            {
                doc.EmbeddedFiles.Delete(attachmentName);
                Console.WriteLine($"Attachment \"{attachmentName}\" deleted.");
            }
            catch (Exception ex)
            {
                // If the attachment does not exist, Delete throws an exception;
                // handle it gracefully and continue.
                Console.Error.WriteLine($"Warning: Could not delete attachment \"{attachmentName}\": {ex.Message}");
            }

            // Save the modified document as a PDF
            doc.Save(outputPath);
            Console.WriteLine($"Updated PDF saved to \"{outputPath}\".");
        }
    }
}