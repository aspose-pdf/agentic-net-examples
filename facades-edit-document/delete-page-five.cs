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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Delete page 5 if it exists
                if (doc.Pages.Count >= 5)
                {
                    doc.Pages.Delete(5);
                }
                else
                {
                    Console.WriteLine("Document has fewer than 5 pages; no deletion performed.");
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"Page 5 deleted. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
