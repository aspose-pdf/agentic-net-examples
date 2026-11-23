using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputTxt = "attachments.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Ensure the document actually contains embedded files
            if (doc.EmbeddedFiles == null || doc.EmbeddedFiles.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                File.WriteAllText(outputTxt, string.Empty);
                return;
            }

            // Prepare a StringWriter to collect attachment names
            using (StringWriter writer = new StringWriter())
            {
                // Aspose.Pdf uses 1‑based indexing for collections
                for (int i = 1; i <= doc.EmbeddedFiles.Count; i++)
                {
                    // Each item is a FileSpecification
                    Aspose.Pdf.FileSpecification fileSpec = doc.EmbeddedFiles[i];

                    // Use the Name property; if empty generate a fallback name
                    string name = fileSpec.Name;
                    if (string.IsNullOrEmpty(name))
                    {
                        // Fallback format: "attachment_<index>"
                        name = $"attachment_{i}";
                    }

                    writer.WriteLine(name);
                }

                // Write all collected names to the output text file
                File.WriteAllText(outputTxt, writer.ToString());
            }

            Console.WriteLine($"Attachment names saved to '{outputTxt}'.");
        }
    }
}