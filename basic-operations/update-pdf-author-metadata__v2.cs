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

        // Get the current user name from the environment
        string currentUser = Environment.UserName;

        // Load the PDF, update the Author metadata, and save
        using (Document doc = new Document(inputPath))
        {
            doc.Info.Author = currentUser;
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author updated to '{currentUser}' and saved to '{outputPath}'.");
    }
}