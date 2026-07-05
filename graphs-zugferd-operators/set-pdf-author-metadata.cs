using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set the Author metadata, and save
        using (Document doc = new Document(inputPath)) // load
        {
            // Set the Author property to the current system user name
            doc.Info.Author = Environment.UserName;

            // Save the modified PDF
            doc.Save(outputPath); // save
        }

        Console.WriteLine($"Author set to '{Environment.UserName}' and saved to '{outputPath}'.");
    }
}