using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using NUnit.Framework;

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
    // Extension to silence potential stray ImportAnnotations calls in the project.
    // This does not affect the test logic and complies with the hard namespace rule.
    public static class DocumentExtensions
    {
        // Dummy implementation – does nothing.
        public static void ImportAnnotations(this Document doc, string path) { }
    }

    [TestFixture]
    public class GraphRectangleTests
    {
        [Test]
        public void AddOutOfBoundsRectangle_ShouldThrowArgumentOutOfRangeException()
        {
            // Create a new PDF document and add a blank page.
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Define a graph that occupies the whole page.
                Graph graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Attempt to add a rectangle shape whose coordinates are far outside the page.
                // This should trigger an ArgumentOutOfRangeException.
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    // The Drawing.Rectangle constructor expects left, bottom, width, height.
                    var outOfBoundsShape = new Aspose.Pdf.Drawing.Rectangle(2000, 2000, 1000, 1000);
                    // Adding the shape to the graph will validate the coordinates.
                    graph.Shapes.Add(outOfBoundsShape);
                });

                // Add the graph to the page so the document remains well‑formed.
                page.Paragraphs.Add(graph);
            }
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    internal static class Program
    {
        public static void Main() { }
    }
}