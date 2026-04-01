using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Minimal NUnit stubs – they allow the test code to compile without referencing the real NUnit package.
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

    public static class Assert
    {
        // Simple equality check with optional tolerance for floating‑point values.
        public static void AreEqual(double expected, double actual, double tolerance, string message = null)
        {
            if (Math.Abs(expected - actual) > tolerance)
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>. Tolerance:<{tolerance}>.");
        }
    }
}

public static class PdfHelper
{
    /// <summary>
    /// Adds a single text fragment at the specified coordinates to the first page of the source PDF.
    /// </summary>
    public static void AddText(string sourcePath, string destPath, string text, float x, float y)
    {
        using (Document doc = new Document(sourcePath))
        {
            Page page = doc.Pages[1];
            TextFragment tf = new TextFragment(text);
            tf.Position = new Position(x, y);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Black;

            new TextBuilder(page).AppendText(tf);
            doc.Save(destPath);
        }
    }

    /// <summary>
    /// Returns the X and Y coordinates of the first occurrence of <paramref name="searchText"/> on the first page.
    /// </summary>
    public static (float X, float Y) GetTextPosition(string pdfPath, string searchText)
    {
        using (Document doc = new Document(pdfPath))
        {
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[1].Accept(absorber);
            if (absorber.TextFragments.Count == 0)
                throw new InvalidOperationException("Text not found in the document.");

            // NOTE: TextFragmentCollection is 1‑based, not zero‑based.
            TextFragment found = absorber.TextFragments[1];
            return ((float)found.Position.XIndent, (float)found.Position.YIndent);
        }
    }
}

[NUnit.Framework.TestFixture]
public class PdfHelperTests
{
    private const string OriginalPath = "original.pdf";
    private const string OutputPath = "output.pdf";
    private const string SearchText = "Sample Text";
    private const float ExpectedX = 100f;
    private const float ExpectedY = 200f;
    private const float Tolerance = 0.1f;

    [NUnit.Framework.SetUp]
    public void SetUp()
    {
        // Create a blank PDF with a single page for each test run.
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(OriginalPath);
        }
    }

    [NUnit.Framework.TearDown]
    public void TearDown()
    {
        if (File.Exists(OriginalPath)) File.Delete(OriginalPath);
        if (File.Exists(OutputPath)) File.Delete(OutputPath);
    }

    [NUnit.Framework.Test]
    public void AddText_ShouldPlaceTextAtExpectedCoordinates()
    {
        // Act – add the text.
        PdfHelper.AddText(OriginalPath, OutputPath, SearchText, ExpectedX, ExpectedY);

        // Assert – verify the coordinates.
        var (actualX, actualY) = PdfHelper.GetTextPosition(OutputPath, SearchText);
        NUnit.Framework.Assert.AreEqual(ExpectedX, actualX, Tolerance, "X coordinate does not match expected value.");
        NUnit.Framework.Assert.AreEqual(ExpectedY, actualY, Tolerance, "Y coordinate does not match expected value.");
    }
}

// Optional console entry point – mirrors the original program logic.
class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string outputPath = "output.pdf";
        const string searchText = "Sample Text";
        const float expectedX = 100f;
        const float expectedY = 200f;
        const float tolerance = 0.1f;

        // Create a simple PDF with one blank page.
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save(originalPath);
        }

        // Add text at the required coordinates.
        PdfHelper.AddText(originalPath, outputPath, searchText, expectedX, expectedY);

        // Verify the text position using the helper.
        var (actualX, actualY) = PdfHelper.GetTextPosition(outputPath, searchText);
        bool xMatch = Math.Abs(actualX - expectedX) < tolerance;
        bool yMatch = Math.Abs(actualY - expectedY) < tolerance;
        if (xMatch && yMatch)
            Console.WriteLine("PASS: Text position matches expected coordinates.");
        else
            Console.WriteLine($"FAIL: Expected ({expectedX}, {expectedY}) but found ({actualX}, {actualY}).");
    }
}