using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add JavaScript that runs when the first page is opened (page‑level script)
            Page page = doc.Pages[1];
            page.Actions.OnOpen = new JavascriptAction("app.alert('You have reached page 1');");

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with page‑level JavaScript saved to '{outputPath}'.");
    }
}
