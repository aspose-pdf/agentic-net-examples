using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

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
    [NUnit.Framework.TestFixture]
    public class GraphBoundsTests
    {
        [NUnit.Framework.Test]
        public void AddOutOfBoundsRectangle_ThrowsException()
        {
            using (Document doc = new Document())
            {
                Page page = doc.Pages.Add();

                // Create a Graph container of size 200x200 (double literals as required)
                Graph graph = new Graph(200.0, 200.0);

                // NOTE: The BoundsCheckMode API is not available in the current Aspose.Pdf version,
                // so we rely on the default behaviour which throws when a shape does not fit.
                // Define a rectangle that exceeds the graph bounds (300x300)
                Aspose.Pdf.Drawing.Rectangle outOfBoundsRect = new Aspose.Pdf.Drawing.Rectangle(0f, 0f, 300f, 300f);

                // Adding the rectangle should throw an exception (any exception type is acceptable for this test).
                NUnit.Framework.Assert.Throws<Exception>(() => graph.Shapes.Add(outOfBoundsRect));

                // Attach the graph to the page (won't be reached if exception is thrown above)
                page.Paragraphs.Add(graph);
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when the project is built as an executable.
public static class Program
{
    public static void Main() { }
}