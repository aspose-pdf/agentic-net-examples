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

        // Load the PDF document inside a using block for proper disposal
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Retrieve the current system user name
            string currentUser = Environment.UserName;

            // Set the Author metadata property dynamically
            doc.Info.Author = currentUser;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF author set to '{Environment.UserName}' and saved to '{outputPath}'.");
    }
}