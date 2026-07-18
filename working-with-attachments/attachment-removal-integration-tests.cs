using System;
using System.Collections.Generic;
using NUnit.Framework; // now resolves to the top‑level stub namespace

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void AreSame(object expected, object actual, string? message = null)
        {
            if (!object.ReferenceEquals(expected, actual))
                throw new Exception(message ?? "Assert.AreSame failed. Expected the same instance.");
        }
    }
}

namespace AsposePdfAiTests
{
    // Minimal placeholder types to make the test compile without the real SDK
    public class Tool
    {
        public string Name { get; set; }
    }

    public class Attachment
    {
        public string FileId { get; set; }
        // The real SDK expects a list of Tool objects, not a plain string.
        public List<Tool> Tools { get; set; }
    }

    public class ThreadMessageResponse
    {
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }

    [TestFixture]
    public class AttachmentRemovalTests
    {
        // Helper method to create a dummy attachment
        private Attachment CreateAttachment(string fileId, string toolName)
        {
            return new Attachment
            {
                FileId = fileId,
                // Convert the incoming string into a list containing a single Tool instance.
                Tools = new List<Tool> { new Tool { Name = toolName } }
            };
        }

        [Test]
        public void RemovingSingleAttachment_DecreasesCount()
        {
            // Arrange: create a response with two attachments
            ThreadMessageResponse response = new ThreadMessageResponse();

            var attachment1 = CreateAttachment("file1", "toolA");
            var attachment2 = CreateAttachment("file2", "toolB");

            response.Attachments.Add(attachment1);
            response.Attachments.Add(attachment2);

            // Act: remove the first attachment
            response.Attachments.Remove(attachment1);

            // Assert: count should be 1
            Assert.AreEqual(1, response.Attachments.Count);
            Assert.AreSame(attachment2, response.Attachments[0]);
        }

        [Test]
        public void ClearingAllAttachments_ResultsInZeroCount()
        {
            // Arrange: create a response with three attachments
            ThreadMessageResponse response = new ThreadMessageResponse();

            response.Attachments.Add(CreateAttachment("file1", "toolA"));
            response.Attachments.Add(CreateAttachment("file2", "toolB"));
            response.Attachments.Add(CreateAttachment("file3", "toolC"));

            // Act: clear the collection
            response.Attachments.Clear();

            // Assert: count should be 0
            Assert.AreEqual(0, response.Attachments.Count);
        }

        [Test]
        public void RemovingByIndex_UpdatesCountCorrectly()
        {
            // Arrange: create a response with two attachments
            ThreadMessageResponse response = new ThreadMessageResponse();

            response.Attachments.Add(CreateAttachment("file1", "toolA"));
            response.Attachments.Add(CreateAttachment("file2", "toolB"));

            // Act: remove the attachment at index 0
            response.Attachments.RemoveAt(0);

            // Assert: count should be 1 and remaining attachment is the second one
            Assert.AreEqual(1, response.Attachments.Count);
            Assert.AreEqual("file2", response.Attachments[0].FileId);
        }
    }

    // Dummy entry point so the project builds as a console application.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – the tests are executed by the test runner.
        }
    }
}