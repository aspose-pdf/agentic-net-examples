using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_underline.pdf";
        const string phrase = "specific phrase"; // phrase to underline

        // ------------------------------------------------------------
        // Ensure the Aspose.Pdf assembly can be found (case‑sensitive file systems)
        // ------------------------------------------------------------
        string asposeDll = Path.Combine(AppContext.BaseDirectory, "Aspose.Pdf.dll");
        if (!File.Exists(asposeDll))
        {
            Console.Error.WriteLine($"Aspose.Pdf.dll not found at '{asposeDll}'. Make sure the library is present.");
            return;
        }
        // Load the assembly explicitly to avoid case‑sensitivity issues on Linux/macOS
        AssemblyLoadContext.Default.LoadFromAssemblyPath(asposeDll);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Search for the phrase on page 4 (1‑based indexing)
            const int targetPage = 4;
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(phrase);
            doc.Pages[targetPage].Accept(absorber);

            // Ensure the phrase was found
            if (absorber.TextFragments.Count == 0)
            {
                Console.Error.WriteLine($"Phrase \"{phrase}\" not found on page {targetPage}.");
                return;
            }

            // Get the first occurrence rectangle (fully‑qualified type)
            TextFragment fragment = absorber.TextFragments[0];
            Aspose.Pdf.Rectangle rect = fragment.Rectangle;

            // Create an underline annotation and add it to the page
            Page page = doc.Pages[targetPage];
            UnderlineAnnotation underline = new UnderlineAnnotation(page, rect)
            {
                Color = Aspose.Pdf.Color.Blue // fully‑qualified color type
            };
            page.Annotations.Add(underline);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underline annotation added. Saved to '{outputPath}'.");
    }
}
