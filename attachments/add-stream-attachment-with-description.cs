using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string attachmentFileName = "sample.txt";
        const string attachmentDescription = "This is a sample attachment description.";
        const string outputPdfPath = "output_with_attachment.pdf";
        const string descriptionOutputPath = "attachment_description.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a memory stream containing the attachment data
            using (MemoryStream attachmentStream = new MemoryStream())
            using (StreamWriter writer = new StreamWriter(attachmentStream, Encoding.UTF8, 1024, true))
            {
                writer.Write("Content of the attached file.");
                writer.Flush();
                attachmentStream.Position = 0; // reset to beginning

                // Create a FileSpecification, set its description and contents, then add to EmbeddedFiles
                var fileSpec = new FileSpecification(attachmentFileName);
                fileSpec.Description = attachmentDescription;
                fileSpec.Contents = attachmentStream;
                doc.EmbeddedFiles.Add(fileSpec);
            }

            // Save the PDF that now contains the embedded file
            doc.Save(outputPdfPath);

            // Collect descriptions of all embedded files (including the one just added)
            var sb = new StringBuilder();
            foreach (FileSpecification spec in doc.EmbeddedFiles)
            {
                sb.AppendLine(spec.Description);
            }

            // Write the collected descriptions to a text file
            File.WriteAllText(descriptionOutputPath, sb.ToString());
            Console.WriteLine($"Attachment descriptions saved to '{descriptionOutputPath}'.");
        }
    }
}
