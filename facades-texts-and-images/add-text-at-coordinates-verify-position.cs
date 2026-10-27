using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

// Minimal NUnit stubs to allow compilation when NUnit package is not referenced
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
        public static void AreEqual(double expected, double actual, double delta, string message = null)
        {
            if (Math.Abs(expected - actual) > delta)
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:{expected} Actual:{actual} Delta:{delta}");
        }

        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

// Test class verifying that added text appears at the expected X and Y coordinates
namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class TextPositionTests
    {
        private const double ExpectedX = 100.0;
        private const double ExpectedY = 200.0;
        private const string SampleText = "Hello, Aspose!";

        private MemoryStream _pdfStream;

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            // Create a new PDF document in memory
            var doc = new Document();

            // Add a single page
            var page = doc.Pages.Add();

            // Create a text fragment, set its position, and add it to the page
            var textFragment = new TextFragment(SampleText);
            // Position uses XIndent/YIndent in Aspose.Pdf
            textFragment.Position = new Position(ExpectedX, ExpectedY);
            page.Paragraphs.Add(textFragment);

            // Save the document to a memory stream for later verification
            _pdfStream = new MemoryStream();
            doc.Save(_pdfStream);
            // Reset stream position for reading
            _pdfStream.Position = 0;
        }

        [NUnit.Framework.Test]
        public void Verify_Text_Position_Is_Correct()
        {
            // Load the PDF from the memory stream
            var loadedDoc = new Document(_pdfStream);

            // Use TextFragmentAbsorber to extract text fragments with their positions
            var absorber = new TextFragmentAbsorber();
            loadedDoc.Pages.Accept(absorber);

            // Find the fragment that matches the sample text
            TextFragment foundFragment = null;
            foreach (TextFragment fragment in absorber.TextFragments)
            {
                if (fragment.Text == SampleText)
                {
                    foundFragment = fragment;
                    break;
                }
            }

            // Ensure the fragment was found
            NUnit.Framework.Assert.IsTrue(foundFragment != null, "Text fragment not found in the PDF.");

            // Verify X and Y coordinates (allow a small tolerance)
            const double tolerance = 0.5; // points tolerance
            // Position class exposes XIndent/YIndent, not X/Y
            NUnit.Framework.Assert.AreEqual(ExpectedX, foundFragment.Position.XIndent, tolerance, "X coordinate mismatch.");
            NUnit.Framework.Assert.AreEqual(ExpectedY, foundFragment.Position.YIndent, tolerance, "Y coordinate mismatch.");
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    // In a real test project this would be omitted and the project would be a library.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No operation – tests are executed by the test runner.
        }
    }
}
