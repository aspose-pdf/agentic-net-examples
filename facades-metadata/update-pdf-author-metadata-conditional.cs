using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor  = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load PDF metadata using the Facade class PdfFileInfo
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Update Author only if it is empty or whitespace
            if (string.IsNullOrWhiteSpace(info.Author))
            {
                info.Author = newAuthor;
                // Save the modified metadata to a new file
                info.Save(outputPath);
                Console.WriteLine($"Author set to '{newAuthor}' and saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine($"Existing Author: '{info.Author}'. No changes made.");
                // Preserve the original file if no update is needed
                File.Copy(inputPath, outputPath, true);
            }
        }
    }
}