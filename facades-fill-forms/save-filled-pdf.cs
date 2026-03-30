using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Optional: modify the document (e.g., fill a form field)
            // if (doc.Form != null && doc.Form["Name"] != null)
            // {
            //     doc.Form["Name"].Value = "John Doe";
            // }

            // Save the document preserving the original layout
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}