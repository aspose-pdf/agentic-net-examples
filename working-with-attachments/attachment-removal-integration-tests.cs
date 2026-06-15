using System;
using System.Collections.Generic;

// Core library namespace – contains the domain models used by the tests
namespace Aspose.Pdf.AI
{
    // Represents a file attached to a thread message.
    // The original code expected a List<Tool> for the Tools property, but the tests
    // assign a simple string (e.g. "ToolA"). To make the API usable from the tests
    // we expose Tools as a string. If a richer model is required later it can be
    // introduced without breaking the current contract.
    public class Attachment
    {
        // Non‑nullable properties are initialised with empty strings to avoid CS8625.
        public string FileId { get; set; } = string.Empty;
        public string Tools { get; set; } = string.Empty;
    }

    // Container for a collection of attachments returned by the AI service.
    public class ThreadMessageResponse
    {
        // Initialise the list to an empty collection so the property is never null.
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }

    // Minimal entry point required by the C# compiler. The project is a library used
    // by the test runner, but the compiler still expects a static Main method.
    public class Program
    {
        public static void Main(string[] args)
        {
            // Intentionally left blank – the executable does not perform any work.
        }
    }
}

// ---------------------------------------------------------------------------
// Mock NUnit framework – the real NUnit package is not referenced in this kata.
// ---------------------------------------------------------------------------
namespace NUnit.Framework
{
    using System;

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

// ---------------------------------------------------------------------------
// Integration tests that verify attachment removal updates the collection count.
// ---------------------------------------------------------------------------
namespace AsposePdfAiTests
{
    using Aspose.Pdf.AI;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class AttachmentCollectionTests
    {
        // Helper method to create a sample attachment
        private Attachment CreateAttachment(string fileId, string tools)
        {
            return new Attachment
            {
                FileId = fileId,
                Tools = tools
            };
        }

        [Test]
        public void RemovingAttachment_UpdatesCollectionCount()
        {
            // Arrange: create a response with three attachments
            ThreadMessageResponse response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>()
            };

            response.Attachments.Add(CreateAttachment("file1", "ToolA"));
            response.Attachments.Add(CreateAttachment("file2", "ToolB"));
            response.Attachments.Add(CreateAttachment("file3", "ToolC"));

            // Verify initial count is 3
            Assert.AreEqual(3, response.Attachments.Count, "Initial attachment count should be 3.");

            // Act: remove the second attachment
            response.Attachments.RemoveAt(1); // List uses zero‑based indexing

            // Assert: count should be reduced to 2
            Assert.AreEqual(2, response.Attachments.Count, "Attachment count should be 2 after removal.");

            // Additionally verify that the remaining attachments are the expected ones
            Assert.AreEqual("file1", response.Attachments[0].FileId, "First attachment should remain unchanged.");
            Assert.AreEqual("file3", response.Attachments[1].FileId, "Third attachment should now be at index 1.");
        }

        [Test]
        public void RemovingAllAttachments_ResultsInZeroCount()
        {
            // Arrange: create a response with two attachments
            ThreadMessageResponse response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>
                {
                    new Attachment { FileId = "a1", Tools = "ToolX" },
                    new Attachment { FileId = "a2", Tools = "ToolY" }
                }
            };

            // Verify initial count is 2
            Assert.AreEqual(2, response.Attachments.Count, "Initial attachment count should be 2.");

            // Act: clear the collection
            response.Attachments.Clear();

            // Assert: count should be 0
            Assert.AreEqual(0, response.Attachments.Count, "Attachment count should be 0 after clearing the collection.");
        }
    }
}
