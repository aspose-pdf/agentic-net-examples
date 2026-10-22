using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Minimal NUnit stubs to satisfy compilation when the NUnit package is not referenced
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex);
            }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "highlighted.pdf";
        const string keyword = "searchTerm";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create a TextFragmentAbsorber to find the keyword (case‑insensitive)
                TextFragmentAbsorber absorber = new TextFragmentAbsorber(keyword);
                absorber.TextSearchOptions = new TextSearchOptions(true); // true => ignore case

                // Extract matching fragments from all pages
                doc.Pages.Accept(absorber);

                // Highlight each found fragment with a yellow background
                foreach (TextFragment fragment in absorber.TextFragments)
                {
                    fragment.TextState.BackgroundColor = Aspose.Pdf.Color.Yellow;
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Keyword highlighted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
