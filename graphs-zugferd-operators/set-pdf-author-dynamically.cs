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

        // Load the PDF, set the Author to the current system user, and save.
        using (Document doc = new Document(inputPath))
        {
            // Get the current Windows/OS user name.
            string currentUser = Environment.UserName;

            // Set the Author metadata property.
            doc.Info.Author = currentUser;

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author set to '{Environment.UserName}' and saved to '{outputPath}'.");
    }
}