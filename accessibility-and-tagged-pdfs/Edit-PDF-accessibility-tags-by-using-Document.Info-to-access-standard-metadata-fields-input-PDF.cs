using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the standard metadata via Document.Info
                DocumentInfo info = doc.Info;

                // Update predefined metadata fields
                info.Title = "Updated Document Title";
                info.Author = "John Doe";
                info.Subject = "Accessibility Metadata Update";
                info.Keywords = "PDF, Accessibility, Metadata";
                info.Creator = "My Application";

                // Add a custom metadata entry (key/value pair)
                info["CustomField"] = "CustomValue";

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Metadata updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}