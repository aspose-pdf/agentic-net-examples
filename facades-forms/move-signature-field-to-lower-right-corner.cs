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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Ensure the document has at least 5 pages (1‑based indexing)
        if (doc.Pages.Count < 5)
        {
            Console.Error.WriteLine("Document does not contain page 5.");
            return;
        }

        Page page5 = doc.Pages[5];

        // Desired size of the signature field (in points)
        const float fieldWidth = 150f; // width
        const float fieldHeight = 50f; // height

        // Position the field in the lower‑right corner with a 20‑point margin
        // page5.PageInfo.Width is double, so cast to float after the calculation
        float llx = (float)(page5.PageInfo.Width - fieldWidth - 20f); // lower‑left X
        float lly = 20f;                                            // lower‑left Y
        float urx = llx + fieldWidth;                               // upper‑right X
        float ury = lly + fieldHeight;                              // upper‑right Y

        // Use the non‑obsolete FormEditor constructor that works with a Document instance
        using (FormEditor formEditor = new FormEditor(doc))
        {
            bool moved = formEditor.MoveField("Signature", llx, lly, urx, ury);
            if (!moved)
            {
                Console.Error.WriteLine("Failed to move the field. Verify the field name exists.");
            }

            // Save the changes to the specified output file (non‑obsolete overload)
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Signature field repositioned and saved to '{outputPath}'.");
    }
}
