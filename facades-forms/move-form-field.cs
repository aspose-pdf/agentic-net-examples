using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "Logo";
        const float fieldWidth = 100f;
        const float fieldHeight = 50f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the document to obtain page dimensions.
        using (Document doc = new Document(inputPath))
        {
            // Coordinates for top‑left corner on the first page.
            float llx = 0f;
            // PageInfo.Height returns double – cast to float before arithmetic.
            float lly = (float)(doc.Pages[1].PageInfo.Height - fieldHeight);
            float urx = fieldWidth;
            float ury = (float)doc.Pages[1].PageInfo.Height;

            // Use the non‑obsolete FormEditor constructor that accepts a Document instance.
            using (FormEditor formEditor = new FormEditor(doc))
            {
                bool moved = formEditor.MoveField(fieldName, llx, lly, urx, ury);
                // Save to the destination file using the overload that takes a file path.
                formEditor.Save(outputPath);

                Console.WriteLine(moved ? "Field moved successfully." : "Failed to move field.");
            }
        }
    }
}
