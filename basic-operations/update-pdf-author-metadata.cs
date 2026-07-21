using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file
        FileInfo pdfFile = new FileInfo("sample.pdf");

        if (!pdfFile.Exists)
        {
            Console.Error.WriteLine($"File not found: {pdfFile.FullName}");
            return;
        }

        // Open the PDF, modify author metadata, and save back to the same file
        using (Document doc = new Document(pdfFile.FullName))
        {
            // Set new author name
            doc.Info.Author = "John Doe";

            // Save changes overwriting the original file
            doc.Save(pdfFile.FullName);
        }

        Console.WriteLine("Author metadata updated successfully.");
    }
}