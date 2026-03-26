using System;
using System.IO;
using Aspose.Pdf;

public class Program
{
    public static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.html";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // The Document class does not expose a Progress event. For a simple conversion we just save the file.
        // If progress reporting is required, use a facade class (e.g., PdfConverter) and report progress manually.
        using (Document doc = new Document(inputPath))
        {
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            doc.Save(outputPath, saveOptions);
        }

        Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
    }
}
