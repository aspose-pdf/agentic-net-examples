using System;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        TestAddOutOfBoundsRectangle();
    }

    private static void TestAddOutOfBoundsRectangle()
    {
        const string testName = "AddOutOfBoundsRectangle_ShouldThrowArgumentException";
        try
        {
            // Create a new PDF document in memory.
            using (var doc = new Document())
            {
                // Add a default A4 page (approximately 595x842 points).
                Page page = doc.Pages.Add();

                // Create a Graph that spans the whole page – Graph is a BaseParagraph.
                var graph = new Graph(page.PageInfo.Width, page.PageInfo.Height);

                // Define a rectangle that exceeds the page bounds.
                // The constructor arguments are (left, bottom, width, height).
                var outOfBoundsRect = new Aspose.Pdf.Drawing.Rectangle(0, 0, 2000, 2000);
                // Minimal styling to keep the shape valid before the bounds check.
                outOfBoundsRect.GraphInfo.FillColor = Color.Transparent;
                outOfBoundsRect.GraphInfo.Color = Color.Black;

                // Add the rectangle to the Graph's shape collection.
                graph.Shapes.Add(outOfBoundsRect);

                // Attempt to add the Graph to the page – Aspose.Pdf is expected to throw ArgumentException.
                page.Paragraphs.Add(graph);
            }

            // If we reach this point, no exception was thrown – the test fails.
            Console.WriteLine($"FAIL {testName}: no exception thrown");
        }
        catch (ArgumentException)
        {
            // Expected exception – the test passes.
            Console.WriteLine($"PASS {testName}");
        }
        catch (Exception ex)
        {
            // Any other exception type means the behavior is not as expected.
            Console.WriteLine($"FAIL {testName}: unexpected exception {ex.GetType().Name}");
        }
    }
}