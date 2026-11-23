using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page with some sample text
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(new TextFragment("Sample PDF with three attachments."));

            // Helper to embed a file with a description
            void AddAttachment(string filePath, string description)
            {
                // Ensure the source file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"File not found: {filePath}");
                    return;
                }

                // Create a FileSpecification using only the file name (the name that will appear in the PDF)
                var fileSpec = new FileSpecification(Path.GetFileName(filePath), description);
                // Load the file contents into a memory stream and assign it to the specification
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(filePath));
                // Add the specification to the document's EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Attachment 1 with a distinct description
            AddAttachment("attachment1.txt", "First attachment description");

            // Attachment 2 with a distinct description
            AddAttachment("attachment2.jpg", "Second attachment description");

            // Attachment 3 with a distinct description
            AddAttachment("attachment3.pdf", "Third attachment description");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
