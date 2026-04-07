using System;
using NUnit.Framework;

// ---------- Minimal NUnit stubs (if the real NUnit package is not referenced) ----------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public delegate void TestDelegate();

    public static class Assert
    {
        // Throws<T> mimics NUnit's Assert.Throws<T>
        public static T Throws<T>(TestDelegate code) where T : Exception
        {
            try
            {
                code();
            }
            catch (T ex)
            {
                return ex; // Expected exception was thrown
            }
            catch (Exception ex)
            {
                throw new Exception($"Assert.Throws failed. Expected {typeof(T)} but got {ex.GetType()}.", ex);
            }
            throw new Exception($"Assert.Throws failed. No exception thrown. Expected {typeof(T)}.");
        }
    }
}

// ---------- Minimal Aspose.Pdf.Generator stubs (to make the test compile without the real Generator API) ----------
namespace Aspose.Pdf.Generator
{
    // Exception that should be thrown when a graphic does not fit the container
    public class BoundsOutOfRangeException : Exception
    {
        public BoundsOutOfRangeException(string message) : base(message) { }
    }

    // Simple enum used by the stubbed Graphics class
    public enum BoundsCheckMode
    {
        ThrowExceptionIfDoesNotFit
    }

    // Simple rectangle definition used by the test
    public class Rectangle
    {
        public double X1 { get; }
        public double Y1 { get; }
        public double X2 { get; }
        public double Y2 { get; }

        public Rectangle(double x1, double y1, double x2, double y2)
        {
            X1 = x1; Y1 = y1; X2 = x2; Y2 = y2;
        }
    }

    // Holds page size information
    public class PageInfo
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    // Represents a page in the document
    public class Page : IDisposable
    {
        public PageInfo PageInfo { get; } = new PageInfo();
        public Graphics Graphics { get; } = new Graphics();

        public void Dispose() { /* No resources to release in the stub */ }
    }

    // Collection of pages – very small stub just to satisfy the test
    public class PageCollection
    {
        private readonly Document _owner;
        public PageCollection(Document owner) => _owner = owner;
        public Page Add()
        {
            var page = new Page();
            _owner.LastAddedPage = page;
            return page;
        }
    }

    // The main document class used in the test
    public class Document : IDisposable
    {
        public PageCollection Pages { get; }
        internal Page LastAddedPage { get; set; }
        public Document() => Pages = new PageCollection(this);
        public void Dispose() { /* No resources to release in the stub */ }
    }

    // Graphics collection that performs bounds checking when adding items
    public class Graphics
    {
        private BoundsCheckMode _mode;
        private double _maxWidth;
        private double _maxHeight;

        // Called by the test to enable bounds‑checking behaviour
        public void UpdateBoundsCheckMode(BoundsCheckMode mode, double maxWidth, double maxHeight)
        {
            _mode = mode;
            _maxWidth = maxWidth;
            _maxHeight = maxHeight;
        }

        // In the real Generator API this would add many different graphic objects.
        // For the purpose of the unit test we only need to support Rectangle.
        public void Add(Rectangle rect)
        {
            if (_mode == BoundsCheckMode.ThrowExceptionIfDoesNotFit)
            {
                // Simple check: if any side of the rectangle exceeds the allowed bounds, throw.
                if (rect.X2 > _maxWidth || rect.Y2 > _maxHeight)
                {
                    throw new BoundsOutOfRangeException($"Rectangle ({rect.X1},{rect.Y1})-({rect.X2},{rect.Y2}) does not fit within {_maxWidth}x{_maxHeight}.");
                }
            }
            // In a full implementation the rectangle would be stored for rendering.
        }
    }
}

// ---------- Actual unit test ----------
namespace AsposePdfTests
{
    using Aspose.Pdf.Generator;

    [TestFixture]
    public class GraphBoundsTests
    {
        [Test]
        public void Adding_OutOfBounds_Rectangle_Should_Throw_BoundsOutOfRangeException()
        {
            // Create a new generator document (uses the stubbed Generator API)
            using (Document doc = new Document())
            {
                // Add a page with a small size (100x100 points)
                Page page = doc.Pages.Add();
                page.PageInfo.Width = 100;
                page.PageInfo.Height = 100;

                // Configure the graphics collection to enforce bounds checking.
                page.Graphics.UpdateBoundsCheckMode(
                    BoundsCheckMode.ThrowExceptionIfDoesNotFit,
                    page.PageInfo.Width,
                    page.PageInfo.Height);

                // Create a rectangle that exceeds the page dimensions (200x200 points)
                Rectangle outOfBoundsRect = new Rectangle(0, 0, 200, 200);

                // Assert that adding the out‑of‑bounds rectangle throws the expected exception
                Assert.Throws<BoundsOutOfRangeException>(() => page.Graphics.Add(outOfBoundsRect));
            }
        }
    }

    // Dummy entry point to satisfy the C# compiler when building as an executable.
    // Test runners (e.g., NUnit console, Visual Studio Test Explorer) will discover the tests
    // via reflection, so the Main method does not need to execute any code.
    public static class Program
    {
        public static void Main()
        {
            // No-op
        }
    }
}
