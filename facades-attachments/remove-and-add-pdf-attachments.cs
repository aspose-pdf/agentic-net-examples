using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the high‑level Document API.
        Document pdf = new Document(inputPath);

        // ----- Remove any existing embedded file attachments -----
        if (pdf.EmbeddedFiles != null && pdf.EmbeddedFiles.Count > 0)
        {
            // EmbeddedFileCollection uses 1‑based indexing and provides a Delete(string name) method.
            for (int i = pdf.EmbeddedFiles.Count; i >= 1; i--)
            {
                FileSpecification spec = pdf.EmbeddedFiles[i];
                if (spec != null && !string.IsNullOrEmpty(spec.Name))
                {
                    pdf.EmbeddedFiles.Delete(spec.Name);
                }
            }
        }

        // ----- Add first attachment (from a memory stream) -----
        byte[] firstData = Encoding.UTF8.GetBytes("First attachment content");
        var firstSpec = new FileSpecification("FirstAttachment.txt")
        {
            Description = "First attachment",
            // The Contents property expects a Stream.
            Contents = new MemoryStream(firstData)
        };
        pdf.EmbeddedFiles.Add(firstSpec);

        // ----- Add second attachment (from a file or fallback data) -----
        const string secondFile = "image.png";
        if (File.Exists(secondFile))
        {
            byte[] secondData = File.ReadAllBytes(secondFile);
            var secondSpec = new FileSpecification("ImageAttachment.png")
            {
                Description = "Image attachment",
                Contents = new MemoryStream(secondData)
            };
            pdf.EmbeddedFiles.Add(secondSpec);
        }
        else
        {
            // Fallback: add dummy binary data if the file is missing.
            byte[] secondData = new byte[] { 0x00, 0x01, 0x02 };
            var dummySpec = new FileSpecification("DummyAttachment.bin")
            {
                Description = "Dummy attachment",
                Contents = new MemoryStream(secondData)
            };
            pdf.EmbeddedFiles.Add(dummySpec);
        }

        // Save the modified PDF with the new attachments.
        pdf.Save(outputPath);

        Console.WriteLine($"PDF saved with attachments to '{outputPath}'.");
    }
}
