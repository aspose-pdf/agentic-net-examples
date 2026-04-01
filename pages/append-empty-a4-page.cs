using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Load the existing PDF if it exists; otherwise start with a new empty document.
        using (Document document = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Add a new empty A4‑sized page at the end of the document.
            Page newPage = document.Pages.Add();
            newPage.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // Save the modified document.
            document.Save(outputPath);
        }

        Console.WriteLine($"Added A4 page and saved to {outputPath}");
    }
}