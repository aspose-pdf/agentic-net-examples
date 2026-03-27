using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            int beforeCount = doc.Pages.Count;

            // Delete the first page if the document has at least one page
            if (beforeCount > 0)
            {
                doc.Pages.Delete(1);
            }

            int afterCount = doc.Pages.Count;

            Console.WriteLine($"Pages before delete: {beforeCount}");
            Console.WriteLine($"Pages after delete: {afterCount}");
            Console.WriteLine(afterCount == beforeCount - 1 ? "Delete operation succeeded." : "Delete operation did not reduce page count as expected.");

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}