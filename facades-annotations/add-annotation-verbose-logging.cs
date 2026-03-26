using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

public class Program
{
    public static void Main(string[] args)
    {
        string inputPath = null;
        string outputPath = null;
        bool verbose = false;

        foreach (string arg in args)
        {
            if (arg.Equals("--verbose", StringComparison.OrdinalIgnoreCase))
            {
                verbose = true;
            }
            else if (inputPath == null)
            {
                inputPath = arg;
            }
            else if (outputPath == null)
            {
                outputPath = arg;
            }
        }

        if (string.IsNullOrEmpty(inputPath) || string.IsNullOrEmpty(outputPath))
        {
            Console.Error.WriteLine("Usage: program.exe <input.pdf> <output.pdf> [--verbose]");
            return;
        }

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                if (verbose)
                {
                    doc.EnableNotificationLogging = true;
                }

                Page page = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation annotation = new TextAnnotation(page, rect);
                annotation.Title = "Verbose Example";
                annotation.Contents = "Annotation added with verbose logging.";
                page.Annotations.Add(annotation);

                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
