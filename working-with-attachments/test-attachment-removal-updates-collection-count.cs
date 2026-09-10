using System;
using System.Collections.Generic;
using Aspose.Pdf.AI;
using NUnit.Framework;

namespace AsposePdfAiTests
{
    [TestFixture]
    public class AttachmentRemovalTests
    {
        // Helper method to create a sample attachment
        private Attachment CreateSampleAttachment(string fileId)
        {
            return new Attachment
            {
                FileId = fileId,
                // The Tools property expects a list of Tool objects, not strings.
                // Initialise it with an empty list (or populate with real Tool instances if needed).
                Tools = new List<Tool>()
            };
        }

        [Test]
        public void RemovingAttachment_DecrementsCollectionCount()
        {
            // Arrange: create a ThreadMessageResponse with a list of attachments
            ThreadMessageResponse response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>()
            };

            // Add two attachments
            response.Attachments.Add(CreateSampleAttachment("file1"));
            response.Attachments.Add(CreateSampleAttachment("file2"));

            // Verify initial count
            Assert.AreEqual(2, response.Attachments.Count, "Initial attachment count should be 2.");

            // Act: remove the first attachment
            response.Attachments.RemoveAt(0);

            // Assert: count should be decremented
            Assert.AreEqual(1, response.Attachments.Count, "Attachment count should be 1 after removal.");
        }

        [Test]
        public void ClearingAttachments_ResultsInZeroCount()
        {
            // Arrange: create a response with three attachments
            ThreadMessageResponse response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>
                {
                    new Attachment { FileId = "fileA", Tools = new List<Tool>() },
                    new Attachment { FileId = "fileB", Tools = new List<Tool>() },
                    new Attachment { FileId = "fileC", Tools = new List<Tool>() }
                }
            };

            // Verify initial count
            Assert.AreEqual(3, response.Attachments.Count, "Initial attachment count should be 3.");

            // Act: clear the collection
            response.Attachments.Clear();

            // Assert: count should be zero
            Assert.AreEqual(0, response.Attachments.Count, "Attachment count should be 0 after clearing.");
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    // In a real test project this would be compiled as a library, but adding a Main method
    // removes the CS5001 error without affecting the tests.
    public static class Program
    {
        public static void Main() { /* No-op */ }
    }
}

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!EqualityComparer<T>.Default.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}
