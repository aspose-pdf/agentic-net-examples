using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portfolio.pdf";   // PDF with embedded files (portfolio)
        const string outputPath = "portfolio_updated.pdf";
        const int fileIndexToRemove = 2; // zero‑based index of the embedded file to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the collection of embedded files
            EmbeddedFileCollection embeddedFiles = doc.EmbeddedFiles;

            // Find the name of the file at the requested index using reflection (avoids direct dependency on EmbeddedFile type)
            string? nameToDelete = null;
            int currentIndex = 0;
            foreach (var fileObj in embeddedFiles)
            {
                if (currentIndex == fileIndexToRemove)
                {
                    var nameProp = fileObj.GetType().GetProperty("Name");
                    if (nameProp != null)
                    {
                        nameToDelete = nameProp.GetValue(fileObj) as string;
                    }
                    break;
                }
                currentIndex++;
            }

            if (!string.IsNullOrEmpty(nameToDelete))
            {
                // Delete the embedded file by its name
                embeddedFiles.Delete(nameToDelete);
                Console.WriteLine($"Deleted embedded file: {nameToDelete}");
            }
            else
            {
                Console.WriteLine($"No embedded file found at index {fileIndexToRemove}.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
