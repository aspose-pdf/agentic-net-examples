using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Specify the PDF file using a FileInfo object
        FileInfo pdfFile = new FileInfo("input.pdf");

        // Verify that the file exists before proceeding
        if (!pdfFile.Exists)
        {
            Console.Error.WriteLine($"File not found: {pdfFile.FullName}");
            return;
        }

        // Open the PDF, modify its author metadata, and save back to the same path
        // Document disposal is handled via a using block (recommended lifecycle pattern)
        using (Document doc = new Document(pdfFile.FullName))
        {
            // Update the Author property in the document's metadata
            doc.Info.Author = "New Author Name";

            // Save the document, overwriting the original file
            doc.Save(pdfFile.FullName);
        }

        Console.WriteLine("Author metadata updated successfully.");
    }
}