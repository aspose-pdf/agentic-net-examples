using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string author = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create stamp value with current date and author using string interpolation
        string stampValue = $"{DateTime.Now:yyyy-MM-dd} - Author: {author}";

        // Initialize a TextStamp with the interpolated value
        TextStamp textStamp = new TextStamp(stampValue);
        textStamp.HorizontalAlignment = HorizontalAlignment.Center;
        textStamp.VerticalAlignment = VerticalAlignment.Center;
        textStamp.Opacity = 0.5f;
        textStamp.Draw = false; // render as text

        using (Document doc = new Document(inputPath))
        {
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(textStamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
