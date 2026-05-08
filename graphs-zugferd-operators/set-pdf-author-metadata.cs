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

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Get the current system user name
        string currentUser = Environment.UserName;

        // Open the PDF, set the Author metadata, and save the result
        using (Document doc = new Document(inputPath))
        {
            // Set the Author property in the document's Info dictionary
            doc.Info.Author = currentUser;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Author set to '{currentUser}' and saved to '{outputPath}'.");
    }
}