using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file
        string pdfPath = "sample.pdf";

        // Ensure the file exists before proceeding
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Wrap the FileInfo in a variable for convenience
        FileInfo fileInfo = new FileInfo(pdfPath);

        // Open the PDF, modify metadata, and save back to the same location
        using (Document doc = new Document(fileInfo.FullName))
        {
            // Update the Author metadata
            doc.Info.Author = "New Author";

            // Save the changes back to the original file
            doc.Save(fileInfo.FullName);
        }

        Console.WriteLine("Author metadata updated successfully.");
    }
}