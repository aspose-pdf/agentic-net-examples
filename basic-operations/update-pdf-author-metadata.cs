using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF file (can be obtained from any source)
        FileInfo pdfFile = new FileInfo("sample.pdf");

        if (!pdfFile.Exists)
        {
            Console.Error.WriteLine($"File not found: {pdfFile.FullName}");
            return;
        }

        // Open the PDF, modify metadata, and save back to the same location
        using (Document doc = new Document(pdfFile.FullName))
        {
            // Update the Author metadata
            doc.Info.Author = "New Author Name";

            // Save changes overwriting the original file
            doc.Save(pdfFile.FullName);
        }

        Console.WriteLine("Author metadata updated successfully.");
    }
}