using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file (replace with your actual file path)
        var fileInfo = new FileInfo("sample.pdf");
        if (!fileInfo.Exists)
        {
            Console.Error.WriteLine($"File not found: {fileInfo.FullName}");
            return;
        }

        // Open the PDF, modify author metadata, and save back to the same file
        using (Document pdf = new Document(fileInfo.FullName))
        {
            pdf.Info.Author = "John Doe"; // set new author
            pdf.Save(fileInfo.FullName);   // overwrite original file
        }

        Console.WriteLine("Author metadata updated successfully.");
    }
}