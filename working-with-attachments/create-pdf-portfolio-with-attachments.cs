using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Output PDF Portfolio file path
        const string outputPath = "portfolio.pdf";

        // Create an empty PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required for a valid PDF)
            Page page = doc.Pages.Add();

            // Add a simple text fragment to the page (optional)
            TextFragment tf = new TextFragment("PDF Portfolio")
            {
                Position = new Position(100, 700)
            };
            tf.TextState.FontSize = 24;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            page.Paragraphs.Add(tf);

            // Add file attachments (portfolio items)
            // Each attachment is represented by a FileSpecification object
            // Adjust the file paths as needed; they must exist on disk
            string[] filesToAttach = { "file1.pdf", "image1.png", "document1.docx" };
            foreach (string filePath in filesToAttach)
            {
                if (!File.Exists(filePath))
                {
                    Console.Error.WriteLine($"Attachment not found: {filePath}");
                    continue;
                }

                // Create a FileSpecification that embeds the file.
                // The first argument is the physical file path, the second argument is a description (optional).
                FileSpecification attachment = new FileSpecification(filePath, Path.GetFileName(filePath));

                // Add the attachment to the document's EmbeddedFiles collection.
                doc.EmbeddedFiles.Add(attachment);
            }

            // Save the PDF Portfolio
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF Portfolio created at '{outputPath}'.");
    }
}
