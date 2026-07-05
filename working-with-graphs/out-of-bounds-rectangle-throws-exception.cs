using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using NUnit.Framework; // Added using directive for NUnit stubs

// Minimal NUnit stubs to allow compilation without the NUnit package
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

[TestFixture]
public class GraphBoundsTests
{
    [Test]
    public void AddingOutOfBoundsRectangle_ShouldThrowBoundsOutOfRangeException()
    {
        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the container dimensions for the graph
            double containerWidth = 200;
            double containerHeight = 200;

            // Create a graph that fits exactly inside the container
            Graph graph = new Graph(containerWidth, containerHeight);

            // Enable strict bounds checking: an exception is thrown if an element does not fit
            graph.Shapes.UpdateBoundsCheckMode(
                BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                containerWidth,
                containerHeight);

            // Add the graph to the page's content
            page.Paragraphs.Add(graph);

            // Create a rectangle that is larger than the graph's container (300x300)
            // Drawing.Rectangle constructor: (left, bottom, width, height)
            Aspose.Pdf.Drawing.Rectangle outOfBoundsRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 300f, 300f);

            // Adding the rectangle should raise BoundsOutOfRangeException
            Assert.Throws<BoundsOutOfRangeException>(() => graph.Shapes.Add(outOfBoundsRect));
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑type project.
public class Program
{
    public static void Main() { }
}
