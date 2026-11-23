using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "sample.pdf";
        const string outputPath = "removedsummary.pdf";
        const string bookmarkTitle = "Summary";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, delete the specified bookmark, and save the result.
        using (Document doc = new Document(inputPath))
        {
            // Delete the bookmark with the given title if it exists.
            // OutlineCollection.Delete(string) removes the first outline matching the title.
            doc.Outlines.Delete(bookmarkTitle);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Bookmark \"{bookmarkTitle}\" removed. Saved as \"{outputPath}\".");
    }
}