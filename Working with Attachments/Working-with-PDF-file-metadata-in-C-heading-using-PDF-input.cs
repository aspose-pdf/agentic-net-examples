using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Read existing standard metadata
            Console.WriteLine($"Title : {pdfInfo.Title}");
            Console.WriteLine($"Author: {pdfInfo.Author}");
            Console.WriteLine($"Subject: {pdfInfo.Subject}");
            Console.WriteLine($"Keywords: {pdfInfo.Keywords}");
            Console.WriteLine($"Creator: {pdfInfo.Creator}");
            Console.WriteLine($"CreationDate: {pdfInfo.CreationDate}");
            Console.WriteLine($"ModDate: {pdfInfo.ModDate}");

            // Read a custom metadata entry (if it exists)
            string customValue = pdfInfo.GetMetaInfo("CustomKey");
            if (!string.IsNullOrEmpty(customValue))
                Console.WriteLine($"CustomKey (before): {customValue}");

            // Update standard metadata
            pdfInfo.Title   = "Updated Document Title";
            pdfInfo.Author  = "John Doe";
            pdfInfo.Subject = "Metadata Example";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, C#";

            // Add or update a custom metadata entry
            pdfInfo.SetMetaInfo("CustomKey", "CustomValue");

            // Persist the changes to a new PDF file
            pdfInfo.SaveNewInfo(outputPath);
        }

        // Verify that the changes were saved
        using (PdfFileInfo updatedInfo = new PdfFileInfo(outputPath))
        {
            Console.WriteLine("\n--- After SaveNewInfo ---");
            Console.WriteLine($"Title : {updatedInfo.Title}");
            Console.WriteLine($"Author: {updatedInfo.Author}");
            Console.WriteLine($"Subject: {updatedInfo.Subject}");
            Console.WriteLine($"Keywords: {updatedInfo.Keywords}");
            Console.WriteLine($"CustomKey (after): {updatedInfo.GetMetaInfo("CustomKey")}");
        }

        Console.WriteLine($"\nMetadata updated PDF saved as '{outputPath}'.");
    }
}