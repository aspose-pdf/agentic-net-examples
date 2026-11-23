using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentListPath = "attachments.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Example attachment data; replace with real streams as needed
        byte[][] attachmentData = new byte[][]
        {
            System.Text.Encoding.UTF8.GetBytes("First attachment content"),
            System.Text.Encoding.UTF8.GetBytes("Second attachment content"),
            System.Text.Encoding.UTF8.GetBytes("Third attachment content")
        };

        string[] attachmentNames = new string[] { "First.txt", "Second.txt", "Third.txt" };

        // Load PDF, add attachments (as embedded files), and save
        using (Document doc = new Document(inputPath))
        {
            for (int i = 0; i < attachmentData.Length; i++)
            {
                // Create a FileSpecification for each attachment
                var fileSpec = new FileSpecification(attachmentNames[i]);
                fileSpec.Contents = new MemoryStream(attachmentData[i]);
                // Add the file specification to the EmbeddedFiles collection
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Write the attachment names to a text file
        File.WriteAllLines(attachmentListPath, attachmentNames);

        Console.WriteLine($"PDF saved to '{outputPath}'. Attachment list written to '{attachmentListPath}'.");
    }
}
