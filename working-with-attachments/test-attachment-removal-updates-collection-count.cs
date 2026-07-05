using System;
using System.Collections.Generic;
using Aspose.Pdf.AI; // Namespace containing Attachment and ThreadMessageResponse

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
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfAiTests
{
    // Integration tests for attachment handling
    [NUnit.Framework.TestFixture]
    public class AttachmentRemovalTests
    {
        [NUnit.Framework.Test]
        public void RemovingAttachment_UpdatesCollectionCount()
        {
            // Arrange: create a response with two attachments
            ThreadMessageResponse response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>()
            };

            // The Attachment.Tools property expects a List<Tool>.  An empty list is sufficient for the test.
            Attachment attachment1 = new Attachment { FileId = "file1", Tools = new List<Tool>() };
            Attachment attachment2 = new Attachment { FileId = "file2", Tools = new List<Tool>() };

            response.Attachments.Add(attachment1);
            response.Attachments.Add(attachment2);

            // Verify initial count
            NUnit.Framework.Assert.AreEqual(2, response.Attachments.Count, "Initial attachment count should be 2.");

            // Act: remove one attachment
            response.Attachments.Remove(attachment1);

            // Assert: count reflects removal
            NUnit.Framework.Assert.AreEqual(1, response.Attachments.Count, "Attachment count should decrement after removal.");

            // Act: remove the remaining attachment by index
            response.Attachments.RemoveAt(0);

            // Assert: collection is empty
            NUnit.Framework.Assert.AreEqual(0, response.Attachments.Count, "Attachment count should be zero after removing all items.");
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
