using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, set the Author metadata to the current system user,
        // and save the modified document.
        using (Document doc = new Document(inputPath))
        {
            // Get the current Windows user name (e.g., "DOMAIN\\User")
            string currentUser = Environment.UserName;

            // Set the Author property in the document's Info dictionary
            doc.Info.Author = currentUser;

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author set to current user and saved to '{outputPath}'.");
    }
}