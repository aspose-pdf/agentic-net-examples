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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Rotate the first page 90 degrees clockwise
            Page page = doc.Pages[1];
            page.Rotate = Rotation.on90; // corrected enum value

            // Retrieve the page rectangle taking rotation into account
            Aspose.Pdf.Rectangle rect = page.GetPageRect(true);
            Console.WriteLine($"After rotation: Width = {rect.Width}, Height = {rect.Height}");

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine("Operation completed.");
    }
}
