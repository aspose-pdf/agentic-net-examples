using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string csvPath = "attachments_report.csv";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Prepare CSV content with header
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name,Size (bytes),Description");

            // Iterate over all embedded file attachments (use EmbeddedFiles collection)
            if (doc.EmbeddedFiles != null)
            {
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    // Attachment name (may be null)
                    string name = fileSpec.Name ?? string.Empty;

                    // Attachment description (may be null)
                    string description = fileSpec.Description ?? string.Empty;

                    // Determine the size of the embedded file by copying its Contents stream to a memory stream
                    long size = 0;
                    if (fileSpec.Contents != null)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            fileSpec.Contents.CopyTo(ms);
                            size = ms.Length;
                        }
                    }

                    // Escape commas and quotes for CSV compliance
                    string escapedName = $"\"{name.Replace("\"", "\"\"")}\"";
                    string escapedDescription = $"\"{description.Replace("\"", "\"\"")}\"";

                    sb.AppendLine($"{escapedName},{size},{escapedDescription}");
                }
            }

            // Write the CSV data to the output file (UTF‑8 encoding)
            File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Attachment report saved to '{csvPath}'.");
    }
}
