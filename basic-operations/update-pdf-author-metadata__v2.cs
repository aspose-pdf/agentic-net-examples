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

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the current OS user name
            string currentUser = Environment.UserName;

            // Replace the Author metadata with the current user name
            doc.Info.Author = currentUser;

            // Save the updated document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author metadata updated. Saved to '{outputPath}'.");
    }
}