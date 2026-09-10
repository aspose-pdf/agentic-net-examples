using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using NUnit.Framework; // Added to bring NUnit stubs into scope

// Minimal NUnit stubs to allow compilation without the NUnit package.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

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

namespace AsposePdfTests
{
    [TestFixture]
    public class GraphBoundsTests
    {
        [Test]
        public void AddingOutOfBoundsRectangle_ShouldThrowBoundsOutOfRangeException()
        {
            // Arrange: create a PDF document with a single page of known size.
            using (Document doc = new Document())
            {
                // Add a page and set its dimensions (e.g., 500 x 500 points).
                Page page = doc.Pages.Add();
                page.PageInfo.Width = 500;
                page.PageInfo.Height = 500;

                // Create a Graph container (size does not matter for the test).
                // Use the double overload to avoid the obsolete constructor warning.
                Graph graph = new Graph(400.0, 400.0);

                // Enable bounds checking that throws when an item does not fit.
                // The container dimensions are the page size.
                graph.Shapes.UpdateBoundsCheckMode(
                    BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Define a rectangle that lies partially outside the page bounds.
                // Left=450, Bottom=450, Width=100, Height=100 => exceeds 500x500 page.
                Aspose.Pdf.Drawing.Rectangle outOfBoundsRect = new Aspose.Pdf.Drawing.Rectangle(450f, 450f, 100f, 100f);

                // Act & Assert: adding the rectangle should raise BoundsOutOfRangeException.
                Assert.Throws<BoundsOutOfRangeException>(() =>
                {
                    // This call triggers the bounds check.
                    graph.Shapes.Add(outOfBoundsRect);
                });
            }
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public class Program
    {
        public static void Main() { }
    }
}
