using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document. Document implements IDisposable, so wrap it in a using block.
        using (Document doc = new Document(pdfPath))
        {
            // Embedded files (attachments) are accessed via the Document.EmbeddedFiles collection.
            if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    string name = fileSpec.Name ?? string.Empty;
                    // Description may contain Unicode characters; output to verify proper encoding.
                    string description = fileSpec.Description ?? string.Empty;

                    Console.WriteLine($"Attachment: {name}");
                    Console.WriteLine($"Description: {description}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No embedded file attachments found.");
            }
        }
    }
}
