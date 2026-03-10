using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string embeddedFileName = "sample.txt"; // name of the embedded file to delete

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
                // Delete a specific embedded file by its name (if it exists)
                if (doc.EmbeddedFiles.Count > 0)
                {
                    doc.EmbeddedFiles.Delete(embeddedFileName);
                }

                // To delete all embedded files, uncomment the following line:
                // doc.EmbeddedFiles.Delete();

                // Save the modified document as PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Embedded files removed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}