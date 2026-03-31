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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            // Set a sample title for the document (could be any existing title)
            document.SetTitle("Sample Document Title for Transition Demo");

            // Retrieve the title and calculate a transition duration based on its length.
            // Example: 1 second per 10 characters, minimum 1 second.
            string title = document.Info.Title;
            int duration = 1;
            if (title != null)
            {
                int calculated = title.Length / 10;
                if (calculated > 0)
                {
                    duration = calculated;
                }
            }

            // Configure the page editor with the calculated transition settings.
            PdfPageEditor editor = new PdfPageEditor(document);
            editor.TransitionDuration = duration; // duration in seconds
            editor.TransitionType = PdfPageEditor.BLINDV; // example transition style
            editor.ApplyChanges(); // apply to all pages

            document.Save(outputPath);
        }

        Console.WriteLine("Transition applied and saved to 'output.pdf'.");
    }
}
