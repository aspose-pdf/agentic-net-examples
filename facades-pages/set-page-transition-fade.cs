using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document document = new Document(inputPath))
        {
            using (PdfPageEditor editor = new PdfPageEditor(document))
            {
                // Edit only the first page (1‑based index)
                editor.ProcessPages = new int[] { 1 };

                // Set fade transition (integer value 9) and a duration of 2 seconds
                editor.TransitionType = 9; // Fade transition
                editor.TransitionDuration = 2; // seconds

                editor.ApplyChanges();
            }

            document.Save(outputPath);
        }

        Console.WriteLine("Transition applied and saved to '" + outputPath + "'.");
    }
}
