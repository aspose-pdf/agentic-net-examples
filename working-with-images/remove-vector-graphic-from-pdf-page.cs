using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

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
            // Select the page from which the vector graphic will be removed (first page)
            Page page = doc.Pages[1];

            // Absorb all vector graphics on the page
            GraphicsAbsorber absorber = new GraphicsAbsorber();
            absorber.Visit(page);

            // Check whether any vector graphics were found
            if (absorber.Elements.Count == 0)
            {
                Console.WriteLine("No vector graphics found on the page.");
            }
            else
            {
                // Example: remove the first vector graphic found
                int targetIndex = 0;
                if (targetIndex < absorber.Elements.Count)
                {
                    // Remove this vector graphic from the page
                    absorber.Elements[targetIndex].Remove();
                    Console.WriteLine($"Removed vector graphic at index {targetIndex}.");
                }
                else
                {
                    Console.WriteLine($"Target index {targetIndex} is out of range. Total graphics: {absorber.Elements.Count}.");
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
