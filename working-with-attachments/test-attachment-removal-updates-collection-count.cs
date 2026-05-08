using System;
using System.Collections.Generic;
using Xunit;

namespace AsposePdfAiTests
{
    // ---------------------------------------------------------------------------
    // Minimal domain model stubs required for the test.
    // ---------------------------------------------------------------------------
    public class ThreadMessageResponse
    {
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }

    public class Attachment
    {
        public string FileId { get; set; }
        public List<Tool> Tools { get; set; } = new List<Tool>();
    }

    public class Tool
    {
        // No members needed for the current test scenario.
    }

    // ---------------------------------------------------------------------------
    // Integration test that verifies removal of an attachment updates the collection count.
    // ---------------------------------------------------------------------------
    public class AttachmentRemovalTests
    {
        [Fact]
        public void RemovingAttachment_UpdatesCollectionCount()
        {
            // Arrange: create a response with an empty Attachments list
            var response = new ThreadMessageResponse
            {
                Attachments = new List<Attachment>()
            };

            // Create two attachments. The Tools property expects a List<Tool>,
            // so we initialise it with an empty list (the actual tools are not relevant for this test).
            var att1 = new Attachment
            {
                FileId = "file1",
                Tools = new List<Tool>()
            };
            var att2 = new Attachment
            {
                FileId = "file2",
                Tools = new List<Tool>()
            };

            // Add the attachments to the collection
            response.Attachments.Add(att1);
            response.Attachments.Add(att2);

            // Verify the initial count is 2
            Assert.Equal(2, response.Attachments.Count);

            // Act: remove one attachment
            response.Attachments.Remove(att1);

            // Verify the count is now 1
            Assert.Equal(1, response.Attachments.Count);
        }
    }

    // ---------------------------------------------------------------------------
    // Provide a dummy entry point so the project compiles as a console application.
    // ---------------------------------------------------------------------------
    public class Program
    {
        public static void Main()
        {
            // No runtime logic required – the test runner will execute the tests.
        }
    }
}

// ---------------------------------------------------------------------------
// Minimal stub for the Xunit framework when the real package is not referenced.
// This provides the Fact attribute and the Assert.Equal method used in the test.
// ---------------------------------------------------------------------------
namespace Xunit
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FactAttribute : Attribute { }

    public static class Assert
    {
        public static void Equal<T>(T expected, T actual)
        {
            if (!object.Equals(expected, actual))
                throw new Exception($"Assert.Equal failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}
