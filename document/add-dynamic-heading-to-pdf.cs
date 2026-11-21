using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;
using Aspose.Pdf.LogicalStructure;

class Program
{
    static void Main()
    {
        const string inputPath  = "template.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Dynamic data to insert into the heading
        string userName = Environment.UserName;
        string dateStr  = DateTime.Now.ToString("yyyy-MM-dd");

        // Load the PDF and work within a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access tagged content (creates a tagged structure if none exists)
            ITaggedContent tagged = doc.TaggedContent;

            // Enable auto‑tagging globally (optional but ensures a structure tree)
            AutoTaggingSettings.Default.EnableAutoTagging = true;

            // Set document language and title (optional)
            tagged.SetLanguage("en-US");
            tagged.SetTitle(Path.GetFileNameWithoutExtension(outputPath));

            // Root of the structure tree
            StructureElement root = tagged.RootElement;

            // Create a level‑1 heading and set its text dynamically
            HeaderElement heading = tagged.CreateHeaderElement(1);
            heading.SetText($"Report for {userName} on {dateStr}");

            // Attach the heading to the root element
            root.AppendChild(heading);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}