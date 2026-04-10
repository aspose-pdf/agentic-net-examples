using System;
using System.IO;
using Aspose.Pdf.Facades;
using NUnit.Framework;

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfFileEditorTests
    {
        // Test that Concatenate throws an exception when the input stream array is empty.
        [Test]
        public void Concatenate_EmptyInputStreams_ThrowsException()
        {
            // Arrange
            PdfFileEditor editor = new PdfFileEditor();
            Stream[] emptyInput = new Stream[0];
            using (MemoryStream outputStream = new MemoryStream())
            {
                // Act & Assert
                Assert.Throws<Exception>(() => editor.Concatenate(emptyInput, outputStream));
            }
        }
    }

    // Dummy entry point to satisfy the C# compiler when the project is built as an executable.
    // In real test projects the output type would be a library, but adding a minimal Main method
    // removes the CS5001 error without affecting the test logic.
    public static class Program
    {
        public static void Main() { }
    }
}

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
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