using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputRoot = "Attachments";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the root folder for extracted attachments exists
        Directory.CreateDirectory(outputRoot);

        try
        {
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document doc = new Document(inputPath))
            {
                int index = 1;

                // Iterate over each embedded file in the PDF
                foreach (FileSpecification fileSpec in doc.EmbeddedFiles)
                {
                    if (fileSpec == null || fileSpec.Contents == null)
                        continue; // skip invalid entries

                    // Create a unique subfolder for this attachment
                    string safeName = SanitizeFileName(fileSpec.Name);
                    string subFolder = Path.Combine(outputRoot, $"Attachment_{index}_{safeName}");
                    Directory.CreateDirectory(subFolder);

                    // Determine the file name to write (fallback if name is missing)
                    string fileName = string.IsNullOrEmpty(fileSpec.Name)
                        ? $"attachment_{index}"
                        : fileSpec.Name;

                    string filePath = Path.Combine(subFolder, fileName);

                    // Write the embedded file's binary content to disk
                    using (Stream content = fileSpec.Contents)
                    {
                        // Ensure the stream is at the beginning
                        if (content.CanSeek)
                            content.Position = 0;

                        using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        {
                            content.CopyTo(fs);
                        }
                    }

                    Console.WriteLine($"Extracted '{fileName}' to '{subFolder}'");
                    index++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Removes characters that are invalid in file or folder names
    static string SanitizeFileName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return string.Empty;

        foreach (char c in Path.GetInvalidFileNameChars())
            name = name.Replace(c, '_');

        return name;
    }
}
