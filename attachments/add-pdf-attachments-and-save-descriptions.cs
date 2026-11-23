using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string attachmentPath1 = "attach1.txt";
        const string attachmentPath2 = "attach2.bin";
        const string outputPdf = "output_with_attachments.pdf";
        const string descOutput1 = "attachment1_desc.txt";
        const string descOutput2 = "attachment2_desc.txt";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(attachmentPath1) || !File.Exists(attachmentPath2))
        {
            Console.Error.WriteLine("One or both attachment files not found.");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // --- Add first attachment -------------------------------------------------
            var attach1 = new FileSpecification(Path.GetFileName(attachmentPath1))
            {
                Description = "Description for first attachment",
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath1))
            };
            doc.EmbeddedFiles.Add(attach1);

            // --- Add second attachment ------------------------------------------------
            var attach2 = new FileSpecification(Path.GetFileName(attachmentPath2))
            {
                Description = "Description for second attachment",
                Contents = new MemoryStream(File.ReadAllBytes(attachmentPath2))
            };
            doc.EmbeddedFiles.Add(attach2);

            // Save the PDF that now contains the embedded files
            doc.Save(outputPdf);

            // Retrieve each attachment's description and write it to a separate text file
            // (The order of EmbeddedFiles matches the order we added them.)
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count >= 2)
            {
                File.WriteAllText(descOutput1, doc.EmbeddedFiles[0].Description ?? string.Empty);
                File.WriteAllText(descOutput2, doc.EmbeddedFiles[1].Description ?? string.Empty);
            }
        }

        Console.WriteLine("Attachments added and descriptions saved.");
    }
}
