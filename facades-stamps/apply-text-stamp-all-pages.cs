using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp – this is the type expected by Page.AddStamp
            TextStamp stamp = new TextStamp("CONFIDENTIAL");
            stamp.TextState.Font = FontRepository.FindFont("Helvetica");
            stamp.TextState.FontSize = 36;
            // Use fully‑qualified Aspose.Pdf.Color to avoid ambiguity with System.Drawing.Color
            stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Red;
            stamp.Opacity = 0.5f;
            // The TextStamp class does not expose an IsBackground property in recent versions;
            // use the Background property instead (or omit if not required).
            stamp.Background = true;
            // Position the stamp – you can use XIndent/YIndent or SetOrigin
            stamp.XIndent = 100f;
            stamp.YIndent = 400f;

            // Apply the same stamp to every page. The PageCollection does not have an AddStamp overload,
            // so iterate the pages and add the stamp individually – this is still efficient because the
            // same TextStamp instance is reused.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}
