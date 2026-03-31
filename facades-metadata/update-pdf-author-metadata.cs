using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string newAuthor = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                string currentAuthor = doc.Info.Author;
                if (string.IsNullOrEmpty(currentAuthor))
                {
                    doc.Info.Author = newAuthor;
                    Console.WriteLine("Author metadata was empty. Updated to new author.");
                }
                else
                {
                    Console.WriteLine($"Existing author: '{currentAuthor}'. No change made.");
                }

                doc.Save(outputPath);
                Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
