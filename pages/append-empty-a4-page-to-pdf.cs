using System;
using System.IO;
using Aspose.Pdf; // Core API only – no Facades or Plugins namespaces

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

        // Load the existing PDF using the Document constructor (preferred over DocumentFactory)
        using (Document doc = new Document(inputPath))
        {
            // Append an empty page at the end of the document
            Page newPage = doc.Pages.Add();

            // Resize the newly added page to A4 dimensions (width & height are in points)
            newPage.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"A4 page appended and saved to '{outputPath}'.");
    }
}
