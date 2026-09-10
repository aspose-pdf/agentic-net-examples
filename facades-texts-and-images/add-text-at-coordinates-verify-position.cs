using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ---------------------------------------------------------------------------
// Minimal stubs for MSTest attributes and Assert when the MSTest package is not
// referenced. They are placed in the same namespace that the test code expects.
// ---------------------------------------------------------------------------
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestClassAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestMethodAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfFacadesTests
{
    [TestClass]
    public class AddTextCoordinateTests
    {
        // Expected coordinates for the added text (in points; 1 inch = 72 points)
        private const float ExpectedX = 100f;
        private const float ExpectedY = 200f;
        private const string TestString = "SampleText";

        // Helper method to create a simple one‑page PDF document
        private static Document CreateBlankDocument()
        {
            // Create a new PDF document and add a single blank page
            Document doc = new Document();
            doc.Pages.Add();
            return doc;
        }

        // Helper method to add text at a specific position using TextBuilder (core API)
        private static void AddTextAtPosition(Document doc, int pageNumber, string text, float x, float y)
        {
            // Obtain the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[pageNumber];

            // Create a TextFragment with the desired string
            TextFragment fragment = new TextFragment(text);

            // Set the lower‑left corner of the text (baseline position)
            fragment.Position = new Position(x, y);

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);
        }

        // Helper method to extract the first occurrence of a string and return its position
        private static Position GetTextPosition(Document doc, int pageNumber, string searchText)
        {
            // Use TextFragmentAbsorber to locate the text on the specified page
            TextFragmentAbsorber absorber = new TextFragmentAbsorber(searchText);
            doc.Pages[pageNumber].Accept(absorber);

            // Ensure the text was found
            if (absorber.TextFragments.Count == 0)
                throw new InvalidOperationException($"Text \"{searchText}\" not found on page {pageNumber}.");

            // Return the Position of the first found fragment
            return absorber.TextFragments[0].Position;
        }

        [TestMethod]
        public void VerifyAddedTextAppearsAtExpectedCoordinates()
        {
            // Create a temporary file path for the generated PDF
            string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // ------------------------------------------------------------
                // 1. Create a blank PDF and add the test text at the expected coordinates
                // ------------------------------------------------------------
                using (Document doc = CreateBlankDocument())
                {
                    AddTextAtPosition(doc, pageNumber: 1, text: TestString, x: ExpectedX, y: ExpectedY);

                    // Save the document to a temporary file
                    doc.Save(tempPdfPath);
                }

                // ------------------------------------------------------------
                // 2. Load the saved PDF and verify the text position
                // ------------------------------------------------------------
                using (Document loadedDoc = new Document(tempPdfPath))
                {
                    Position actualPos = GetTextPosition(loadedDoc, pageNumber: 1, searchText: TestString);

                    // Allow a small tolerance due to floating‑point rounding
                    const float tolerance = 0.01f;

                    Assert.IsTrue(Math.Abs(actualPos.XIndent - ExpectedX) <= tolerance,
                        $"X coordinate mismatch. Expected: {ExpectedX}, Actual: {actualPos.XIndent}");

                    Assert.IsTrue(Math.Abs(actualPos.YIndent - ExpectedY) <= tolerance,
                        $"Y coordinate mismatch. Expected: {ExpectedY}, Actual: {actualPos.YIndent}");
                }
            }
            finally
            {
                // Clean up the temporary file
                if (File.Exists(tempPdfPath))
                {
                    try { File.Delete(tempPdfPath); } catch { /* ignore cleanup errors */ }
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public class Program
    {
        public static void Main() { }
    }
}
