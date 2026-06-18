using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file
        string pdfPath = "sample.pdf";

        // Create a FileInfo object for the PDF
        FileInfo fileInfo = new FileInfo(pdfPath);
        if (!fileInfo.Exists)
        {
            Console.Error.WriteLine($"File not found: {fileInfo.FullName}");
            return;
        }

        // Open the PDF, modify the author, and save back to the same location
        using (Document doc = new Document(fileInfo.FullName))
        {
            // Update the Author metadata
            doc.Info.Author = "New Author Name";

            // Save the changes to the original file
            doc.Save(fileInfo.FullName);
        }

        Console.WriteLine($"Author metadata updated for '{fileInfo.FullName}'.");
    }
}