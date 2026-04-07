using System;
using System.Collections.Generic;
using Aspose.Pdf.AI; // Namespace containing Attachment and ThreadMessageResponse

// Minimal NUnit stubs to allow compilation without the real NUnit package
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

        public static void AreSame(object expected, object actual, string message = null)
        {
            if (!object.ReferenceEquals(expected, actual))
                throw new Exception(message ?? "Assert.AreSame failed. Expected the same instance.");
        }
    }
}

// Dummy entry point to satisfy the compiler (project expects an executable)
public class Program
{
    public static void Main(string[] args)
    {
        // No runtime logic required for the unit‑test library.
    }
}

namespace AsposePdfAiTests
{
    using NUnit.Framework;

    [TestFixture]
    public class AttachmentTests
    {
        // Helper method to create a dummy attachment
        private Attachment CreateAttachment(string fileId = "file1", string tools = "toolA")
        {
            return new Attachment
            {
                FileId = fileId,
                // The Tools property expects a List<Tool>. Provide an empty list (or populate as needed).
                Tools = new List<Tool>()
            };
        }

        [Test]
        public void RemovalUpdatesCount()
        {
            // Arrange: create a response with two attachments
            var response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>()
            };

            var att1 = CreateAttachment("file1", "toolA");
            var att2 = CreateAttachment("file2", "toolB");

            response.Attachments.Add(att1);
            response.Attachments.Add(att2);

            // Verify initial count
            Assert.AreEqual(2, response.Attachments.Count, "Initial attachment count should be 2.");

            // Act: remove one attachment
            response.Attachments.Remove(att1);

            // Assert: count should be decremented and remaining item should be att2
            Assert.AreEqual(1, response.Attachments.Count, "Attachment count should be 1 after removal.");
            Assert.AreSame(att2, response.Attachments[0], "Remaining attachment should be the second one added.");
        }
    }
}
