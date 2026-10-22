using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Stub definitions for unit‑test attributes/types when the testing framework is not referenced.
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    // Optional empty types that may be referenced directly in the source.
    public class TestClass { }
    public class TestMethod { }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "aligned_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Left aligned text added to the PDF.");

            // Align text to the left margin.
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Left;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with left‑aligned text to '{outputPath}'.");
    }
}