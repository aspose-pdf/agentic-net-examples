using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level Document API (attachments are managed here)
        Document pdf = new Document(inputPath);

        // Remove the first attachment if any exist
        try
        {
            if (pdf.EmbeddedFiles != null && pdf.EmbeddedFiles.Count > 0)
            {
                // Delete the first attachment by its name (Delete expects a string, not an index)
                string firstAttachmentName = pdf.EmbeddedFiles[0].Name;
                pdf.EmbeddedFiles.Delete(firstAttachmentName);
                Console.WriteLine("Attachment removed.");
            }
            else
            {
                Console.WriteLine("No attachments found – nothing to remove.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Attachment removal skipped: {ex.Message}");
        }

        // Save the cleaned PDF
        pdf.Save(outputPath);
        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}
