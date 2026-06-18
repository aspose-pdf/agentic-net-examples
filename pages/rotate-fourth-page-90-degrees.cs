using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated_page4.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 4 pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Rotate the fourth page 90 degrees clockwise
            // Rotation.on90 corresponds to a 90° clockwise rotation
            doc.Pages[4].Rotate = Rotation.on90;

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page 4 rotated and saved to '{outputPath}'.");
    }
}