using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "Attachments";
        const string summaryPath = "attachment_summary.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the directory for extracted attachments exists
        Directory.CreateDirectory(outputDir);

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Access the collection of embedded files (attachments)
            var embeddedFiles = doc.EmbeddedFiles;

            // If there are no attachments, create an empty summary file and exit
            if (embeddedFiles == null || embeddedFiles.Count == 0)
            {
                File.WriteAllText(summaryPath, "No attachments found.");
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            // Build the summary content
            StringBuilder summaryBuilder = new StringBuilder();

            // Iterate over each attachment using FileSpecification (the correct type)
            foreach (FileSpecification fileSpec in embeddedFiles)
            {
                // Original file name of the attachment
                string originalName = fileSpec.Name;

                // Destination path for the extracted file
                string extractionPath = Path.Combine(outputDir, originalName);

                // Save the attachment to the file system using its Contents stream
                using (FileStream outStream = new FileStream(extractionPath, FileMode.Create, FileAccess.Write))
                using (Stream content = fileSpec.Contents)
                {
                    content.CopyTo(outStream);
                }

                // Record the mapping in the summary
                summaryBuilder.AppendLine($"{originalName} -> {extractionPath}");
            }

            // Write the summary file
            File.WriteAllText(summaryPath, summaryBuilder.ToString());
        }

        Console.WriteLine($"Attachments extracted to '{outputDir}'. Summary written to '{summaryPath}'.");
    }
}
