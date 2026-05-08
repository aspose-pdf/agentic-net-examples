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
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        // Load the original PDF, make a simple edit (add a blank page), and save as edited PDF
        using (Document doc = new Document(originalPath))
        {
            // Simple edit: add an empty page at the end
            doc.Pages.Add();

            // Save the edited document
            doc.Save(editedPath);
        }

        // Use PdfFileInfo (Facade) to bind each PDF – demonstrates Facades usage
        PdfFileInfo originalInfo = new PdfFileInfo(originalPath);
        PdfFileInfo editedInfo   = new PdfFileInfo(editedPath);

        // Retrieve file sizes via System.IO (no direct size property in PdfFileInfo)
        long originalSize = new FileInfo(originalPath).Length;
        long editedSize   = new FileInfo(editedPath).Length;

        Console.WriteLine($"Original PDF size: {originalSize} bytes");
        Console.WriteLine($"Edited PDF size:   {editedSize} bytes");

        if (originalSize != editedSize)
        {
            Console.WriteLine("File sizes differ – changes have been applied.");
        }
        else
        {
            Console.WriteLine("File sizes are identical – no changes detected.");
        }

        // Clean up Facade objects
        originalInfo.Close();
        editedInfo.Close();
    }
}