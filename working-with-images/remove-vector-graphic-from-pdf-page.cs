using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // for Path operator

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

        // Use a using block for deterministic disposal of the Document
        using (Document doc = new Document(inputPath))
        {
            // Pages collection is 1‑based (global rule)
            Page page = doc.Pages[1];

            // The Contents collection is also 1‑based (see fix note).
            // Iterate over the operators and remove the first Path operator we encounter.
            for (int i = 1; i <= page.Contents.Count; i++)
            {
                // Operators on a page are represented by classes derived from Aspose.Pdf.Operators.Operator.
                // The vector graphic we want to delete is a Path operator.
                if (page.Contents[i] is Path)
                {
                    // Remove the identified vector graphic from the contents collection.
                    page.Contents.Delete(i);
                    Console.WriteLine($"Removed vector graphic (Path operator) at index {i} from page 1.");
                    break; // exit after removal if only one graphic is targeted
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
