using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Input file not found: {originalPath}");
            return;
        }

        // Get size of the original PDF
        long originalSize = new FileInfo(originalPath).Length;
        Console.WriteLine($"Original PDF size: {originalSize} bytes");

        // Edit the PDF: add a blank page and save as a new file
        using (Document doc = new Document(originalPath))
        {
            // Add a blank page at the end
            doc.Pages.Add();
            // Save the edited document
            doc.Save(editedPath);
        }

        // Verify that the edited file was created
        if (!File.Exists(editedPath))
        {
            Console.Error.WriteLine($"Failed to create edited PDF: {editedPath}");
            return;
        }

        // Get size of the edited PDF
        long editedSize = new FileInfo(editedPath).Length;
        Console.WriteLine($"Edited PDF size: {editedSize} bytes");

        // Use Aspose.Pdf.Facades to read some info from the edited PDF (optional)
        PdfFileInfo info = new PdfFileInfo(editedPath);
        Console.WriteLine($"Edited PDF page count: {info.NumberOfPages}");

        // Compare sizes
        if (editedSize != originalSize)
        {
            Console.WriteLine("File sizes differ – changes have been applied.");
        }
        else
        {
            Console.WriteLine("File sizes are identical – no changes detected.");
        }
    }
}