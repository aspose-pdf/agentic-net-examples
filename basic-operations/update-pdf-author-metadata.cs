using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Locate the PDF file via FileInfo
        FileInfo pdfFile = new FileInfo("input.pdf");
        if (!pdfFile.Exists)
        {
            Console.Error.WriteLine($"File not found: {pdfFile.FullName}");
            return;
        }

        // Load, modify metadata, and save back using a using block for deterministic disposal
        using (Document doc = new Document(pdfFile.FullName))
        {
            // Update the Author metadata
            doc.Info.Author = "New Author Name";

            // Save changes to the original file path
            doc.Save(pdfFile.FullName);
        }

        Console.WriteLine("Author metadata updated successfully.");
    }
}