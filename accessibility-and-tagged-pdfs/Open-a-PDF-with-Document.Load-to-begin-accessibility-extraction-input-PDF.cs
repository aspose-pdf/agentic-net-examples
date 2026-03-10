using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document; the using block ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (if present) to start accessibility extraction
            ITaggedContent taggedContent = doc.TaggedContent;

            // Example diagnostics
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Determine if the PDF is tagged (no direct IsTaggedPdf property in recent versions)
            bool isTagged = taggedContent != null;
            Console.WriteLine($"Is tagged PDF: {isTagged}");

            // Accessibility extraction logic would follow here...
        }
    }
}