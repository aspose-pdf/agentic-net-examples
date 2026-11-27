using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string attachFile1 = "attachment1.txt";
        const string attachFile2 = "attachment2.txt";
        const string attachFile3 = "attachment3.txt";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachFile1) || !File.Exists(attachFile2) || !File.Exists(attachFile3))
        {
            Console.Error.WriteLine("One or more attachment files are missing.");
            return;
        }

        try
        {
            // Load the PDF using the high‑level Document API
            Document pdf = new Document(inputPdf);

            // Add three file attachments via Document.EmbeddedFiles
            AddAttachment(pdf, attachFile1);
            AddAttachment(pdf, attachFile2);
            AddAttachment(pdf, attachFile3);

            // Delete all attachments – use Delete() instead of non‑existent Clear()
            pdf.EmbeddedFiles.Delete();

            // Save the final PDF
            pdf.Save(outputPdf);

            Console.WriteLine($"Final PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void AddAttachment(Document pdf, string filePath)
    {
        // Create a FileSpecification for the attachment
        var fileSpec = new FileSpecification(Path.GetFileName(filePath))
        {
            Description = $"Attachment {Path.GetFileName(filePath)}",
            // Load the file bytes into a memory stream
            Contents = new MemoryStream(File.ReadAllBytes(filePath))
        };
        // Add to the PDF's embedded files collection
        pdf.EmbeddedFiles.Add(fileSpec);
    }
}
