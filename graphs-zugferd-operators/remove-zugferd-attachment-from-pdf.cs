using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string attachmentName = "ZUGFeRD.xml"; // typical ZUGFeRD attachment name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // If the document contains embedded files, attempt to delete the ZUGFeRD attachment
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    try
                    {
                        doc.EmbeddedFiles.Delete(attachmentName);
                        Console.WriteLine($"Deleted attachment: {attachmentName}");
                    }
                    catch (Exception)
                    {
                        // Attachment not present – ignore
                        Console.WriteLine($"Attachment '{attachmentName}' not found.");
                    }
                }
                else
                {
                    Console.WriteLine("No embedded files found in the document.");
                }

                // Save the modified PDF, preserving all other content
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved without ZUGFeRD attachment to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}