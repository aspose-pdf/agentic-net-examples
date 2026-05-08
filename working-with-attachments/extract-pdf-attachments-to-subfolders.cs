using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF containing attachments
        const string outputRoot   = "ExtractedAttachments"; // Base folder for extracted files

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the root output directory exists
        Directory.CreateDirectory(outputRoot);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // The EmbeddedFiles collection holds all file attachments in the PDF
            var attachments = pdfDoc.EmbeddedFiles;

            if (attachments == null || attachments.Count == 0)
            {
                Console.WriteLine("No attachments found in the PDF.");
                return;
            }

            int index = 1;
            foreach (var attachment in attachments)
            {
                // Create a subfolder for each attachment (e.g., Attachment_1, Attachment_2, ...)
                string subFolder = Path.Combine(outputRoot, $"Attachment_{index}");
                Directory.CreateDirectory(subFolder);

                // Retrieve the attachment name via reflection (avoids compile‑time dependency on EmbeddedFile type)
                string fileName = GetAttachmentName(attachment) ?? $"attachment_{index}";

                // Combine subfolder path with the file name
                string outputFilePath = Path.Combine(subFolder, fileName);

                // Save the attachment to disk using reflection (calls Save(string))
                SaveAttachment(attachment, outputFilePath);

                Console.WriteLine($"Saved attachment #{index} to: {outputFilePath}");
                index++;
            }
        }

        Console.WriteLine("All attachments have been extracted.");
    }

    // Helper: obtains the Name property of an embedded file via reflection
    private static string GetAttachmentName(object attachment)
    {
        var nameProp = attachment.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
        if (nameProp != null)
        {
            var value = nameProp.GetValue(attachment) as string;
            if (!string.IsNullOrWhiteSpace(value))
                return value;
        }
        return null;
    }

    // Helper: invokes the Save(string) method via reflection; falls back to stream copy if needed
    private static void SaveAttachment(object attachment, string path)
    {
        var saveMethod = attachment.GetType().GetMethod("Save", new[] { typeof(string) });
        if (saveMethod != null)
        {
            saveMethod.Invoke(attachment, new object[] { path });
            return;
        }

        // Fallback – extract the raw stream from FileSpecification if Save(string) is unavailable
        var fileSpecProp = attachment.GetType().GetProperty("FileSpecification");
        var fileSpec = fileSpecProp?.GetValue(attachment);
        var contentsProp = fileSpec?.GetType().GetProperty("Contents");
        var stream = contentsProp?.GetValue(fileSpec) as Stream;
        if (stream != null)
        {
            using (var outStream = File.Create(path))
            {
                stream.CopyTo(outStream);
            }
            return;
        }

        throw new InvalidOperationException("Unable to save attachment – no suitable Save method or stream found.");
    }
}