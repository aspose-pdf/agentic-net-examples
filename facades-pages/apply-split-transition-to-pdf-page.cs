using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfPageEditor facade and bind the document
            PdfPageEditor editor = new PdfPageEditor();
            editor.BindPdf(doc);

            // Specify that only page 3 will be edited
            editor.ProcessPages = new int[] { 3 };

            // Set the transition effect to a horizontal split (IN) and duration to 2 seconds
            editor.TransitionType = PdfPageEditor.SPLITHIN; // IN Horizontal Split
            editor.TransitionDuration = 2; // duration in seconds

            // Apply the changes to the document
            editor.ApplyChanges();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Transition applied and saved to '{outputPath}'.");
    }
}