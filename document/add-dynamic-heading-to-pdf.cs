using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dynamic data for the heading (e.g., current date and user name)
        string userName = Environment.UserName;
        string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        string headingText = $"Report for {userName} – {currentDate}";

        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates structure if not present)
            ITaggedContent taggedContent = doc.TaggedContent;
            taggedContent.SetLanguage("en-US");
            taggedContent.SetTitle(Path.GetFileNameWithoutExtension(inputPath));

            // Root element of the structure tree
            StructureElement root = taggedContent.RootElement;

            // Create a heading (HeaderElement level 1) and set its text dynamically
            HeaderElement heading = taggedContent.CreateHeaderElement(1);
            heading.SetText(headingText);
            // Optional: set language on the heading element itself
            heading.Language = "en-US";

            // Append the heading to the root of the structure tree
            root.AppendChild(heading);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with dynamic heading to '{outputPath}'.");
    }
}