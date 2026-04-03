using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

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
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Check whether the page contains any vector graphics
            if (!page.HasVectorGraphics())
            {
                Console.WriteLine("The page does not contain vector graphics.");
            }
            else
            {
                // Use GraphicsAbsorber to locate all vector graphics on the page
                GraphicsAbsorber absorber = new GraphicsAbsorber();
                absorber.Visit(page);

                // Remove a specific vector graphic (the first one found in this example)
                foreach (var element in absorber.Elements)
                {
                    element.Remove();               // Deletes this vector graphic from the page
                    Console.WriteLine("A vector graphic was removed.");
                    break;                          // Stop after removing the desired graphic
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
