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

        // PdfFileInfo implements IDisposable – wrap it in a using block.
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            // Retrieve the current Author metadata.
            string currentAuthor = info.Author;

            // Update only if the Author field is empty or whitespace.
            if (string.IsNullOrWhiteSpace(currentAuthor))
            {
                info.Author = newAuthor;
                // Save the modified PDF to a new file.
                info.Save(outputPath);
                Console.WriteLine($"Author set to '{newAuthor}' and saved as '{outputPath}'.");
            }
            else
            {
                Console.WriteLine($"Existing Author: '{currentAuthor}'. No update performed.");
                // Optionally copy the original file to the output location.
                File.Copy(inputPath, outputPath, true);
            }
        }
    }
}