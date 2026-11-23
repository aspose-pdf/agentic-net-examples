using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string summaryPath = "attachments_summary.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Embedded files are stored in the EmbeddedFiles collection as FileSpecification objects
            var embeddedFiles = doc.EmbeddedFiles;

            // Prepare a summary using StringBuilder
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Attachment summary for '{Path.GetFileName(pdfPath)}':");
            sb.AppendLine($"Total attachments: {embeddedFiles.Count}");
            sb.AppendLine();

            long totalSize = 0;

            // Enumerate each embedded file (FileSpecification)
            foreach (FileSpecification fileSpec in embeddedFiles)
            {
                // Guard against null values
                string name = fileSpec.Name ?? "(unnamed)";
                long size = 0;
                if (fileSpec.Contents != null && fileSpec.Contents.CanSeek)
                {
                    size = fileSpec.Contents.Length;
                }
                totalSize += size;

                sb.AppendLine($"- Name: {name}");
                sb.AppendLine($"  Size: {size} bytes");
                sb.AppendLine();
            }

            sb.AppendLine($"Total size of all attachments: {totalSize} bytes");

            // Write the summary to a text file
            File.WriteAllText(summaryPath, sb.ToString());

            Console.WriteLine($"Attachment summary written to '{summaryPath}'.");
        }
    }
}
