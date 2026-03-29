using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string editedPath = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"File not found: {originalPath}");
            return;
        }

        // Load the original PDF, perform a simple edit, and save as a new file
        using (Document originalDoc = new Document(originalPath))
        {
            // Example edit: add a blank page at the end
            originalDoc.Pages.Add();
            // Save the edited document
            originalDoc.Save(editedPath);
        }

        // Retrieve file size information
        FileInfo originalInfo = new FileInfo(originalPath);
        FileInfo editedInfo = new FileInfo(editedPath);

        long originalSize = originalInfo.Length;
        long editedSize = editedInfo.Length;

        Console.WriteLine($"Original size: {originalSize} bytes");
        Console.WriteLine($"Edited size: {editedSize} bytes");

        if (editedSize != originalSize)
        {
            Console.WriteLine("File size changed, indicating modifications were applied.");
        }
        else
        {
            Console.WriteLine("File size unchanged.");
        }
    }
}