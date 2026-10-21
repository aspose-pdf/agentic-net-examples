using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least input and output paths; optional --verbose flag
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.pdf> <output.pdf> [--verbose]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        bool verbose = Array.Exists(args, a => a.Equals("--verbose", StringComparison.OrdinalIgnoreCase));

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    // Define annotation rectangle (llx, lly, urx, ury)
                    var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                    // Create a text annotation
                    var txtAnn = new TextAnnotation(doc.Pages[i], rect)
                    {
                        Title = "Note",
                        Contents = $"Annotation on page {i}",
                        Open = true,
                        Icon = TextIcon.Note
                    };

                    // Add the annotation directly to the page's annotation collection
                    doc.Pages[i].Annotations.Add(txtAnn);

                    if (verbose)
                    {
                        Console.WriteLine($"Added text annotation to page {i} at {rect}");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
                if (verbose)
                {
                    Console.WriteLine($"Document saved to {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
