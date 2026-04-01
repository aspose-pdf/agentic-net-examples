using System;
using System.IO;
using Aspose.Pdf;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputPath = "input.pdf";
            string outputPath = "output.pdf";

            // Load the source PDF if it exists; otherwise start with an empty document.
            Document document;
            if (File.Exists(inputPath))
            {
                document = new Document(inputPath);
            }
            else
            {
                // Create a new, empty PDF document.
                document = new Document();
                // (Optional) you could add a blank page here so the document is not completely empty.
            }

            // Insert an empty page at the beginning (position 1, because indexing is 1‑based)
            Page newPage = document.Pages.Insert(1);
            // Set custom dimensions: 200 points width, 300 points height
            newPage.SetPageSize(200.0, 300.0);

            document.Save(outputPath);
            Console.WriteLine($"Inserted empty page of size 200x300 points at the beginning of '{outputPath}'.");
        }
    }
}
