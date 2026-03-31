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
            PdfPageEditor editor = new PdfPageEditor(document);
            // Edit only page 5
            editor.ProcessPages = new int[] { 5 };
            // Set dissolve transition type
            editor.TransitionType = PdfPageEditor.DISSOLVE;
            // Set transition duration to 3 seconds
            editor.TransitionDuration = 3;
            // Apply the changes to the document
            editor.ApplyChanges();

            document.Save(outputPath);
        }

        Console.WriteLine("Transition applied and saved to '" + outputPath + "'.");
    }
}
