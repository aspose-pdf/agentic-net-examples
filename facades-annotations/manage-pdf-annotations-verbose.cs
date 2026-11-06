using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: inputPdf outputPdf [--verbose]
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <input.pdf> <output.pdf> [--verbose]");
            return;
        }

        string inputPath  = args[0];
        string outputPath = args[1];
        bool verbose      = Array.Exists(args, a => a.Equals("--verbose", StringComparison.OrdinalIgnoreCase));

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (using ensures proper disposal)
            using (Document doc = new Document(inputPath))
            {
                int pageCount = doc.Pages.Count;

                // Define a rectangle for the annotation (coordinates are in points)
                // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 300, 750);

                // Add a text annotation to each page
                for (int i = 1; i <= pageCount; i++)
                {
                    // Use the constructor that binds the annotation to a specific page
                    TextAnnotation annotation = new TextAnnotation(doc.Pages[i], rect)
                    {
                        Title    = "Note",
                        Contents = $"Annotation on page {i}"
                    };
                    doc.Pages[i].Annotations.Add(annotation);

                    if (verbose)
                    {
                        Console.WriteLine($"Added text annotation to page {i}");
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
                if (verbose)
                {
                    Console.WriteLine($"Annotated PDF saved to '{outputPath}'");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
