using System;
using System.IO;
using Aspose.Pdf;

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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the current system user name
            string currentUser = Environment.UserName;

            // Set the PDF author property dynamically
            doc.Info.Author = currentUser;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author set to current user and saved as '{outputPath}'.");
    }
}